using NET.efilnukefesin.Lib.Common.Attributes.Ai;
using NET.efilnukefesin.Lib.Common.Attributes.Ai.Enums;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services.Ai.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Ai
{
    [AiAbility(AiAbility.Move2D)]
    public class SteeringActor : BaseActor
    {
        #region Properties

        public new string Name { get; private set; } = "SteeringActor";

        public AIMovement aiMovement { get; set; }

        private string lastCommand = string.Empty;

        #endregion Properties

        #region Construction

        public SteeringActor(ILogService LogService, AIMovement AIMovement)
            : base(LogService)
        {
            this.aiMovement = AIMovement;
            this.Keywords = new List<string>() { "Steer" };
        }

        #endregion Construction

        #region Methods

        #region Execute
        public override void Execute(IActionPipeline ActionPipeline, IAction Action, IActorContainer ActorContainer)
        {
            string[] parts = Action.Text.Split(':');
            string keyword = parts[0].Trim();
            string command = parts[1].Trim();

            //if (this.Keywords.Contains(keyword))
            //{
            //    if (command.Equals("Left"))
            //    {
            //        if (this.lastCommand.Equals(command))
            //        {
            //            this.aiMovement.MoveLeft();
            //        }
            //        else
            //        {
            //            this.aiMovement.MoveLeft(true);
            //        }
            //    }
            //    else if (command.Equals("Right"))
            //    {
            //        if (this.lastCommand.Equals(command))
            //        {
            //            this.aiMovement.MoveRight();
            //        }
            //        else
            //        {
            //            this.aiMovement.MoveRight(true);
            //        }
            //    }
            //    //TODO: parse command and translate for MovementBehaviour
            //    this.lastCommand = command; //store last command for comparing
            //}
        }
        #endregion Execute

        #endregion Methods
    }
}
