using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using NDepend;
using NDepend.Project;
using NDepend.CodeModel;
using System.Reflection;
using ExtensionMethods;

namespace NDependMetricsReporter
{
    public partial class MetricsViewer : Form
    {
        bool inhibitAutocheckOnDoubleClick;
        NDependServicesProvider nDependServicesProvider;
        CodeElementsManager codeElementsManager;
        IProject nDependProject;
        DataTableHelper dataTableHelper;

        public MetricsViewer()
        {
            InitializeComponent();
            nDependServicesProvider = new NDependServicesProvider();
            TagAllDataGridViews();
        }

        private void OpenNdependProject()
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            if (nDependProject == null) return;
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            codeElementsManager = new CodeElementsManager(lastAnalysisCodebase);
            dataTableHelper = new DataTableHelper(codeElementsManager);
            FillBaseControls();
        }

        private void TagAllDataGridViews()
        {
            Type asemblyType = typeof(IAssembly);
            Type namespaceType = typeof(INamespace);
            Type typeType = typeof(IType);
            Type methodType = typeof(IMethod);

            //Code tab DataGridViews
            TagDataGridView(this.dgvCodeAssemblies, null, this.dgvCodeNamespaces, asemblyType);
            TagDataGridView(this.dgvCodeNamespaces, this.dgvCodeAssemblies, this.dgvCodeTypes, namespaceType);
            TagDataGridView(this.dgvCodeTypes, this.dgvCodeNamespaces, this.dgvCodeMethods, typeType);
            TagDataGridView(this.dgvCodeMethods, this.dgvCodeTypes, null, methodType);

            //UnitTests tab DataGridViews
            TagDataGridView(this.dgvUnitTestsAssemblies, null, this.dgvUnitTestsNamespaces, asemblyType);
            TagDataGridView(this.dgvUnitTestsNamespaces, this.dgvUnitTestsAssemblies, this.dgvUnitTestsTypes, namespaceType);
            TagDataGridView(this.dgvUnitTestsTypes, this.dgvUnitTestsNamespaces, this.dgvUnitTestsMethods, typeType);
            TagDataGridView(this.dgvUnitTestsMethods, this.dgvUnitTestsTypes, null, methodType);

            //SpecFlow tab DataGridViews
            TagDataGridView(this.dgvBDDAssemblies, null, this.dgvBDDNamespaces, asemblyType);
            TagDataGridView(this.dgvBDDNamespaces, this.dgvBDDAssemblies, this.dgvBDDTypes, namespaceType);
            TagDataGridView(this.dgvBDDTypes, this.dgvBDDNamespaces, this.dgvBDDMethods, typeType);
            TagDataGridView(this.dgvBDDMethods, this.dgvBDDTypes, null, methodType);
        }

        private void TagDataGridView(DataGridView dataGridViewToTag, DataGridView parentDataGridView, DataGridView childDataGridView, Type codeElementType)
        {
            LinkedDatagrids linkedDatagrids = new LinkedDatagrids(parentDataGridView, childDataGridView);
            DataGridViewTagInfo dataGridViewTagInfo = new DataGridViewTagInfo(codeElementType, linkedDatagrids);
            dataGridViewToTag.Tag = dataGridViewTagInfo;
        }

        private void FillBaseControls()
        {
            FillNDependProjectInfo();
            List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
            IEnumerable<IAssembly> lastAnalysisAssembliesList = codeElementsManager.GetNonThirdPartyAssembliesInApplication();
            DataTable assemblyMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<IAssembly>(lastAnalysisAssembliesList, assemblyMetricsDefinionsList);
            FillCodeAsembliestDataGridView(assemblyMetricsDataTable);
            FillTestAssembliesDataGridView(assemblyMetricsDataTable);
            FillSpecFlowBDDAssembliesDataGridView(assemblyMetricsDataTable);
        }

        private void FillNDependProjectInfo()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            this.tBoxProjectPath.Text = nDependProject.Properties.FilePath.ToString();
        }

        private void FillCodeAsembliestDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] NOT LIKE '*UnitTest*') AND ([Code Element] NOT LIKE '*SpecFlowBDD*')";
            DataTable onlyCodeAsemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvCodeAssemblies, onlyCodeAsemblies);
        }

        private void FillTestAssembliesDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] LIKE '*UnitTest*')";
            DataTable onlyTestAssemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvUnitTestsAssemblies, onlyTestAssemblies);
        }

        private void FillSpecFlowBDDAssembliesDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] LIKE '*SpecFlowBDD*')";
            DataTable onlySpecFlowBDDAssemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvBDDAssemblies, onlySpecFlowBDDAssemblies);
        }

        private void FillCodeElementsDataGridView(DataGridView codeElementMetricsDataGridView, DataTable codeElementMetricsDataTable)
        {
            bool filledForFirstTime = (codeElementMetricsDataGridView.DataSource == null);
            codeElementMetricsDataGridView.DataSource = codeElementMetricsDataTable;
            if (filledForFirstTime)
            {
                codeElementMetricsDataGridView.RowHeadersWidth = 25;
                foreach (DataGridViewColumn dvgc in codeElementMetricsDataGridView.Columns)
                {
                    dvgc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dvgc.Visible = false;
                }
                codeElementMetricsDataGridView.Columns[0].Visible = true;
                codeElementMetricsDataGridView.Columns[0].Frozen = true;
            }
        }

        private void FillMetricsListView(ListView targetListview, DataGridViewRow selectedDataGridViewRow, List<NDependMetricDefinition> metricDefinitionsList)
        {
            string formatSpecifier = "0.####";
            DataGridView sourceDataGridView = selectedDataGridViewRow.DataGridView;
            targetListview.Tag = sourceDataGridView;
            targetListview.Items.Clear();
            foreach (NDependMetricDefinition metricDefinition in metricDefinitionsList)
            {
                double cellValue = Double.Parse(selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Value.ToString());
                string formatedCellValue = cellValue.ToString(formatSpecifier);
                ListViewItem lvi = new ListViewItem(new[] { metricDefinition.PropertyName, formatedCellValue });
                lvi.Tag = metricDefinition;
                lvi.Checked = selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Displayed;
                targetListview.Items.Add(lvi);
            }
        }

        private void FillMetricDescriptionRTFBox(RichTextBox targetRichBox, NDependMetricDefinition nDependMetricDefinition)
        {
            targetRichBox.Clear();
            targetRichBox.AppendText(nDependMetricDefinition.MetricName);
            targetRichBox.Find(nDependMetricDefinition.MetricName);
            targetRichBox.SelectionFont = new Font(rtfCodeMetricProperties.Font, rtfCodeMetricProperties.Font.Style ^ FontStyle.Bold);
            targetRichBox.SelectionStart = this.rtfCodeMetricProperties.Text.Length;
            targetRichBox.SelectionLength = 0;
            targetRichBox.SelectionFont = rtfCodeMetricProperties.Font;
            targetRichBox.AppendText(Environment.NewLine + nDependMetricDefinition.Description);
        }

        private DataGridView GetAssebliesDataGridView(DataGridView selectedDataGridView)
        {
            string dataGridViewCodeElementsType = ((DataGridViewTagInfo)selectedDataGridView.Tag).CodeElementsType.Name;
            if (dataGridViewCodeElementsType == "IAssembly") return selectedDataGridView;
            return GetAssebliesDataGridView(((DataGridViewTagInfo)selectedDataGridView.Tag).LinkedDataGrids.ParentDataGridView);
        }

        private void dgvCodeAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            AssembliesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeNamespaces, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            AssembliesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsNamespaces, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvCodeNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeTypes, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsTypes, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvCodeTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeMethods, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsMethods, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvCodeMethods_SelectionChanged(object sender, EventArgs e)
        {
            MethodsDataGridViewSelectionChangedEventManager((DataGridView)sender, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsMethods_SelectionChanged(object sender, EventArgs e)
        {
            MethodsDataGridViewSelectionChangedEventManager((DataGridView)sender, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvBDDAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            AssembliesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvBDDNamespaces, lvwBDDMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void dgvBDDNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvBDDTypes, lvwBDDMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void dgvBDDTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvBDDMethods, lvwBDDMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void dgvBDDMethods_SelectionChanged(object sender, EventArgs e)
        {
            MethodsDataGridViewSelectionChangedEventManager((DataGridView)sender, lvwBDDMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void AssembliesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetNamespacesDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedAssemblyName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IAssembly assembly = codeElementsManager.GetAssemblyByName(selectedAssemblyName);

                List<NDependMetricDefinition> assemblyMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("AssemblyMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], assemblyMetricsDefinionsList);

                targetElementTypeLabel.Text = "Assembly";
                targetElementNameLabel.Text = selectedAssemblyName;

                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                DataTable namespaceMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<INamespace>(assembly.ChildNamespaces, namespaceMetricsDefinionsList);
                FillCodeElementsDataGridView(targetNamespacesDataGridView, namespaceMetricsDataTable);
            }
        }

        private void NamespacesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetTypesDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedNamespaceName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);

                List<NDependMetricDefinition> namespaceMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("NamespaceMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], namespaceMetricsDefinionsList);

                targetElementTypeLabel.Text = "Namespace";
                targetElementNameLabel.Text = selectedNamespaceName;

                List<NDependMetricDefinition> typeMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                DataTable typeMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<IType>(nNamespace.ChildTypes, typeMetricsDefinionsList);
                FillCodeElementsDataGridView(targetTypesDataGridView, typeMetricsDataTable);
            }
        }

        private void TypesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetMethodsDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IType nType = codeElementsManager.GetTypeByName(selectedType);

                List<NDependMetricDefinition> typesMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("TypeMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], typesMetricsDefinionsList);

                targetElementTypeLabel.Text = "Type";
                targetElementNameLabel.Text = selectedType;

                List<NDependMetricDefinition> methodMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("MethodMetrics.xml");
                DataTable methodMetricsDataTable = dataTableHelper.CreateCodeElemetMetricsDataTable<IMethod>(nType.MethodsAndContructors, methodMetricsDefinionsList);
                FillCodeElementsDataGridView(targetMethodsDataGridView, methodMetricsDataTable);
            }
        }

        private void MethodsDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedMethod = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                List<NDependMetricDefinition> methodsMetricsDefinionsList = new NDependXMLMetricsDefinitionLoader().LoadMetricsDefinitions("MethodMetrics.xml");
                FillMetricsListView(targetMetricsListView, senderDataGridView.SelectedRows[0], methodsMetricsDefinionsList);

                targetElementTypeLabel.Text = "Method";
                targetElementNameLabel.Text = selectedMethod;
            }
        }

        private void lvwCodeMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager(this.lvwCodeMetricsList, this.rtfCodeMetricProperties);
        }

        private void lvwUnitTestsMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager(this.lvwUnitTestsMetricsList, this.rtfUnitTestsMetricProperties);
        }

        private void lvwBDDMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager(this.lvwBDDMetricsList, this.rtfBDDMetricProperties);
        }

        private void ListViewSelectedIndexEventManager(ListView senderListView, RichTextBox targetRichTextBox)
        {
            if (senderListView.SelectedItems.Count > 0)
            {
                ListViewItem lvi = senderListView.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                FillMetricDescriptionRTFBox(targetRichTextBox, nDependMetricDefinition);
            }
        }

        private void lvwCodeMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedCodeElementName = this.lblCodeElementName.Text;
            ListViewMouseDoubleClickEventManager(this.lvwCodeMetricsList, selectedCodeElementName);
        }

        private void lvwUnitTestsMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedCodeElementName = this.lblUnitTestsCodeElementName.Text;
            ListViewMouseDoubleClickEventManager(this.lvwUnitTestsMetricsList, selectedCodeElementName);
        }

        private void lvwBDDMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string selectedCodeElementName = this.lblBDDCodeElementName.Text;
            ListViewMouseDoubleClickEventManager(this.lvwBDDMetricsList, selectedCodeElementName);
        }

        private void ListViewMouseDoubleClickEventManager(ListView senderListView, string codeElementName)
        {
            if (senderListView.SelectedItems.Count > 0)
            {
                ListViewItem lvi = senderListView.SelectedItems[0];
                NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                DataGridView sourceDataGridView = (DataGridView)senderListView.Tag;
                DataTable metricsDataTable = (DataTable)sourceDataGridView.DataSource;
                DataGridViewTagInfo dataGridViewTagInfo = ((DataGridViewTagInfo)sourceDataGridView.Tag);
                string parentCodeElementName = 
                    dataGridViewTagInfo.LinkedDataGrids.ParentDataGridView==null ? 
                    String.Empty : dataGridViewTagInfo.LinkedDataGrids.ParentDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                DataGridView assembliesDatagrid = GetAssebliesDataGridView(sourceDataGridView);
                string assemblyName = assembliesDatagrid.SelectedRows[0].Cells[0].Value.ToString();
                MetricProperties metricProperties = new MetricProperties(
                    nDependMetricDefinition,
                    codeElementName,
                    metricsDataTable,
                    parentCodeElementName,
                    assemblyName,
                    nDependProject);

                metricProperties.Show();
            }
        }

        private void lvwCodeMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager(this.lvwCodeMetricsList, e);
        }

        private void lvwUnitTestsMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager(this.lvwUnitTestsMetricsList, e);
        }

        private void lvwBDDMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager(this.lvwBDDMetricsList, e);
        }

        private void ListViewmCheckEventManager(ListView senderListView, ItemCheckEventArgs eventArguments)
        {
            if (inhibitAutocheckOnDoubleClick)
            {
                eventArguments.NewValue = eventArguments.CurrentValue;
            }
            else
            {
                DataGridView sourceDataGridView = (DataGridView)senderListView.Tag;
                sourceDataGridView.Columns[senderListView.Items[eventArguments.Index].Text].Visible = (eventArguments.NewValue == CheckState.Checked);
            }
        }

        private void lvwCodeMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwCodeMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void lvwUnitTestsMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwUnitTestsMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void lvwBDDMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwBDDMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenNdependProject();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            UserDefinedMetrics userDefinedMetrics = new UserDefinedMetrics(lastAnalysisCodebase);
            userDefinedMetrics.CheckStringCodeQuery(lastAnalysisCodebase);*/
            //userDefinedMetrics.GetDistribition(lastAnalysisCodebase, "");

            /*
            List<int> list = new List<int> { 1, 1, 2, 4, 4, 4, 5, 7, 7, 7, 9, 10 };
            Dictionary <int,int> frequenceList = Statistics.FrequencesList(list);*/

        }


        class DataGridViewTagInfo
        {
            Type codeElementsType;
            LinkedDatagrids linkedDataGrids;

            public DataGridViewTagInfo(Type codeElementsType, LinkedDatagrids linkedDataGrids)
            {
                this.codeElementsType = codeElementsType;
                this.linkedDataGrids = linkedDataGrids;
            }

            public Type CodeElementsType
            {
                get { return codeElementsType; }
            }

            public LinkedDatagrids LinkedDataGrids
            {
                get { return linkedDataGrids; }
            }
        }
        
        
        class LinkedDatagrids
        {
            DataGridView parentDataGridView;
            DataGridView childDataGridView;

            public LinkedDatagrids(DataGridView parent, DataGridView child)
            {
                this.parentDataGridView = parent;
                this.childDataGridView = child;
            }

            public DataGridView ParentDataGridView
            {
                get{return parentDataGridView;}
            }

            public DataGridView ChildDataGridView
            {
                get { return childDataGridView; }
            }
        }
    }
}
