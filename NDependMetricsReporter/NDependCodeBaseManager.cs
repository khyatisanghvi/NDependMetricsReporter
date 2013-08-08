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
    class NDependCodeBaseManager
    {
        IProject nDependProject;

        public NDependCodeBaseManager(IProject nDependProject)
        {
            this.nDependProject = nDependProject;
        }

        public ICodeBase LoadLastCodebase()
        {
            IAnalysisResultRef lastAnalysisResultRef;
            bool result = nDependProject.TryGetMostRecentAnalysisResultRef(out lastAnalysisResultRef);
            try
            {
                IAnalysisResult analisysResult = lastAnalysisResultRef.Load();
                ICodeBase codeBase = analisysResult.CodeBase;
                return codeBase;
            }
            catch (AnalysisException analysisException)
            {
                string exceptionString = analysisException.ToString();
                return null;
            }
        }
    }
}
