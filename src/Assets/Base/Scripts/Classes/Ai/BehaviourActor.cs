using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services.Ai.Actors;
using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Base.Scripts.Classes.Ai
{
    public class BehaviourActor : BaseActor
    {
        #region Properties

        public new string Name { get; private set; } = "BehaviourActor";

        private AiBehaviours aiBehaviours;

        #endregion Properties

        #region Construction

        public BehaviourActor(ILogService LogService, AiBehaviours aiBehaviours)
            : base(LogService)
        {
            this.aiBehaviours = aiBehaviours;
            this.Keywords = new List<string>() { "Seek", "Flee", "Arrive" };  //TODO: rename with Behaviour-Prefix?
        }

        #endregion Construction

        #region Methods

        #region Execute
        public override void Execute(IActionPipeline ActionPipeline, IAction Action, IActorContainer ActorContainer)
        {
            string[] parts = Action.Text.Split(':');
            string keyword = parts[0].Trim();
            string command = parts[1].Trim();

            if (this.Keywords.Contains(keyword))
            {
                //command not relevant (yet)
                throw new NotImplementedException();
                //TODO: interpret keyword
                //TODO: parse commands and pass them to this.aiBehaviours
            }
        }
        #endregion Execute

        #endregion Methods

        #region Events

        #endregion Events
    }
}
