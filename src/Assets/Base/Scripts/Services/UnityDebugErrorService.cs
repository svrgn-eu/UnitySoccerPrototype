using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Services
{
    public class UnityDebugErrorService : NET.efilnukefesin.Lib.Common.Services.BaseService, IErrorService
    {
        #region Properties

        #endregion Properties

        #region Construction
        public UnityDebugErrorService(IServiceRegistry ServiceRegistry, ILogService LogService)
            : base(ServiceRegistry, LogService)
        {
            
        }

        #endregion Construction

        #region Methods

        #region ReportError
        public void ReportError(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null, bool DoAbortApp = false)
        {
            this.Report("Error", SenderClassName, SenderMethodName, Entry, exception, DoAbortApp);
        }
        #endregion ReportError

        #region ReportFatal
        public void ReportFatal(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null, bool DoAbortApp = true)
        {
            this.Report("Fatal", SenderClassName, SenderMethodName, Entry, exception, DoAbortApp);
        }
        #endregion ReportFatal

        #region Report
        private void Report(string Severity, string SenderClassName, string SenderMethodName, string Entry, Exception exception, bool DoAbortApp)
        {
            //TODO: format Exception and add if not empty
            string abortText = string.Empty;
            if (DoAbortApp)
            {
                abortText = ": App should be aborted";
            }
            UnityEngine.Debug.LogError($"[{Severity} (ErrorService{abortText})] {SenderClassName}.{SenderMethodName}: {Entry}");
            //TODO: how to abort the app? Send a message? Ring dependency at the end. Use method in IApplicationService?
        }
        #endregion Report

        #endregion Methods
    }
}
