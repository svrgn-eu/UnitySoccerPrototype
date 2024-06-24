using NET.efilnukefesin.Lib.Common.Attributes.Ai;
using NET.efilnukefesin.Lib.Common.Attributes.Ai.Enums;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services.Ai.Actors;
using NET.efilnukefesin.Unity.Base.WeaponSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Base.Scripts.Classes.Ai
{
    [AiAbility(AiAbility.Shoot)]
    public class ShootActor : BaseActor
    {
        #region Properties

        public new string Name { get; private set; } = "ShootActor";

        public BulletEmitter Weapons { get; set; }

        #endregion Properties

        #region Construction

        public ShootActor(ILogService LogService, BulletEmitter weapons)
            : base(LogService)
        {
            this.Weapons = weapons;
            this.Keywords = new List<string>() { "Shoot" };
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
                this.Weapons.ShootOnce();
            }
        }
        #endregion Execute

        #endregion Methods

        #region Events

        #endregion Events
    }
}
