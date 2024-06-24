using Assets.Base.Scripts.Classes.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Behaviours.Data;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Enums;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.MovementPipelines;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.MovementPipelines.Modules;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Objects;
using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using NET.efilnukefesin.Lib.Common.Services.Ai.Actors;
using NET.efilnukefesin.Lib.Common.Services.Ai.DecisionNodes;
using NET.efilnukefesin.Lib.Common.Services.Ai.MovementPipelines.Modules;
using NET.efilnukefesin.Lib.Common.Services.Ai.Objects;
using NET.efilnukefesin.Lib.Common.Services.Ai.Processors;
using NET.efilnukefesin.Lib.Common.Services.Ai.Sensors;
using NET.efilnukefesin.Unity.Base;
using NET.efilnukefesin.Unity.Base.Ai;
using NET.efilnukefesin.Unity.Base.WeaponSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine;
using Action = NET.efilnukefesin.Lib.Common.Services.Ai.Objects.Action;

namespace NET.efilnukefesin.Unity.Base
{
    public class AiContainer : BaseBehaviour
    {
        #region Properties

        [Header("World Interface")]
        public GameObject Target;
        public AIMovement AIMovementForAiActor;
        public BulletEmitter WeaponsForAiActor;

        [Header("Inner Interface")]
        public GameObject OwnContainer;
        public PropertyList OwnProperties;
        public AiBehaviours OwnBehaviours;

        [Header("Debug")]
        public bool DoDrawDebugInfos = false;

        private IAIEntity aIEntity;
        private PositionInputSensor positionInputSensor;
        private HealthInputSensor healthInputSensor;

        //expose to AiContainerDebugger as readonly-fields
        // mem size
        // list of sensors
        // list of actors
        // etc
        private IAIEnityDebugInfos debugInfos;

        private bool isAiUpdateRunning = false;

        #endregion Properties

        #region Methods

        #region Awake
        private void Awake()
        {
            this.aIEntity = this.objectService.Create<AIEntity>(this.name);
            this.AIEntity_OnInitialize();

            this.SetupSensors();
            this.SetupDataGeneration();

            // TODO: add decision engine / decision engine needs to decide to approach target position, select best weapon and shoot when it thinks it will hit

            //TODO: add movement pipeline
            this.aIEntity.AddActor(this.objectService.Create<BehaviourStartActor>());
            this.logService.Debug("AiContainer", "Awake", $"Added new BehaviourStartActor to AiEntity.");

            if (this.AIMovementForAiActor != null)
            {
                //this.aIEntity.AddActor(this.objectService.Create<MovementActor>(this.AIMovementForAiActor));
                //this.logService.Debug("AiContainer", "Awake", $"Added new MovementActor to AiEntity.");

                this.aIEntity.AddActor(this.objectService.Create<SteeringActor>(this.AIMovementForAiActor));
                this.logService.Debug("AiContainer", "Awake", $"Added new SteeringActor to AiEntity.");
            }
            else
            {
                this.logService.Error("AiContainer", "Awake", $"MovementForAiActor is null, cannot initialize MovementActor!");
            }

            if (this.WeaponsForAiActor != null)
            {
                this.aIEntity.AddActor(this.objectService.Create<ShootActor>(this.WeaponsForAiActor));
                this.logService.Debug("AiContainer", "Awake", $"Added new ShootActor to AiEntity.");
            }
            else
            {
                this.logService.Error("AiContainer", "Awake", $"WeaponsForAiActor is null, cannot initialize ShootActor!");
            }

            if (this.OwnBehaviours != null)
            {
                this.aIEntity.AddActor(this.objectService.Create<BehaviourActor>(this.OwnBehaviours));
                this.logService.Debug("AiContainer", "Awake", $"Added new BehaviourActor to AiEntity.");
            }
            else
            {
                this.logService.Error("AiContainer", "Awake", $"OwnBehaviours is null, cannot initialize BehaviourActor!");
            }

        }
        #endregion Awake

        #region SetupSensors
        private void SetupSensors()
        {
            // add sensors
            TargetInputSensor targetInputSensor = this.objectService.Create<TargetInputSensor>();
            this.aIEntity.AddSensor(targetInputSensor);

            this.positionInputSensor = this.objectService.Create<PositionInputSensor>();
            this.aIEntity.AddSensor(this.positionInputSensor);

            this.healthInputSensor = this.objectService.Create<HealthInputSensor>();
            this.aIEntity.AddSensor(this.healthInputSensor);

            // add processors
            TargetInputProcessor targetInputProcessor = this.objectService.Create<TargetInputProcessor>();
            this.aIEntity.AddProcessor(targetInputProcessor);

            PositionInputProcessor positionInputProcessor = this.objectService.Create<PositionInputProcessor>();
            this.aIEntity.AddProcessor(positionInputProcessor);

            HealthInputProcessor healthInputProcessor = this.objectService.Create<HealthInputProcessor>();
            this.aIEntity.AddProcessor(healthInputProcessor);

            //train
            targetInputSensor.Add(this.objectService.Create<TargetInfo>(AiTargetVerb.Kill, this.Target));  //TODO: define parameters
        }
        #endregion SetupSensors

        #region SetupDataGeneration
        private void SetupDataGeneration()
        {
            IDataClassifier dataClassifier = this.aIEntity.GetDataClassifier();
            dataClassifier.AddRule(this.objectService.Create<RelativePositionClassificationRule>());
            dataClassifier.AddRule(this.objectService.Create<DistanceClassificationRule>());
        }
        #endregion SetupDataGeneration

        #region FixedUpdate
        private void FixedUpdate()
        {
            this.positionInputSensor.Add(this.objectService.Create<PositionInfo>(this.Target.transform.position, this.Target.transform.right, "Player", this.OwnContainer.transform.position, this.OwnContainer.transform.right));
            this.healthInputSensor.Add(this.objectService.Create<HealthInfo>(this.OwnProperties.Health));
            //ai needs to be updated, but not each frame I suppose
            if (!this.isAiUpdateRunning)
            {
                //start update task and set bool afterwards not to start repeatedly
                this.taskService.StartAsyncCallAndContinueInSameThread(() => { this.isAiUpdateRunning = true; this.aIEntity.Update(); }, () => { this.isAiUpdateRunning = false; });
            }
            this.debugInfos = this.aIEntity.GetDebugInfos();
        }
        #endregion FixedUpdate

        #region AIEntity_OnInitialize
        private void AIEntity_OnInitialize()
        {
            this.logService.Debug("AiContainer", "AIEntity_OnInitialize", $"Starting Method.");
            //TODO: set up decision tree etc
            IDecisionNode root = this.CreateSimpleDecisionTree();
            IDecisionEngine decisionEngine = this.aIEntity.GetDecisionEngine();
            decisionEngine.AddRoot(root);

            this.logService.Debug("AiContainer", "AIEntity_OnInitialize", $"Ended Method.");
        }
        #endregion AIEntity_OnInitialize

        #region CreateSimpleDecisionTree
        private IDecisionNode CreateSimpleDecisionTree()
        {
            IDecisionNode result = this.objectService.Create<DecisionNode>("Root Node", this.objectService.Create<Condition>("None"), this.objectService.Create<Action>("None"));

            //level 1
            IDecisionNode layer1_1 = this.objectService.Create<DecisionNode>("Move Left Node", this.objectService.Create<Condition>("RelativePositionInput is Left"), this.objectService.Create<Action>("Move: Left"));
            IDecisionNode layer1_2 = this.objectService.Create<DecisionNode>("Move Right Node", this.objectService.Create<Condition>("RelativePositionInput is Right"), this.objectService.Create<Action>("Move: Right"));
            IDecisionNode layer1_3 = this.objectService.Create<DecisionNode>("Shoot Node", this.objectService.Create<Condition>("RelativePositionInput is in Front"), this.objectService.Create<Action>("Shoot: -"));
            result.AddNode(layer1_1);
            result.AddNode(layer1_2);
            result.AddNode(layer1_3);

            return result;
        }
        #endregion CreateSimpleDecisionTree

        #region GetDebugInfos
        internal IAIEnityDebugInfos GetDebugInfos()
        {
            return this.debugInfos;
        }
        #endregion GetDebugInfos

        #region GetSensors
        internal List<ISensor> GetSensors()
        {
            return this.aIEntity.GetSensorContainer().Sensors.ToList();
        }
        #endregion GetSensors

        #endregion Methods
    }
}
