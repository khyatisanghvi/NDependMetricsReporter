using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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

        public IList GetMetricHistory(object codeElement, NDependMetricDefinition metricDefinition)
        {
            string codeElementType = metricDefinition.NDependCodeElementType;
            string metricInternalPorpertyName = metricDefinition.InternalPropertyName;
            Type metricType = Type.GetType(metricDefinition.NDependMetricType);
            IList metricValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(metricType));

            foreach (var m in analysisResultRefsList)
            {
                try
                {
                    IAnalysisResult analisysResult = m.Load();
                    ICodeBase codeBase = analisysResult.CodeBase;
                    string codeElementName = "";

                    switch (codeElementType)
                    {
                        case "NDepend.CodeModel.IAssembly":
                            codeElementName = ((IAssembly)codeElement).Name;
                            IEnumerable<IAssembly> selectedAssemblies = codeBase.Application.Assemblies.Where(a => a.Name == codeElementName);
                            if (!selectedAssemblies.Any()) break;
                            IAssembly selectedAssembly = selectedAssemblies.First();
                            metricValues.Add(selectedAssembly.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedAssembly));
                            break;
                        case "NDepend.CodeModel.INamespace":
                            codeElementName = ((INamespace)codeElement).Name;
                            IEnumerable<INamespace> selectedNamespaces = codeBase.Application.Namespaces.Where(a => a.Name == codeElementName);
                            if (!selectedNamespaces.Any()) break;
                            INamespace selectedNamespace = selectedNamespaces.First();
                            metricValues.Add(selectedNamespace.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedNamespace));
                            break;
                        case "NDepend.CodeModel.IType":
                            codeElementName = ((IType)codeElement).Name;
                            IEnumerable<IType> selectedTypes = codeBase.Application.Types.Where(a => a.Name == codeElementName);
                            if (!selectedTypes.Any()) break;
                            IType selectedType = selectedTypes.First();
                            metricValues.Add(selectedType.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedType));
                            break;
                        case "NDepend.CodeModel.IMethod":
                            codeElementName = ((IMethod)codeElement).Name;
                            IEnumerable<IMethod> selectedMethods = codeBase.Application.Methods.Where(a => a.Name == codeElementName);
                            if (!selectedMethods.Any()) break;
                            IMethod selectedMethod = selectedMethods.First();
                            metricValues.Add(selectedMethod.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedMethod));
                            break;
                    }
                }
                catch (AnalysisException analysisException)
                {
                    string exceptionString = analysisException.ToString();
                }
            }
            return metricValues;
        }

    }
}
