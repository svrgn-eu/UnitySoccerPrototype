using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public class Movement : BaseBehaviour
    {
        #region Properties

        public Vector3 CurrentDelta { get; private set; }
        public Vector3 CurrentDeltaByTime { get; private set; }

        #endregion Properties

        #region Methods

        #region AddToDelta
        public void AddToDelta(Vector3 Delta)
        {
            this.CurrentDelta += Delta;
        }
        #endregion AddToDelta

        #region Update
        private void Update()
        {
            Vector3 newPosition = this.transform.position;

            this.CurrentDeltaByTime = this.CurrentDelta * Time.deltaTime;
            newPosition += this.CurrentDeltaByTime;
            this.transform.position = newPosition;

            this.CurrentDelta = Vector3.zero;
        }
        #endregion Update

        #endregion Methods
    }
}
