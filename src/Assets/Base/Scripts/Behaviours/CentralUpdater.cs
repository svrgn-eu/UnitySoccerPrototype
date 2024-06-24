using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Base.Scripts.Behaviours
{
    public class CentralUpdater : BaseBehaviour
    {
        #region Properties

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Update
        private void Update()
        {
            this.timeService.Tick(Time.deltaTime);
            //TODO: update other stuff, more general approach needed
        }
        #endregion Update

        #endregion Methods

        #region Events

        #endregion Events
    }
}
