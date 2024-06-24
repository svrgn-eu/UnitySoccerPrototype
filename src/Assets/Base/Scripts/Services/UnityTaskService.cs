using NET.efilnukefesin.Lib.Common.Interfaces;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Services
{
    public class UnityTaskService : NET.efilnukefesin.Lib.Common.Services.BaseService, ITaskService
    {
        #region Properties

        private ThreadDispatcher dispatcher;

        #endregion Properties

        #region Construction
        public UnityTaskService(IServiceRegistry ServiceRegistry, ILogService LogService)
            : base(ServiceRegistry, LogService)
        {
        }
        #endregion Construction

        #region Methods

        #region StartAsyncCall
        public Task StartAsyncCall(Action ActionToTake)
        {
            Task result = Task.Factory.StartNew(ActionToTake, TaskCreationOptions.LongRunning);
            return result;
        }
        #endregion StartAsyncCall

        #region ExecuteOnMainThread
        public void ExecuteOnMainThread(Action ActionToTake)
        {
            if (this.dispatcher != null)
            {
                this.dispatcher.AddToBacklog(ActionToTake);
            }
            else
            {
                this.logService.Error("UnityTaskService", "ExecuteOnMainThread", $"Local dispatcher is null, cannot perform a call to the main thread!");
            }
        }
        #endregion ExecuteOnMainThread

        #region SetLocalDispatcher: sets the local dispatcher which will run things on the main thread, this is attached to a GameObject and will associate itself with this service
        /// <summary>
        /// sets the local dispatcher which will run things on the main thread, this is attached to a GameObject and will associate itself with this service
        /// </summary>
        /// <param name="Dispatcher">the dispatcher component</param>
        internal void SetLocalDispatcher(ThreadDispatcher Dispatcher)
        {
            this.dispatcher = Dispatcher;
        }
        #endregion SetLocalDispatcher

        #region StartAsyncCallAndContinueInSameThread
        public void StartAsyncCallAndContinueInSameThread(Action ActionToTake, Action ActionToContinueWith)
        {
            this.StartAsyncCall(ActionToTake).ContinueWith((task) => { ActionToContinueWith.Invoke(); } );
        }
        #endregion StartAsyncCallAndContinueInSameThread

        #region StartAsyncCallAndContinueInMainThread
        public void StartAsyncCallAndContinueInMainThread(Action ActionToTake, Action ActionToContinueWith)
        {
            this.StartAsyncCall(ActionToTake).ContinueWith((task) => { this.ExecuteOnMainThread(ActionToContinueWith); });
        }
        #endregion StartAsyncCallAndContinueInMainThread

        #endregion Methods
    }
}
