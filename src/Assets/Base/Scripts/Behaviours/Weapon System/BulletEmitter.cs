using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using NET.efilnukefesin.Lib.Common.Extensions;

namespace NET.efilnukefesin.Unity.Base.WeaponSystem
{
    public class BulletEmitter : BaseBehaviour
    {
        #region Properties

        public List<WeaponInfo> Weapons;
        public BulletPool BulletPool;

        private float timeAtLastFiring = 0f;

        private int ShotsFiredInCurrentActionPress = 0;

        private bool IsActionPressedCurrently = false;
        private bool ShallShootOnce = false;

        #endregion Properties

        #region Methods

        #region ShootPressedPerformed
        public void ShootPressedPerformed(InputAction.CallbackContext context)
        {
            this.logService.Debug("BulletEmitter", "ShootPressedPerformed", $"Entered Method");

            if (context.phase.Equals(InputActionPhase.Started))
            {
                this.IsActionPressedCurrently = true;
                this.logService.Debug("BulletEmitter", "ShootPressedPerformed", $"Phase was started");
            }
            else if (context.phase.Equals(InputActionPhase.Canceled))
            {
                this.IsActionPressedCurrently = false;
                this.logService.Debug("BulletEmitter", "ShootPressedPerformed", $"Phase was cancelled");
            }

            this.logService.Debug("BulletEmitter", "ShootPressedPerformed", $"Exited Method");
        }
        #endregion ShootPressedPerformed

        #region ShootOnce
        //used by ai actors
        public void ShootOnce()
        {
            this.logService.Debug("BulletEmitter", "ShootOnce", $"Entered Method");

            this.ShallShootOnce = true;

            this.logService.Debug("BulletEmitter", "ShootOnce", $"Exited Method");
        }
        #endregion ShootOnce

        #region Update
        private void Update()
        {
            if (this.IsActionPressedCurrently || this.ShallShootOnce)
            {
                this.DoShooting();
                this.ShallShootOnce = false;  //reset trigger var
            }
            else
            {
                this.ResetShooting();
            }
        }
        #endregion Update

        #region DoShooting
        private void DoShooting()
        {
            WeaponInfo activeWeapon = this.GetActiveWeapon();  // acquire the current weapon
            BulletInfo newBulletInfo = this.BulletPool.GetInfo(activeWeapon.BulletName);  // get the used bullet

            float currentTime = Time.realtimeSinceStartup;

            bool doFire = this.timeAtLastFiring + activeWeapon.NextBulletEmissionInSeconds <= currentTime;
            this.logService.Debug("BulletEmitter", "ShootPressedPerformed", $"Time condition for firing a shot is '{doFire}'");

            if (doFire)  // only spawn a new bullet after cooldown period, not caring about the mode and conditions; minimum duration between two bullets is 'NextBulletEmissionInSeconds'
            {
                if ((activeWeapon.IsSingleShotMode && this.ShotsFiredInCurrentActionPress.Equals(0)) ||
                    (activeWeapon.IsBurstMode && (this.ShotsFiredInCurrentActionPress < activeWeapon.BulletsPerBurst)) ||
                    (!activeWeapon.IsSingleShotMode && !activeWeapon.IsBurstMode)) // test special conditions like single shot etc
                {
                    // fire on each emitting source
                    foreach (EmissionPointInfo emissionPoint in activeWeapon.EmissionPoints)
                    {
                        this.FireBullet(activeWeapon, newBulletInfo, emissionPoint);
                    }

                    this.ShotsFiredInCurrentActionPress++;  // increase number of shots fired in current action phase (NOT per emmission point)

                    this.timeAtLastFiring = Time.realtimeSinceStartup;

                    this.logService.Debug("BulletEmitter", "ShootPressedPerformed", $"Shot fired! Time at list firing is '{this.timeAtLastFiring}', Shots fired in current action is '{this.ShotsFiredInCurrentActionPress}'");
                }
            }
            else
            {
                // TODO: notify user?
            }
        }
        #endregion DoShooting

        #region ResetShooting
        private void ResetShooting()
        {
            this.ShotsFiredInCurrentActionPress = 0;  // reset shots fired as key was released
            this.logService.Debug("BulletEmitter", "ResetShooting", $"Shooting Phase was reset");
        }
        #endregion ResetShooting

        #region FireBullet
        private void FireBullet(WeaponInfo activeWeapon, BulletInfo newBulletInfo, EmissionPointInfo emissionPoint)
        {
            GameObject newBullet = this.BulletPool.GetBullet(activeWeapon.BulletName);

            newBullet.GetComponent<Bullet>().Create(newBulletInfo.LifeTimeInSeconds);
            // firing only allowed within the bullet's individual time frame

            newBullet.transform.position = emissionPoint.Source.position;

            Vector3 direction = emissionPoint.Direction.position - emissionPoint.Source.position;
            direction.Normalize();

            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
            float bulletSpeed = newBulletInfo.Speed;
            bulletRigidbody.AddForce(direction * bulletSpeed);  // TODO: find out, why sereveral tries produce seemingly different speeds
        }
        #endregion FireBullet

        #region GetActiveWeapon
        public WeaponInfo GetActiveWeapon()
        {
            WeaponInfo result = default;

            if (this.Weapons.Any(x => x.IsActive))
            {
                result = this.Weapons.Where(x => x.IsActive).FirstOrDefault();
            }
            else
            {
                this.logService.Warning("BulletEmitter", "GetActiveWeapon", $"No Active Weapon found!");
            }

            return result;
        }
        #endregion GetActiveWeapon

        #region SwitchToNextWeapon
        public void SwitchToNextWeapon(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Started))
            {
                int activeIndex = this.Weapons.FindIndex(x => x.IsActive);
                int newIndex = activeIndex + 1;
                if (newIndex > this.Weapons.Count - 1)
                {
                    newIndex = 0;
                }

                this.Weapons[activeIndex].IsActive = false;
                this.Weapons[newIndex].IsActive = true;
            }
        }
        #endregion SwitchToNextWeapon

        #region SwitchToPreviousWeapon
        public void SwitchToPreviousWeapon(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Started))
            {
                int activeIndex = this.Weapons.FindIndex(x => x.IsActive);
                int newIndex = activeIndex - 1;
                if (newIndex < 0)
                {
                    newIndex = this.Weapons.Count - 1;
                }

                this.Weapons[activeIndex].IsActive = false;
                this.Weapons[newIndex].IsActive = true;
            }
        }
        #endregion SwitchToPreviousWeapon

        #endregion Methods
    }
}