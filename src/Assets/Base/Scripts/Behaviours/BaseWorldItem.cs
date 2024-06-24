using NET.efilnukefesin.Lib.Common.Interfaces.StateMachine;
using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace ANET.efilnukefesin.Unity.Base
{
    public abstract class BaseWorldItem : BaseBehaviour, IUseStateMachine
    {
        #region Properties

        private IStateMachine stateMachine;
        public IStateMachine StateMachine { get { return this.stateMachine; } private set { this.stateMachine = value; } }

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region InjectStateMachine
        [Inject]
        public void InjectStateMachine(IStateMachine StateMachine)
        {
            this.stateMachine = StateMachine;
            this.InitializeStateMachine(this.stateMachine);
        }
        #endregion InjectStateMachine

        #region InitializeStateMachine
        protected abstract void InitializeStateMachine(IStateMachine StateMachine);
        #endregion InitializeStateMachine

        #region Interact
        public abstract void Interact();
        #endregion Interact

        #endregion Methods

        #region Events

        #endregion Events
    }
}
