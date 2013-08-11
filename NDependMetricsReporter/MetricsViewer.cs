using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDepend;
using NDepend.Path;
using NDepend.Analysis;
using NDepend.Project;
using NDepend.CodeModel;
using NDepend.PowerTools;

namespace NDependMetricsReporter
{
    public partial class MetricsViewer : Form
    {
        NDependServicesProvider nDependServicesProvider;
        IProject nDependProject;
        ICodeBase lastAnalysisCodebase;

        public MetricsViewer()
        {
            InitializeComponent();

            nDependServicesProvider = new NDependServicesProvider();
        }

        private void FillBaseControls()
        {
            this.tboxProjectName.Text = nDependProject.Properties.Name;
            IEnumerable<IAssembly> lastAnalysisAssembliesList = lastAnalysisCodebase.Assemblies;
            FillAssembliesListView(lastAnalysisAssembliesList);
        }

        private void FillAssembliesListView(IEnumerable<IAssembly> assembliesList)
        {
            lvwAssembliesList.Items.Clear();
            foreach (IAssembly assembly in assembliesList)
            {
                if (assembly.IsThirdParty) continue;
                ListViewItem lvi = new ListViewItem(new string[] { assembly.Name, assembly.FilePath.ToString() });
                this.lvwAssembliesList.Items.Add(lvi);
            }
        }

        private void FillNamespacesListView(IEnumerable<INamespace> namespacesList)
        {
            this.lvwNamespacesList.Items.Clear(); ;
            foreach (INamespace nspc in namespacesList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { nspc.Name });
                this.lvwNamespacesList.Items.Add(lvi);
            }
        }

        private void FillTypesListView(IEnumerable<IType> typesList)
        {
            this.lvwTypesList.Items.Clear(); ;
            foreach (IType tp in typesList)
            {
                ListViewItem lvi = new ListViewItem(new string[] { tp.Name });
                this.lvwNamespacesList.Items.Add(lvi);
            }
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            nDependServicesProvider.ProjectManager.ShowDialogChooseAnExistingProject(out nDependProject);
            this.lastAnalysisCodebase = new NDependCodeBaseManager(nDependProject).LoadLastCodebase();
            FillBaseControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NDependCodeBaseManager codebaseLoader = new NDependCodeBaseManager(nDependProject);
            ICodeBase codeBase = codebaseLoader.LoadLastCodebase();
            IAssembly myAssembly = codeBase.Application.Assemblies.ElementAt<IAssembly>(0);


            //var allNameSpaces = (from n in codeBase.Application.Namespaces select n).ToArray();
            var allPropertiesInGivenAssembly = codeBase.Application.Assemblies.ElementAt<IAssembly>(0).GetType().GetProperties();
            string propertyName = allPropertiesInGivenAssembly[0].Name;

            /*foreach (var t in allTypesInGiveNamespace)
            {
                this.lboxNamespacesList.Items.Add(t.Name);
            }

            foreach (var m in allNameSpaces)
            {
                this.lboxNamespacesList.Items.Add(m.Name);
            }
            var complexMethods = (from m in codeBase.Application.Methods 
                               where m.ILCyclomaticComplexity > 10 
                               orderby m.ILCyclomaticComplexity descending 
                               select m).ToArray();*/

        }

        private void btnHistoryChart_Click(object sender, EventArgs e)
        {
            NDependAnalysisHistoryManager nDependAnalisysHistoryManager = new NDependAnalysisHistoryManager(nDependProject);
            List<uint> nbLinesOfCodeList = nDependAnalisysHistoryManager.GetAssemblyMetricHistory<uint>("", "NbLinesOfCode"); 
            

            this.chrtLineChart.DataSource = new BindingList<uint>(nbLinesOfCodeList);
            this.chrtLineChart.Series["Lines Of Code"].YValueMembers = "Y";
            this.chrtLineChart.DataBind();
            this.chrtLineChart.Update();
            
        }

        private void lvwAssembliesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAssemblyName = this.lvwAssembliesList.SelectedItems[0].Text;
            var namespacesList = new NDependCodeElementsManager(lastAnalysisCodebase).GetAssemblyByName(selectedAssemblyName).ChildNamespaces;
            FillNamespacesListView(namespacesList); 
        }

        private void lvwNamespacesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedNamespaceName = this.lvwNamespacesList.SelectedItems[0].Text;
            var typesList = new NDependCodeElementsManager(lastAnalysisCodebase).GetNamespaceByName(selectedNamespaceName).ChildTypes;
            FillTypesListView(typesList); 

        }

        private void lvwTypesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAssemblyName = lvwAssembliesList.SelectedItems[0].Text;
           // var namespacesList = new NDependCodeElementsManager(lastAnalysisCodebase).GetNamespacesInAssembly(selectedAssemblyName);
            //FillNamespacesListView(namespacesList); 
        }




    }
}
