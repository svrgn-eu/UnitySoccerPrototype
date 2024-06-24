using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Ai.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    [RequireComponent(typeof(AiContainer))]
    public class AiContainerDebugger : BaseBehaviour
    {
        #region Properties

        private AiContainer aiContainer;

        public IAIEnityDebugInfos DebugInfos;

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Awake
        private void Awake()
        {
            this.aiContainer = this.GetComponent<AiContainer>();
        }
        #endregion Awake

        #region Update
        private void Update()
        {
            
        }
        #endregion Update

        #region FixedUpdate
        private void FixedUpdate()
        {
            this.DebugInfos = this.aiContainer.GetDebugInfos();
        }
        #endregion FixedUpdate

        #endregion Methods

        #region Events

        #endregion Events
    }
}
