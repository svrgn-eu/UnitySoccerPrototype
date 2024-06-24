using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    /// <summary>
    /// this class provides a simple auto move feature on any of the axis.
    /// </summary>
    [RequireComponent(typeof(Movement))]  // for moving the object
    public class AutoMove : BaseBehaviour
    {
        #region Properties

        public float SpeedX = 0f;
        public float SpeedY = 0f;
        public float SpeedZ = 0f;

        private Movement movement;

        #endregion Properties

        #region Methods

        #region Awake
        private void Awake()
        {
            this.movement = this.GetComponent<Movement>();
        }
        #endregion Awake

        #region Update
        private void Update()
        {
            this.movement.AddToDelta(new Vector3(this.SpeedX, this.SpeedY, this.SpeedZ));
        }
        #endregion Update

        #endregion Methods
    }
}