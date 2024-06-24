using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.WeaponSystem
{
    public class BulletPool : BaseBehaviour
    {
        #region Properties

        public List<BulletInfo> BulletPrefabs;

        private Dictionary<string, List<Bullet>> buffer = new Dictionary<string, List<Bullet>>();

        #endregion Properties

        #region Methods

        #region GetBullet: returns an inactive bullet instance
        /// <summary>
        /// returns an inactive bullet instance
        /// </summary>
        /// <param name="bulletPrefabName">the name of the prefab</param>
        /// <returns>an inactive bullet instance, needs to be activated manually</returns>
        internal GameObject GetBullet(string bulletPrefabName)
        {
            GameObject result = default;
            if (!this.IsInactiveBulletAvailable(bulletPrefabName))  // check, if an inactive but instantiated bullet is available to save instantiation time
            {
                // create new bullet instance
                GameObject bulletPrefab = this.GetPrefab(bulletPrefabName);
                if (bulletPrefab != null)
                {
                    result = this.container.InstantiatePrefab(bulletPrefab);
                    result.transform.parent = this.gameObject.transform;  // use as container
                    this.AddToBuffer(bulletPrefabName, result);
                    result.SetActive(false);
                }
                else
                {
                    this.logService.Warning("BulletPool", $"GetBullet({bulletPrefabName})", $"Could not find a registered prefab with the name '{bulletPrefabName}'");
                }
            }
            else
            {
                // use an existing bullet instance
                result = this.GetInactiveBullet(bulletPrefabName);
            }
            return result;
        }
        #endregion GetBullet

        #region GetInfo
        internal BulletInfo GetInfo(string bulletPrefabName)
        {
            BulletInfo result = default;

            if (this.BulletPrefabs.Any(x => x.Name.Equals(bulletPrefabName)))
            {
                result = this.BulletPrefabs.Where(x => x.Name.Equals(bulletPrefabName)).FirstOrDefault();
            }

            return result;
        }
        #endregion GetInfo

        #region GetPrefab
        private GameObject GetPrefab(string bulletPrefabName)
        {
            GameObject result = default;

            if (this.BulletPrefabs.Any(x => x.Name.Equals(bulletPrefabName)))
            {
                result = this.BulletPrefabs.Where(x => x.Name.Equals(bulletPrefabName)).FirstOrDefault().BulletPrefab;
            }

            return result;
        }
        #endregion GetPrefab

        #region AddToBuffer: Adds a new Bullet to the buffer and names it accordingly
        /// <summary>
        /// Adds a new Bullet to the buffer and names it accordingly
        /// </summary>
        /// <param name="bulletPrefabName">The name for reference</param>
        /// <param name="bullet">the bullet gameobject itself</param>
        private void AddToBuffer(string bulletPrefabName, GameObject bullet)
        {
            if (!this.buffer.ContainsKey(bulletPrefabName))
            {
                this.buffer.Add(bulletPrefabName, new List<Bullet>());  // create buffer list if no instance of the prefab exists
            }

            this.buffer[bulletPrefabName].Add(bullet.GetComponent<Bullet>());  // add the bullet component to the buffer
            bullet.name = $"{bulletPrefabName} #{this.buffer[bulletPrefabName].Count}";  // set name to something speaking
        }
        #endregion AddToBuffer

        #region FixedUpdate
        private void FixedUpdate()
        {
            this.CheckActiveBulletsAndDeactivateIfNecessary();
        }
        #endregion FixedUpdate

        #region CheckActiveBulletsAndDeactivateIfNecessary
        private void CheckActiveBulletsAndDeactivateIfNecessary()
        {
            List<Bullet> deadBullets = new List<Bullet>();
            foreach (KeyValuePair<string, List<Bullet>> keyValuePair in this.buffer)
            {
                foreach (Bullet activeBullet in keyValuePair.Value)
                {
                    if (activeBullet.gameObject.activeSelf && activeBullet.ShouldBeDead())
                    {
                        deadBullets.Add(activeBullet);
                    }
                }
            }

            foreach (Bullet deadBullet in deadBullets)
            {
                deadBullet.gameObject.SetActive(false);
            }

            deadBullets.Clear();  // just in case
        }
        #endregion CheckActiveBulletsAndDeactivateIfNecessary

        #region IsInactiveBulletAvailable: checks, if there is any not active bullet game object in the buffer
        /// <summary>
        /// checks, if there is any not active bullet game object in the buffer
        /// </summary>
        /// <param name="bulletPrefabName">the name of the needed bullet prefab</param>
        /// <returns>true, if a not active bullet game object is in the buffer with the prefab name</returns>
        private bool IsInactiveBulletAvailable(string bulletPrefabName)
        {
            bool result = false;

            if (this.buffer.ContainsKey(bulletPrefabName) && this.buffer[bulletPrefabName].Any(x => x.gameObject.activeSelf.Equals(false)))
            {
                result = true;
            }

            return result;
        }
        #endregion IsInactiveBulletAvailable

        #region GetInactiveBullet
        /// <summary>
        /// checks if an inactive bullet is available and then returns the first available one
        /// </summary>
        /// <param name="bulletPrefabName">the name of the needed bullet prefab</param>
        /// <returns>an inactive bullet instance, null if none is available (should only happen if pool usage is very high so that instances are quicker being used than checks being made)</returns>
        private GameObject GetInactiveBullet(string bulletPrefabName)
        {
            GameObject result = default;

            if (this.buffer.ContainsKey(bulletPrefabName) && this.buffer[bulletPrefabName].Any(x => x.gameObject.activeSelf.Equals(false)))  // double check, yes
            {
                result = this.buffer[bulletPrefabName].Where(x => x.gameObject.activeSelf.Equals(false)).FirstOrDefault().gameObject;
            }

            return result;
        }
        #endregion GetInactiveBullet

        #endregion Methods
    }
}
