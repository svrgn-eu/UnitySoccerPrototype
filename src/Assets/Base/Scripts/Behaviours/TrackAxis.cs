using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public class TrackAxis : BaseBehaviour
    {
        #region Properties

        public Transform Source;
        public bool DoAlignXAxis = false;
        public bool DoAlignYAxis = false;
        public bool DoAlignZAxis = false;

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region FixedUpdate
        private void FixedUpdate()
        {
            //align axis with source and own transform
            Vector3 newPosition = this.transform.position;
            if (this.DoAlignXAxis)
            {
                newPosition.x = this.Source.transform.position.x;
            }
            if (this.DoAlignYAxis)
            {
                newPosition.y = this.Source.transform.position.y;
            }
            if (this.DoAlignZAxis)
            {
                newPosition.z = this.Source.transform.position.z;
            }
            this.transform.position = newPosition;
        }
        #endregion FixedUpdate

        #endregion Methods

        #region Events

        #endregion Events
    }
}
