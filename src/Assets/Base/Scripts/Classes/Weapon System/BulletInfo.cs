using NET.efilnukefesin.Lib.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.WeaponSystem
{
    [Serializable]
    public class BulletInfo
    {
        #region Properties

        public string Name;
        public GameObject BulletPrefab;

        public float Speed;
        public float Damage;
        public float LifeTimeInSeconds;

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
