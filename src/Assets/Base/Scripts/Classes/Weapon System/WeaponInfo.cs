using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.WeaponSystem
{
    [Serializable]
    public class WeaponInfo
    {
        #region Properties

        [Tooltip("The Name of the weapon")]
        public string Name;

        [Tooltip("The Name of the bullet in the bullet pool")]
        public string BulletName;

        [Tooltip("only fire a bullet with a keypress")]
        public bool IsSingleShotMode;

        [Tooltip("fires several bullets at a time, only works when 'IsSingleShotMode' is false")]
        public bool IsBurstMode;

        [Tooltip("how many bullets are fired within a single burst, only considered when 'IsBurstMode' is true")]
        public int BulletsPerBurst;

        [Tooltip("when will the next bullet be fired when the key is pressed?")]
        public float NextBulletEmissionInSeconds;

        [Tooltip("Is the Weapon currently active? Only one Weapon can be active at a time")]
        public bool IsActive = false;

        [Tooltip("The list of emission points the weapon is firing from")]
        public List<EmissionPointInfo> EmissionPoints;

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
