using Assets.Base.Scripts.Services;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Unity.Base.Helpers;
using NET.efilnukefesin.Unity.Base.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace NET.efilnukefesin.Unity.Base
{
    public abstract class BaseBehaviour : MonoBehaviour, IBehaviour
    {
        #region Properties
        protected DiContainer container;

        protected ILogService logService;
        protected IMessageService messageService;
        protected IFeatureService featureService;
        protected IConfigurationService configurationService;
        protected IObjectService objectService;
        protected ITimeService timeService;
        protected ITaskService taskService;

        #endregion Properties

        #region Methods

        #region Construct: Method used by Zenject Dependency Injection (careful, just Method Injection as c'tor for Monobehaviours is hidden), is being called before Start()
        /// <summary>
        /// Method used by Zenject Dependency Injection (careful, just Method Injection as c'tor for Monobehaviours is hidden), is being called before Start()
        /// </summary>
        /// <param name="LogService"></param>
        [Inject]
        public void Construct(DiContainer Container, ILogService LogService, IMessageService MessageService, IFeatureService FeatureService, IConfigurationService ConfigurationService, IObjectService ObjectService, ITimeService TimeService, ITaskService TaskService)
        {
            string servicesNotResolvedList = string.Empty;

            this.container = Container;

            this.logService = LogService;
            this.messageService = MessageService;
            this.featureService = FeatureService;
            this.configurationService = ConfigurationService;
            this.objectService = ObjectService;
            this.timeService = TimeService;
            this.taskService = TaskService;
            //TODO: add more dependencies like MessageService etc

            //TODO: check services not resolved

            if (string.IsNullOrEmpty(servicesNotResolvedList))
            {
                this.logService.Debug("BaseBehaviour", "Construct", $"Dependencies filled");
            }
            else
            {
                this.logService.Error("BaseBehaviour", "Construct", $"These dependencies could not be filled: {servicesNotResolvedList}");
            }
        }
        #endregion Construct

        #region Start
        protected virtual void Start()
        {
            PrerequisitesHelper.CheckRequiredAttributes(this);
        }
        #endregion Start

        #endregion Methods
    }
}