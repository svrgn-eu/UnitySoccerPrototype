using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.WeaponSystem
{
    public class Bullet : BaseBehaviour
    {
        #region Properties

        private float creationTime;
        private float lifeSpan;

        #endregion Properties

        #region Methods

        #region Create
        internal void Create(float lifeSpan)
        {
            this.creationTime = Time.realtimeSinceStartup;
            this.lifeSpan = lifeSpan;
            this.gameObject.SetActive(true);
        }
        #endregion Create

        #region ShouldBeDead
        internal bool ShouldBeDead()
        {
            return this.creationTime + this.lifeSpan < Time.realtimeSinceStartup;
        }
        #endregion ShouldBeDead

        #endregion Methods


    }
}
