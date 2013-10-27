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
        IProject nDependProject;

        CodeElementsManager codeElementsManager;
        UserDefinedMetrics userDefinedMetrics;

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
            userDefinedMetrics = new UserDefinedMetrics(lastAnalysisCodebase);
            dataTableHelper = new DataTableHelper(codeElementsManager, userDefinedMetrics);
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
            IEnumerable<IAssembly> lastAnalysisAssembliesList = codeElementsManager.GetNonThirdPartyAssembliesInApplication();
            List<NDependMetricDefinition> nDependAssemblyMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("AssemblyMetrics.xml");
            List<UserDefinedMetricDefinition> userDefinedAssemblyMetricsDefinitionList = new List<UserDefinedMetricDefinition>();
            DataTable assemblyMetricsDataTable = dataTableHelper.CreateCodeElementMetricsDataTable<IAssembly>(lastAnalysisAssembliesList, nDependAssemblyMetricsDefinionsList, userDefinedAssemblyMetricsDefinitionList);
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
            FillCodeElementsDataGridView(this.dgvCodeAssemblies, onlyCodeAsemblies, true);
        }

        private void FillTestAssembliesDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] LIKE '*UnitTest*')";
            DataTable onlyTestAssemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvUnitTestsAssemblies, onlyTestAssemblies, true);
        }

        private void FillSpecFlowBDDAssembliesDataGridView(DataTable assemblyMetricsDataTable)
        {
            string rowSelectionFiler = "([Code Element] LIKE '*SpecFlowBDD*')";
            DataTable onlySpecFlowBDDAssemblies = assemblyMetricsDataTable.CloneSelection(rowSelectionFiler);
            FillCodeElementsDataGridView(this.dgvBDDAssemblies, onlySpecFlowBDDAssemblies, true);
        }

        private void FillCodeElementsDataGridView(DataGridView codeElementMetricsDataGridView, DataTable codeElementMetricsDataTable, bool freezeCodeElementsNameColumn)
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
                codeElementMetricsDataGridView.Columns[0].Frozen = freezeCodeElementsNameColumn;
            }
        }

        private void FillMetricsListView(
            ListView targetNDependMetricsListview,
            ListView targetUserDefinedMetricsListview,
            DataGridViewRow selectedDataGridViewRow,
            List<NDependMetricDefinition> nDependMetricDefinitionsList,
            List<UserDefinedMetricDefinition> userDefinedMetricsDefinitionsList)
        {
            string formatSpecifier = "0.####";
            DataGridView sourceDataGridView = selectedDataGridViewRow.DataGridView;
 
            targetNDependMetricsListview.Tag = sourceDataGridView;
            targetNDependMetricsListview.Items.Clear();
            foreach (NDependMetricDefinition metricDefinition in nDependMetricDefinitionsList)
            {
                double cellValue = Double.Parse(selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Value.ToString());
                string formatedCellValue = cellValue.ToString(formatSpecifier);
                ListViewItem lvi = new ListViewItem(new[] { metricDefinition.PropertyName, formatedCellValue });
                lvi.Tag = metricDefinition;
                lvi.Checked = selectedDataGridViewRow.Cells[metricDefinition.PropertyName].Displayed;
                targetNDependMetricsListview.Items.Add(lvi);
            }

            targetUserDefinedMetricsListview.Tag = sourceDataGridView;
            targetUserDefinedMetricsListview.Items.Clear();
            foreach (UserDefinedMetricDefinition userDefinedMetricDefinition in userDefinedMetricsDefinitionsList)
            {
                double cellValue = Double.Parse(selectedDataGridViewRow.Cells[userDefinedMetricDefinition.ResumedMetricName].Value.ToString());
                string formatedCellValue = cellValue.ToString(formatSpecifier);
                ListViewItem lvi = new ListViewItem(new[] { userDefinedMetricDefinition.ResumedMetricName, formatedCellValue });
                lvi.Tag = userDefinedMetricDefinition;
                lvi.Checked = selectedDataGridViewRow.Cells[userDefinedMetricDefinition.ResumedMetricName].Displayed;
                targetUserDefinedMetricsListview.Items.Add(lvi);
            }

        }

        private void FillMetricDescriptionRTFBox(RichTextBox targetRichBox, string metricname, string metricDefinition)
        {
            targetRichBox.Clear();
            targetRichBox.AppendText(metricname);
            targetRichBox.Find(metricname);
            targetRichBox.SelectionFont = new Font(rtfCodeMetricProperties.Font, rtfCodeMetricProperties.Font.Style ^ FontStyle.Bold);
            targetRichBox.SelectionStart = this.rtfCodeMetricProperties.Text.Length;
            targetRichBox.SelectionLength = 0;
            targetRichBox.SelectionFont = rtfCodeMetricProperties.Font;
            targetRichBox.AppendText(Environment.NewLine + metricDefinition);
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

        private void dgvCodeNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeTypes, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvCodeTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvCodeMethods, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvCodeMethods_SelectionChanged(object sender, EventArgs e)
        {
            MethodsDataGridViewSelectionChangedEventManager((DataGridView)sender, lvwCodeMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsAssemblies_SelectionChanged(object sender, EventArgs e)
        {
            AssembliesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsNamespaces, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvUnitTestsNamespaces_SelectionChanged(object sender, EventArgs e)
        {
            NamespacesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsTypes, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvUnitTestsTypes_SelectionChanged(object sender, EventArgs e)
        {
            TypesDataGridViewSelectionChangedEventManager((DataGridView)sender, dgvUnitTestsMethods, lvwUnitTestsMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
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

                List<NDependMetricDefinition> nDependNamespaceMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("NamespaceMetrics.xml");
                List<UserDefinedMetricDefinition> userDefinedNamespaceMetricsDefinitionList = new List<UserDefinedMetricDefinition>();
                DataTable namespaceMetricsDataTable = dataTableHelper.CreateCodeElementMetricsDataTable<INamespace>(assembly.ChildNamespaces, nDependNamespaceMetricsDefinionsList, userDefinedNamespaceMetricsDefinitionList);
                FillCodeElementsDataGridView(targetNamespacesDataGridView, namespaceMetricsDataTable, true);
            }
        }

        private void NamespacesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetTypesDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedNamespaceName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                INamespace nNamespace = codeElementsManager.GetNamespaceByName(selectedNamespaceName);

                List<NDependMetricDefinition> nDependTypeMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("TypeMetrics.xml");
                List<UserDefinedMetricDefinition> userDefinedTypeMetricsDefinitionList = new List<UserDefinedMetricDefinition>();
                DataTable typeMetricsDataTable = dataTableHelper.CreateCodeElementMetricsDataTable<IType>(nNamespace.ChildTypes, nDependTypeMetricsDefinionsList, userDefinedTypeMetricsDefinitionList);
                FillCodeElementsDataGridView(targetTypesDataGridView, typeMetricsDataTable, true);
            }
        }

        private void TypesDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, DataGridView targetMethodsDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                IType nType = codeElementsManager.GetTypeByName(selectedType);

                List<NDependMetricDefinition> nDependMethodMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("MethodMetrics.xml");
                List<UserDefinedMetricDefinition> userDefinedMethodMetricsDefinitionList = new XMLMetricsDefinitionLoader().LoadUserDefinedMetricsDefinitions("MethodUserDefinedMetrics.xml");
                DataTable methodMetricsDataTable = dataTableHelper.CreateCodeElementMetricsDataTable<IMethod>(nType.MethodsAndContructors, nDependMethodMetricsDefinionsList, userDefinedMethodMetricsDefinitionList);
                bool isTestMethodsDataViewGrid = targetMethodsDataGridView.Name == "dgvUnitTestsMethods" || targetMethodsDataGridView.Name == "dgvBDDMethods";
                FillCodeElementsDataGridView(targetMethodsDataGridView, methodMetricsDataTable, !isTestMethodsDataViewGrid);
            }
        }

        private void MethodsDataGridViewSelectionChangedEventManager(DataGridView senderDataGridView, ListView targetMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {

        }

        private void dgvCodeAssemblies_Click(object sender, EventArgs e)
        {
            AssembliesDataGridViewClickEventManager((DataGridView)sender, lvwCodeMetricsList, lvwCodeUserDefinedMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvCodeNamespaces_Click(object sender, EventArgs e)
        {
            NamespacesDataGridViewClickEventManager((DataGridView)sender, lvwCodeMetricsList, lvwCodeUserDefinedMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvCodeTypes_Click(object sender, EventArgs e)
        {
            TypesDataGridViewClickEventManager((DataGridView)sender, lvwCodeMetricsList, lvwCodeUserDefinedMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvCodeMethods_Click(object sender, EventArgs e)
        {
            MethodsDataGridViewClickEventManager((DataGridView)sender, lvwCodeMetricsList, lvwCodeUserDefinedMetricsList, lblCodeElementType, lblCodeElementName);
        }

        private void dgvUnitTestsAssemblies_Click(object sender, EventArgs e)
        {
            AssembliesDataGridViewClickEventManager((DataGridView)sender, lvwUnitTestsMetricsList, lvwUnitTestsUserDefinedMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvUnitTestsNamespaces_Click(object sender, EventArgs e)
        {
            NamespacesDataGridViewClickEventManager((DataGridView)sender, lvwUnitTestsMetricsList, lvwUnitTestsUserDefinedMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvUnitTestsTypes_Click(object sender, EventArgs e)
        {
            TypesDataGridViewClickEventManager((DataGridView)sender, lvwUnitTestsMetricsList, lvwUnitTestsUserDefinedMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvUnitTestsMethods_Click(object sender, EventArgs e)
        {
            MethodsDataGridViewClickEventManager((DataGridView)sender, lvwUnitTestsMetricsList, lvwUnitTestsUserDefinedMetricsList, lblUnitTestsCodeElementType, lblUnitTestsCodeElementName);
        }

        private void dgvBDDAssemblies_Click(object sender, EventArgs e)
        {
            AssembliesDataGridViewClickEventManager((DataGridView)sender, lvwBDDMetricsList, lvwBDDUserDefinedMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void dgvBDDNamespaces_Click(object sender, EventArgs e)
        {
            NamespacesDataGridViewClickEventManager((DataGridView)sender, lvwBDDMetricsList, lvwBDDUserDefinedMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void dgvBDDTypes_Click(object sender, EventArgs e)
        {
            TypesDataGridViewClickEventManager((DataGridView)sender, lvwBDDMetricsList, lvwBDDUserDefinedMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void dgvBDDMethods_Click(object sender, EventArgs e)
        {
            MethodsDataGridViewClickEventManager((DataGridView)sender, lvwBDDMetricsList, lvwBDDUserDefinedMetricsList, lblBDDCodeElementType, lblBDDCodeElementName);
        }

        private void AssembliesDataGridViewClickEventManager(DataGridView senderDataGridView, ListView targetNDependMetricsListView, ListView targetUserDefinedMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedAssemblyName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                List<NDependMetricDefinition> assemblyNDependMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("AssemblyMetrics.xml");
                List<UserDefinedMetricDefinition> assemblyUserDefinedMetricsDefinitionsList = new List<UserDefinedMetricDefinition>();
                FillMetricsListView(targetNDependMetricsListView, targetUserDefinedMetricsListView, senderDataGridView.SelectedRows[0], assemblyNDependMetricsDefinionsList, assemblyUserDefinedMetricsDefinitionsList);

                targetElementTypeLabel.Text = "Assembly";
                targetElementNameLabel.Text = selectedAssemblyName;
            }
        }

        private void NamespacesDataGridViewClickEventManager(DataGridView senderDataGridView, ListView targetNDependMetricsListView, ListView targetUserDefinedMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedNamespaceName = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                List<NDependMetricDefinition> namespaceNDependMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("NamespaceMetrics.xml");
                List<UserDefinedMetricDefinition> namespaceUserDefinedMetricsDefinitionsList = new List<UserDefinedMetricDefinition>();
                FillMetricsListView(targetNDependMetricsListView, targetUserDefinedMetricsListView, senderDataGridView.SelectedRows[0], namespaceNDependMetricsDefinionsList, namespaceUserDefinedMetricsDefinitionsList);

                targetElementTypeLabel.Text = "Namespace";
                targetElementNameLabel.Text = selectedNamespaceName;
            }
        }

        private void TypesDataGridViewClickEventManager(DataGridView senderDataGridView, ListView targetNDependMetricsListView, ListView targetUserDefinedMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedType = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                List<NDependMetricDefinition> typesNDependMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("TypeMetrics.xml");
                List<UserDefinedMetricDefinition> typesUserDefinedMetricsDefinitionsList = new List<UserDefinedMetricDefinition>();
                FillMetricsListView(targetNDependMetricsListView, targetUserDefinedMetricsListView, senderDataGridView.SelectedRows[0], typesNDependMetricsDefinionsList, typesUserDefinedMetricsDefinitionsList);

                targetElementTypeLabel.Text = "Type";
                targetElementNameLabel.Text = selectedType;
            }
        }

        private void MethodsDataGridViewClickEventManager(DataGridView senderDataGridView, ListView targetNDependMetricsListView, ListView targetUserDefinedMetricsListView, Label targetElementTypeLabel, Label targetElementNameLabel)
        {
            if (senderDataGridView.SelectedRows.Count > 0)
            {
                string selectedMethod = senderDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                List<NDependMetricDefinition> methodsNDependMetricsDefinionsList = new XMLMetricsDefinitionLoader().LoadNDependMetricsDefinitions("MethodMetrics.xml");
                List<UserDefinedMetricDefinition> methodsUserDefinedMetricsDefinitionsList = new XMLMetricsDefinitionLoader().LoadUserDefinedMetricsDefinitions("MethodUserDefinedMetrics.xml");
                FillMetricsListView(targetNDependMetricsListView, targetUserDefinedMetricsListView, senderDataGridView.SelectedRows[0], methodsNDependMetricsDefinionsList, methodsUserDefinedMetricsDefinitionsList);

                targetElementTypeLabel.Text = "Method";
                targetElementNameLabel.Text = selectedMethod;
            }
        }

        private void lvwCodeMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager((ListView)sender, rtfCodeMetricProperties);
        }

        private void lvwUnitTestsMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager((ListView)sender, rtfUnitTestsMetricProperties);
        }

        private void lvwBDDMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager((ListView)sender, rtfBDDMetricProperties);
        }

        private void lvwCodeUserDefinedMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager((ListView)sender, rtfCodeMetricProperties);
        }

        private void lvwUnitTestsUserDefinedMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager((ListView)sender, rtfUnitTestsMetricProperties);
        }

        private void lvwBDDUserDefinedMetricsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexEventManager((ListView)sender, rtfBDDMetricProperties);
        }

        private void ListViewSelectedIndexEventManager(ListView senderListView, RichTextBox targetRichTextBox)
        {
            string metricName="";
            string metricDescription="";
            if (senderListView.SelectedItems.Count > 0)
            {
                object lviTag = senderListView.SelectedItems[0].Tag;
                if (lviTag.GetType()==typeof(UserDefinedMetricDefinition))
                {
                    metricName = ((UserDefinedMetricDefinition)lviTag).MetricName;
                    metricDescription = ((UserDefinedMetricDefinition)lviTag).Description;
                }
                else
                {
                    metricName = ((NDependMetricDefinition)lviTag).MetricName;
                    metricDescription = ((NDependMetricDefinition)lviTag).Description;                    
                }
                FillMetricDescriptionRTFBox(targetRichTextBox, metricName, metricDescription);                
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

        private void lvwCodeUserDefinedMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwCodeUserDefinedMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void lvwUnitTestsUserDefinedMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwUnitTestsUserDefinedMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void lvwBDDUserDefinedMetricsList_MouseDown(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = true;
        }

        private void lvwBDDUserDefinedMetricsList_MouseUp(object sender, MouseEventArgs e)
        {
            inhibitAutocheckOnDoubleClick = false;
        }

        private void lvwCodeMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager((ListView)sender, e);
        }

        private void lvwUnitTestsMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager((ListView)sender, e);
        }

        private void lvwBDDMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager((ListView)sender, e);
        }

        private void lvwCodeUserDefinedMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager((ListView)sender, e);
        }

        private void lvwUnitTestsUserDefinedMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager((ListView)sender, e);
        }

        private void lvwBDDUserDefinedMetricsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewmCheckEventManager((ListView)sender, e);
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

        private void lvwCodeMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMouseDoubleClickEventManager((ListView)sender);
        }

        private void lvwUnitTestsMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMouseDoubleClickEventManager((ListView)sender);
        }

        private void lvwBDDMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMouseDoubleClickEventManager((ListView)sender);
        }

        private void lvwCodeUserDefinedMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMouseDoubleClickEventManager((ListView)sender);
        }

        private void lvwUnitTestsUserDefinedMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMouseDoubleClickEventManager((ListView)sender);
        }

        private void lvwBDDUserDefinedMetricsList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewMouseDoubleClickEventManager((ListView)sender);
        }

        private void ListViewMouseDoubleClickEventManager(ListView senderListView)
        {
            if (senderListView.SelectedItems.Count > 0)
            {
                MetricProperties metricProperties;

                DataGridView sourceDataGridView = (DataGridView)senderListView.Tag;
                string codeElementName = sourceDataGridView.SelectedRows[0].Cells[0].Value.ToString();
                DataTable metricsDataTable = (DataTable)sourceDataGridView.DataSource;
                DataGridViewTagInfo dataGridViewTagInfo = ((DataGridViewTagInfo)sourceDataGridView.Tag);
                DataGridView assembliesDatagrid = GetAssebliesDataGridView(sourceDataGridView);
                string assemblyName = assembliesDatagrid.SelectedRows[0].Cells[0].Value.ToString();
                string parentCodeElementName = dataGridViewTagInfo.LinkedDataGrids.ParentDataGridView == null ?
                    ((IAssembly)codeElementsManager.CodeBase.Assemblies.WithName(assemblyName).First()).VisualStudioProjectFilePath.FileName :
                    dataGridViewTagInfo.LinkedDataGrids.ParentDataGridView.SelectedRows[0].Cells[0].Value.ToString();

                ListViewItem lvi = senderListView.SelectedItems[0];
                if (lvi.Tag.GetType() == typeof(NDependMetricDefinition))
                {
                    NDependMetricDefinition nDependMetricDefinition = (NDependMetricDefinition)lvi.Tag;
                    metricProperties = new MetricProperties(
                        nDependMetricDefinition,
                        codeElementName,
                        metricsDataTable,
                        parentCodeElementName,
                        assemblyName,
                        nDependProject);        
                }
                else
                {                   
                    UserDefinedMetricDefinition userDefinedMetricDefinition = (UserDefinedMetricDefinition)lvi.Tag;
                    metricProperties = new MetricProperties(
                        userDefinedMetricDefinition,
                        codeElementName,
                        metricsDataTable,
                        parentCodeElementName,
                        assemblyName,
                        nDependProject);
                }

                metricProperties.Show();
            }
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

            /*ICodeBase lastAnalysisCodebase = new CodeBaseManager(nDependProject).LoadLastCodebase();
            UserDefinedMetrics userDefinedMetrics = new UserDefinedMetrics(lastAnalysisCodebase);
            userDefinedMetrics.CountAppLogicMethodsCalled("InvoicesAcceptTransactionsWithZeroCost()");*/

            XMLMetricsDefinitionLoader xMLdefinitionLoader = new XMLMetricsDefinitionLoader();
            List<UserDefinedMetricDefinition> userDefinedMetricsList = xMLdefinitionLoader.LoadUserDefinedMetricsDefinitions("MethodUserDefinedMetrics.xml");
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
                get { return parentDataGridView; }
            }

            public DataGridView ChildDataGridView
            {
                get { return childDataGridView; }
            }
        }
    }
}
