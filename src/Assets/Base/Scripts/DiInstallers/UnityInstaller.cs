using Assets.Base.Scripts.Services;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.MovementPipelines;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.MovementPipelines.Data;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.MovementPipelines.Modules;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services;
using NET.efilnukefesin.Lib.Common.Services.Ai;
using NET.efilnukefesin.Lib.Common.Services.Ai.Modules;
using NET.efilnukefesin.Lib.Common.Services.Ai.Objects;
using NET.efilnukefesin.Lib.Common.Services.Ai.MovementPipelines;
using NET.efilnukefesin.Lib.Common.Services.Ai.MovementPipelines.Data;
using NET.efilnukefesin.Lib.Common.Services.Ai.MovementPipelines.Modules;
using NET.efilnukefesin.Unity.Base.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Action = NET.efilnukefesin.Lib.Common.Services.Ai.Objects.Action;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Behaviours.Data;
using NET.efilnukefesin.Lib.Common.Services.Ai.Behaviours.Data;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Objects;
using NET.efilnukefesin.Lib.Common.Interfaces.StateMachine;
using NET.efilnukefesin.Lib.Common.Implementations.StateMachine;
using NET.efilnukefesin.Lib.Common.Interfaces.Generation.Services;
using NET.efilnukefesin.Lib.Common.Implementations.Generation.Services;

namespace NET.efilnukefesin.Unity.Base
{
    public class UnityInstaller : MonoInstaller
    {
        #region Methods

        #region InstallBindings
        public override void InstallBindings()
        {
            this.AddBindings(this.Container);
            this.AddOptionalBindings(this.Container);

            this.RegisterEntities();

            this.DoInitialConfiguration();
        }
        #endregion InstallBindings

        #region AddBindings
        public void AddBindings(DiContainer ContainerToAddBindungsTo)
        {
            ContainerToAddBindungsTo.Bind<IServiceRegistry>().To<ServiceRegistry>().AsSingle();

            ContainerToAddBindungsTo.Bind<IErrorService>().To<UnityDebugErrorService>().AsSingle();
            ContainerToAddBindungsTo.Bind<ILogService>().To<UnityDebugLogService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IMessageService>().To<MessageService>().AsSingle();
            ContainerToAddBindungsTo.Bind<ITimeService>().To<TimeService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IConfigurationService>().To<ConfigurationService>().AsSingle();
            ContainerToAddBindungsTo.Bind<ITechnicalService>().To<TechnicalService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IFeatureService>().To<FeatureService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IUserNotificationService>().To<UserNotificationService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IPersistanceService>().To<MemoryPersistanceService>().AsSingle();
            ContainerToAddBindungsTo.Bind<ITaskService>().To<UnityTaskService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IObjectService>().To<UnityObjectService>().AsSingle();
            ContainerToAddBindungsTo.Bind<ITranslationService>().To<TranslationService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IAIService>().To<AiService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IAudioService>().To<UnityAudioService>().AsSingle();
            ContainerToAddBindungsTo.Bind<IDebugVisualizationService>().To<UnityDebugVisualizationService>().AsSingle();

            ContainerToAddBindungsTo.Bind<IStateMachine>().To<StateMachine>().AsTransient();
            //ContainerToAddBindungsTo.BindInterfacesAndSelfTo<StateMachine>().AsTransient();
            //ContainerToAddBindungsTo.Bind<IMovementPlanningPipeline>().To<SteeringPipeline>().AsTransient();
        }
        #endregion AddBindings

        #region AddOptionalBindings
        public void AddOptionalBindings(DiContainer ContainerToAddBindungsTo)
        {
            ContainerToAddBindungsTo.Bind<IContentGenerationService>().To<ContentGenerationService>().AsSingle();
        }
        #endregion AddOptionalBindings

        #region DoInitialConfiguration
        private void DoInitialConfiguration()
        {
            // set the minimum log level
            ILogService logService = Container.Resolve<ILogService>();
            logService.SetMinimumLogLevel("Info");

            // set individual config settings
            IConfigurationService configurationService = Container.Resolve<IConfigurationService>();
            configurationService.Add<bool>("UsePixelledLook", true);  // use Pixel Cam?
            //configurationService.Add<bool>("UsePixelledLook", false);  // use Pixel Cam?

            //initialize time service
            ITimeService timeService = Container.Resolve<ITimeService>();
            timeService.SetCurrentTime("Reality", DateTime.Now);
        }
        #endregion DoInitialConfiguration

        #region RegisterEntities: registers special classes, like AI stuff
        /// <summary>
        /// registers special classes, like AI stuff
        /// </summary>
        /// <param name="services"></param>
        private void RegisterEntities()
        {
            //Container.Bind<IAIEntity>().To<AIEntity>().AsTransient();
            Container.Bind<IMemory>().To<TransientMemory>().AsTransient();
            Container.Bind<IInformationSeeker>().To<InformationSeeker>().AsTransient();
            Container.Bind<IDataSelector>().To<FixedTimeDataSelector>().AsTransient();
            Container.Bind<IDataClassifier>().To<FixedDataClassifier>().AsTransient();
            Container.Bind<IDecisionLearner>().To<EmptyDecisionLearner>().AsTransient();
            Container.Bind<IDecisionTreeImporter>().To<DecisionTreeTextImporter>().AsTransient();
            Container.Bind<IDecisionTreeExporter>().To<DecisionTreeTextExporter>().AsTransient();
            Container.Bind<IDecisionEngine>().To<DecisionEngine>().AsTransient();
            Container.Bind<ISensorContainer>().To<SensorContainer>().AsTransient();
            Container.Bind<IProcessorContainer>().To<ProcessorContainer>().AsTransient();
            Container.Bind<IActorContainer>().To<ActorContainer>().AsTransient();
            Container.Bind<IActionPipeline>().To<ActionPipeline>().AsTransient();
            Container.Bind<IMovementPlanningPipeline>().To<SteeringPipeline>().AsTransient();  //TODO: re-think if this should be centrally given

            Container.Bind<IInformation>().To<Information>().AsTransient();

            Container.Bind<IAIEnityDebugInfos>().To<AIEnityDebugInfos>().AsTransient();

            Container.Bind<ICondition>().To<Condition>().AsTransient();
            Container.Bind<IAction>().To<Action>().AsTransient();

            //Container.Bind<ITargeter>().To<ChaseTargeter>().AsTransient();
            //Container.Bind<IDecomposer>().To<StraightDecomposer>().AsTransient();
            //Container.Bind<IConstraint>().To<NoConstraint>().AsTransient();
            //Container.Bind<IActuator>().To<Actuator>().AsTransient();

            Container.Bind<IKinematicData>().To<KinematicData>().AsTransient();
            Container.Bind<ISteeringData>().To<SteeringData>().AsTransient();
            Container.Bind<IGoal>().To<Goal>().AsTransient();
            Container.Bind<IPath>().To<Path>().AsTransient();
            Container.Bind<IPathNode>().To<PathNode>().AsTransient();

            Container.Bind<IEntityInfos>().To<EntityInfos>().AsTransient();

            Container.BindInterfacesAndSelfTo<NET.efilnukefesin.Lib.Common.Objects.Vector3>().AsTransient();
        }
        #endregion RegisterEntities

        #endregion Methods
    }
}
