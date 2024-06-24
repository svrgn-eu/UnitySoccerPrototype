using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public class MoveRelativeToObject : BaseBehaviour
    {
        #region Properties

        [Tooltip("The GameObject to move with")]
        public Movement Master;

        [Tooltip("The GameObjects to Follow the Master, relative to their position")]
        public GameObject[] Followers;

        public bool IsXAxisLocked = false;
        public bool IsYAxisLocked = false;
        public bool IsZAxisLocked = false;

        #endregion Properties

        #region Methods

        #region Awake
        private void Awake()
        {
            // TODO: retrieve delta positions

        }
        #endregion Awake

        #region Update
        private void Update()
        {
            // TODO: apply diff movement to Followers
            foreach (GameObject follower in this.Followers)
            {
                Vector3 newPosition = follower.transform.position;
                if (!this.IsXAxisLocked)
                {
                    newPosition.x += this.Master.CurrentDeltaByTime.x;
                }
                if (!this.IsYAxisLocked)
                {
                    newPosition.y += this.Master.CurrentDeltaByTime.y;
                }
                if (!this.IsZAxisLocked)
                {
                    newPosition.z += this.Master.CurrentDeltaByTime.z;
                }
                follower.transform.position = newPosition;
            }
        }
        #endregion Update

        #endregion Methods
    }
}
