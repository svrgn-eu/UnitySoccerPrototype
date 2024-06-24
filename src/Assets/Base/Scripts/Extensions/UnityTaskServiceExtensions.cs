using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Services
{
    public static class UnityTaskServiceExtensions
    {
        #region SetDispatcher
        public static void SetDispatcher(this ITaskService Service, ThreadDispatcher Dispatcher)
        {
            UnityTaskService castedService = (UnityTaskService)Service;
            if (castedService != null)
            {
                castedService.SetLocalDispatcher(Dispatcher);
            }
        }
        #endregion SetDispatcher
    }
}
