using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.WeaponSystem
{
    public class BulletReceiver : BaseBehaviour
    {
        #region Properties

        public Collider Collider;

        #endregion Properties

        #region Methods

        #region OnTriggerEnter
        private void OnTriggerEnter(Collider other)
        {
            // this class needs to be on the same gameobject as collider
            Bullet potentialBullet = other.gameObject.GetComponent<Bullet>();
            if (potentialBullet != null)
            {
                // TODO: collide, minus health etc
            }
        }
        #endregion OnTriggerEnter

        #endregion Methods
    }
}
