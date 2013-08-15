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

        public List<T> GetAssemblyMetricHistory<T>(string assemblyName, string metricName)
        {
            List<T> metricValues = new List<T>();

            foreach (var m in analysisResultRefsList)
            {
                try
                {
                    IAnalysisResult analisysResult = m.Load();
                    ICodeBase codeBase = analisysResult.CodeBase;
                    IAssembly selectedAssembly = codeBase.Application.Assemblies.Where(a => a.Name == assemblyName).First();
                    metricValues.Add((T)selectedAssembly.GetType().GetProperty(metricName).GetValue(selectedAssembly));
                }
                catch (AnalysisException analysisException)
                {
                    string exceptionString = analysisException.ToString();
                }
            }
            return metricValues;
        }

        public List<MetricType> GetMetricHistory<MetricType, CodeElementType>(string codeElementName, string metricName)
        {
            List<MetricType> metricValues = new List<MetricType>();

            foreach (var m in analysisResultRefsList)
            {
                try
                {
                    IAnalysisResult analisysResult = m.Load();
                    ICodeBase codeBase = analisysResult.CodeBase;
                    switch (typeof(CodeElementType).ToString())
                    {
                        case "NDepend.CodeModel.IAssembly":
                            IAssembly selectedAssembly = codeBase.Application.Assemblies.Where(a => a.Name == codeElementName).First();
                            metricValues.Add((MetricType)selectedAssembly.GetType().GetProperty(metricName).GetValue(selectedAssembly));
                            break;
                        case "NDepend.CodeModel.INamespace":
                            INamespace selectedNamespace = codeBase.Application.Namespaces.Where(a => a.Name == codeElementName).First();
                            metricValues.Add((MetricType)selectedNamespace.GetType().GetProperty(metricName).GetValue(selectedNamespace));
                            break;
                        case "NDepend.CodeModel.IType":
                            IType selectedType = codeBase.Application.Types.Where(a => a.Name == codeElementName).First();
                            metricValues.Add((MetricType)selectedType.GetType().GetProperty(metricName).GetValue(selectedType));
                            break;
                        case "NDepend.CodeModel.IModule":
                            IMethod selectedMethod = codeBase.Application.Methods.Where(a => a.Name == codeElementName).First();
                            metricValues.Add((MetricType)selectedMethod.GetType().GetProperty(metricName).GetValue(selectedMethod));
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

        public IList GetMetricHistory(object codeElement, object metricDefinition)
        {
            string codeElementType = ((NDependMetricDefinition)metricDefinition).NDependCodeElementType;
            string metricInternalPorpertyName = ((NDependMetricDefinition)metricDefinition).InternalPropertyName;
            Type metricType = Type.GetType(((NDependMetricDefinition)metricDefinition).NDependMetricType);
            IList metricValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(metricType));

            /*
            MethodInfo method = this.GetType().GetMethod("GetMetricHistory");
            MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { metricType, typeof(IAssembly) });
            metricValues = (IList)genericMethod.Invoke(this, new[] { assemblyName, metricInternalPorpertyName });*/

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
                            IAssembly selectedAssembly = codeBase.Application.Assemblies.Where(a => a.Name == codeElementName).First();
                            PropertyInfo[] pi = selectedAssembly.GetType().GetProperties();
                            metricValues.Add(selectedAssembly.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedAssembly));
                            break;
                        case "NDepend.CodeModel.INamespace":
                            codeElementName = ((INamespace)codeElement).Name;
                            INamespace selectedNamespace = codeBase.Application.Namespaces.Where(a => a.Name == codeElementName).First();
                            metricValues.Add(selectedNamespace.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedNamespace));
                            break;
                        case "NDepend.CodeModel.IType":
                            codeElementName = ((IType)codeElement).Name;
                            IType selectedType = codeBase.Application.Types.Where(a => a.Name == codeElementName).First();
                            metricValues.Add(selectedType.GetType().GetProperty(metricInternalPorpertyName).GetValue(selectedType));
                            break;
                        case "NDepend.CodeModel.IMethod":
                            codeElementName = ((IMethod)codeElement).Name;
                            IMethod selectedMethod = codeBase.Application.Methods.Where(a => a.Name == codeElementName).First();
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
