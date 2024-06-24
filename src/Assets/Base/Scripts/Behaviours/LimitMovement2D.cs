using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    [RequireComponent(typeof(Movement))]
    public class LimitMovement2D : BaseBehaviour
    {
        #region Properties

        public float Left = 0f;
        public float Right = 0f;
        public float Top = 0f;
        public float Bottom = 0f;

        private Movement movement;

        #endregion Properties

        #region Methods

        #region Start
        protected override void Start()
        {
            base.Start();

            this.movement = this.GetComponent<Movement>();
        }
        #endregion Start

        #region Update
        private void Update()
        {
            float xMovementClamp = Mathf.Clamp(this.transform.position.x, this.Left, this.Right);
            float yMovementClamp = Mathf.Clamp(this.transform.position.y, this.Bottom, this.Top);
            Vector3 limitedMovement = new Vector3(xMovementClamp, yMovementClamp, this.transform.position.z);
            this.transform.position = limitedMovement;
        }
        #endregion Update

        #endregion Methods
    }
}
