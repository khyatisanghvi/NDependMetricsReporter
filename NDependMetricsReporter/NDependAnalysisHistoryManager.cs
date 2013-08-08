using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend;
using NDepend.Path;
using NDepend.Analysis;
using NDepend.Project;
using NDepend.CodeModel;
using NDepend.PowerTools;

namespace NDependMetricsReporter
{
    class NDependAnalysisHistoryManager
    {
        List<IAnalysisResultRef> analysisResultRefsList;
        public NDependAnalysisHistoryManager(string nDpendProjectpath)
        {
            NDependServicesProvider nDependServicesProvider = new NDependServicesProvider();
            var projectManager = nDependServicesProvider.ProjectManager;
            IAbsoluteFilePath pathToNDependProject = PathHelpers.ToAbsoluteFilePath(nDpendProjectpath);
            IProject nDependProjet = projectManager.LoadProject(pathToNDependProject);
            ICollection<IAnalysisResultRef> analysisResultRefs = nDependProjet.GetAvailableAnalysisResultsRefs();
            analysisResultRefsList = analysisResultRefs.OrderBy(analysisResultRef => analysisResultRef.Date).ToList();
        }

        public NDependAnalysisHistoryManager(IProject nDependProject)
        {
            ICollection<IAnalysisResultRef> analysisResultRefs = nDependProject.GetAvailableAnalysisResultsRefs();
            analysisResultRefsList = analysisResultRefs.OrderBy(analysisResultRef => analysisResultRef.Date).ToList();
        }

        public List<IAnalysisResultRef> AnalysisResultRefsList
        {
            get { return analysisResultRefsList; }
        }

        public List<T> GetAssemblyMetricHistory<T>(string assemblyName, string metricName)
        {
            List<T> metricsList = new List<T>();

            foreach (var m in analysisResultRefsList)
            {
                try
                {
                    IAnalysisResult analisysResult = m.Load();
                    ICodeBase codeBase = analisysResult.CodeBase;
                    IAssembly selectedAssembly = codeBase.Application.Assemblies.ElementAt<IAssembly>(0);
                    metricsList.Add((T)selectedAssembly.GetType().GetProperty(metricName).GetValue(selectedAssembly));
                }
                catch (AnalysisException analysisException)
                {
                    string exceptionString = analysisException.ToString();
                }
            }
            return metricsList;
        }


    }
}
