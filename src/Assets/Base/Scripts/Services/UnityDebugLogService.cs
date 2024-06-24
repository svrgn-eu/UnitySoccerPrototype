using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.Services
{
    public class UnityDebugLogService : NET.efilnukefesin.Lib.Common.Services.BaseService, ILogService
    {
        #region Properties

        public int FatalCount { get; private set; } = 0;

        public int ErrorCount { get; private set; } = 0;

        public int WarningCount { get; private set; } = 0;

        public int InfoCount { get; private set; } = 0;

        public int DebugCount { get; private set; } = 0;

        public int TotalCount { get; private set; } = 0;

        private string MinimumLogLevel = string.Empty;

        private object lockObject = new object();

        #endregion Properties

        #region Construction

        public UnityDebugLogService(IServiceRegistry ServiceRegistry)
            : base(ServiceRegistry, null)
        {
            
        }

        #endregion Construction

        #region Methods

        #region Debug
        public void Debug(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null)
        {
            if (this.CheckLogLevel("Debug"))
            {
                this.WriteEntry("Debug", SenderClassName, SenderMethodName, Entry, exception);
                this.DebugCount++;
            }
        }
        #endregion Debug

        #region Error
        public void Error(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null)
        {
            if (this.CheckLogLevel("Error"))
            {
                this.WriteEntry("Error", SenderClassName, SenderMethodName, Entry, exception);
                //this.errorService.ReportError(SenderClassName, SenderMethodName, Entry, exception, false);
                this.ErrorCount++;
            }
        }
        #endregion Error

        #region Fatal
        public void Fatal(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null)
        {
            if (this.CheckLogLevel("Fatal"))
            {
                this.WriteEntry("Fatal", SenderClassName, SenderMethodName, Entry, exception);
                //this.errorService.ReportFatal(SenderClassName, SenderMethodName, Entry, exception, true);
                this.FatalCount++;
            }
        }
        #endregion Fatal

        #region Info
        public void Info(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null)
        {
            if (this.CheckLogLevel("Info"))
            {
                this.WriteEntry("Info", SenderClassName, SenderMethodName, Entry, exception);
                this.InfoCount++;
            }
        }
        #endregion Info

        #region Warning
        public void Warning(string SenderClassName, string SenderMethodName, string Entry, Exception exception = null)
        {
            if (this.CheckLogLevel("Warning"))
            {
                this.WriteEntry("Warning", SenderClassName, SenderMethodName, Entry, exception);
                this.WarningCount++;
            }
        }
        #endregion Warning

        #region WriteEntry
        private void WriteEntry(string Severity, string SenderClassName, string SenderMethodName, string Entry, Exception exception)
        {
            lock (this.lockObject)
            {
                //format Exception and add if not empty
                string text = $"[{Severity}] {SenderClassName}.{SenderMethodName}: {Entry}";
                switch (Severity)
                {
                    case "Error":
                        UnityEngine.Debug.LogError(text);
                        break;
                    case "Fatal":
                        UnityEngine.Debug.LogError(text);
                        break;
                    case "Warning":
                        UnityEngine.Debug.LogWarning(text);
                        break;
                    default:
                        UnityEngine.Debug.Log(text);
                        break;
                }
            }
            //UnityEngine.Debug.Log($"[{Severity}] {SenderClassName}.{SenderMethodName}: {Entry}");
            this.TotalCount++;

        }
        #endregion WriteEntry

        #region SetMinimumLogLevel
        public void SetMinimumLogLevel(string LogLevel)
        {
            this.MinimumLogLevel = LogLevel;
        }
        #endregion SetMinimumLogLevel

        #region CheckLogLevel
        private bool CheckLogLevel(string LogLevelToCheck)
        {
            bool result = false;

            if (string.IsNullOrEmpty(this.MinimumLogLevel))
            {
                result = true;
            }
            else if (this.MinimumLogLevel.Equals("Debug"))
            {
                if (LogLevelToCheck.Equals("Debug") || LogLevelToCheck.Equals("Info") || LogLevelToCheck.Equals("Warning") || LogLevelToCheck.Equals("Error") || LogLevelToCheck.Equals("Fatal"))
                {
                    result = true;
                }
            }
            else if (this.MinimumLogLevel.Equals("Info"))
            {
                if (LogLevelToCheck.Equals("Info") || LogLevelToCheck.Equals("Warning") || LogLevelToCheck.Equals("Error") || LogLevelToCheck.Equals("Fatal"))
                {
                    result = true;
                }
            }
            else if (this.MinimumLogLevel.Equals("Warning"))
            {
                if (LogLevelToCheck.Equals("Warning") || LogLevelToCheck.Equals("Error") || LogLevelToCheck.Equals("Fatal"))
                {
                    result = true;
                }
            }
            else if (this.MinimumLogLevel.Equals("Error"))
            {
                if (LogLevelToCheck.Equals("Error") || LogLevelToCheck.Equals("Fatal"))
                {
                    result = true;
                }
            }
            else if (this.MinimumLogLevel.Equals("Fatal"))
            {
                if (LogLevelToCheck.Equals("Fatal"))
                {
                    result = true;
                }
            }

            return result;
        }
        #endregion CheckLogLevel

        #endregion Methods
    }
}
