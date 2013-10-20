using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDepend;
using NDepend.Path;
using NDepend.Analysis;
using NDepend.Project;
using NDepend.CodeModel;


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
            IProject nDependProject = projectManager.LoadProject(pathToNDependProject);
            ICollection<IAnalysisResultRef> analysisResultRefs = nDependProject.GetAvailableAnalysisResultsRefs();
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

        public IList GetMetricHistory(string codeElementName, NDependMetricDefinition metricDefinition)
        {
            CodeBaseManager codeBaseManager = new CodeBaseManager(analysisResultRefsList[0].Project);
            CodeElementsManagerReflectionHelper reflectionHelper = new CodeElementsManagerReflectionHelper();
             
            Type metricType = Type.GetType(metricDefinition.NDependMetricType);
            Type nullableMetricType = typeof(Nullable<>).MakeGenericType(metricType);
            var metricValue = Activator.CreateInstance(nullableMetricType);
            IList metricValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(nullableMetricType));

            foreach (var analysisResultRef in analysisResultRefsList)
            {
                ICodeBase currentAnalysisResultCodeBase = codeBaseManager.LoadCodeBase(analysisResultRef);
                CodeElementsManager currenAnalysisResultCodeBaseManager = new CodeElementsManager(currentAnalysisResultCodeBase);
                metricValue = null;
                switch (metricDefinition.NDependCodeElementType)
                {
                    case "NDepend.CodeModel.IAssembly":
                        IAssembly selectedAssemblyFromCurrentAnalysisResultCodebase = currenAnalysisResultCodeBaseManager.GetAssemblyByName(codeElementName);
                        if (selectedAssemblyFromCurrentAnalysisResultCodebase != null)
                            /*metricValue = reflectionHelper.GetCodeElementMetric(
                                selectedAssemblyFromCurrentAnalysisResultCodebase,
                                typeof(IAssembly),
                                metricDefinition.InternalPropertyName,
                                metricDefinition.NDependMetricType);*/
                            metricValue = currenAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedAssemblyFromCurrentAnalysisResultCodebase, typeof(IAssembly), metricDefinition);
                        break;
                    case "NDepend.CodeModel.INamespace":
                        INamespace selectedNamespaceFromCurrentAnalysisResultCodebase = currenAnalysisResultCodeBaseManager.GetNamespaceByName(codeElementName);
                        if (selectedNamespaceFromCurrentAnalysisResultCodebase != null)
                            /*metricValue = reflectionHelper.GetCodeElementMetric(
                                selectedNamespaceFromCurrentAnalysisResultCodebase,
                                typeof(INamespace),
                                metricDefinition.InternalPropertyName,
                                metricDefinition.NDependMetricType);*/
                            metricValue = currenAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedNamespaceFromCurrentAnalysisResultCodebase,typeof(INamespace), metricDefinition);
                        break;
                    case "NDepend.CodeModel.IType":
                        IType selectedTypeFromCurrentAnalysisResultCodebase = currenAnalysisResultCodeBaseManager.GetTypeByName(codeElementName);
                        if (selectedTypeFromCurrentAnalysisResultCodebase != null)
                            /*metricValue = reflectionHelper.GetCodeElementMetric(
                                selectedTypeFromCurrentAnalysisResultCodebase,
                                typeof(IType),
                                metricDefinition.InternalPropertyName,
                                metricDefinition.NDependMetricType);*/
                            metricValue = currenAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedTypeFromCurrentAnalysisResultCodebase, typeof(IType), metricDefinition);
                        break;
                    case "NDepend.CodeModel.IMethod":
                        IMethod selectedMethodFromCurrentAnalysisResultCodebase = currenAnalysisResultCodeBaseManager.GetMethodByName(codeElementName);
                        if (selectedMethodFromCurrentAnalysisResultCodebase != null)
                            /*metricValue = (reflectionHelper.GetCodeElementMetric(
                                selectedMethodFromCurrentAnalysisResultCodebase,
                                typeof(IMethod),
                                metricDefinition.InternalPropertyName,
                                metricDefinition.NDependMetricType));*/
                            metricValue = currenAnalysisResultCodeBaseManager.GetCodeElementMetricValue(selectedMethodFromCurrentAnalysisResultCodebase, typeof(IMethod), metricDefinition);
                        break;
                }
                metricValues.Add(metricValue);
            }
            return metricValues;
        }
    }
}
