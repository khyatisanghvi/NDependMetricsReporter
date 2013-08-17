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
    class AnalysisHistoryManager
    {
        List<IAnalysisResultRef> analysisResultRefsList;
        public AnalysisHistoryManager(string nDpendProjectpath)
        {
            NDependServicesProvider nDependServicesProvider = new NDependServicesProvider();
            var projectManager = nDependServicesProvider.ProjectManager;
            IAbsoluteFilePath pathToNDependProject = PathHelpers.ToAbsoluteFilePath(nDpendProjectpath);
            IProject nDependProjet = projectManager.LoadProject(pathToNDependProject);
            ICollection<IAnalysisResultRef> analysisResultRefs = nDependProjet.GetAvailableAnalysisResultsRefs();
            analysisResultRefsList = analysisResultRefs.OrderBy(analysisResultRef => analysisResultRef.Date).ToList();
        }

        public AnalysisHistoryManager(IProject nDependProject)
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
            ReflectionHelper reflectionHelper= new ReflectionHelper();

            foreach (var m in analysisResultRefsList)
            {
                try
                {
                    IAnalysisResult currentAnalysisResult = m.Load();
                    ICodeBase currentAnalysisResultCodeBase = currentAnalysisResult.CodeBase;
                    CodeElementsManager currenAnalysisResultCodeBaseManager =  new CodeElementsManager(currentAnalysisResultCodeBase);
                    string codeElementName = "";

                    switch (codeElementType)
                    {
                        case "NDepend.CodeModel.IAssembly":
                            string assemblyName = ((IAssembly)codeElement).Name;
                            IAssembly selectedAssemblyFromCurrentAnalysisResultCodebase = currenAnalysisResultCodeBaseManager.GetAssemblyByName(assemblyName);
                            if (selectedAssemblyFromCurrentAnalysisResultCodebase != null)
                                metricValues.Add(reflectionHelper.GetCodeElementMetric(
                                    selectedAssemblyFromCurrentAnalysisResultCodebase,
                                    typeof(IAssembly),
                                    metricDefinition.InternalPropertyName,
                                    metricDefinition.NDependMetricType));
                            break;
                        case "NDepend.CodeModel.INamespace":
                            string namespaceName = ((INamespace)codeElement).Name;
                            INamespace selectedNamespaceFromCurrentAnalysisResultCodebase = currenAnalysisResultCodeBaseManager.GetNamespaceByName(namespaceName);
                            if (selectedNamespaceFromCurrentAnalysisResultCodebase != null)
                                metricValues.Add(reflectionHelper.GetCodeElementMetric(
                                    selectedNamespaceFromCurrentAnalysisResultCodebase,
                                    typeof(INamespace),
                                    metricDefinition.InternalPropertyName,
                                    metricDefinition.NDependMetricType));
                            /*codeElementName = ((INamespace)codeElement).Name;
                            IEnumerable<INamespace> selectedNamespaces = currentAnalysisResultCodeBase.Application.Namespaces.Where(a => a.Name == codeElementName);
                            if (!selectedNamespaces.Any()) break;
                            INamespace selectedNamespace = selectedNamespaces.First();
                            metricValues.Add(selectedNamespace.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedNamespace));*/
                            break;
                        case "NDepend.CodeModel.IType":
                            codeElementName = ((IType)codeElement).Name;
                            IEnumerable<IType> selectedTypes = currentAnalysisResultCodeBase.Application.Types.Where(a => a.Name == codeElementName);
                            if (!selectedTypes.Any()) break;
                            IType selectedType = selectedTypes.First();
                            metricValues.Add(selectedType.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedType));
                            break;
                        case "NDepend.CodeModel.IMethod":
                            codeElementName = ((IMethod)codeElement).Name;
                            IEnumerable<IMethod> selectedMethods = currentAnalysisResultCodeBase.Application.Methods.Where(a => a.Name == codeElementName);
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
