using Assets.Base.Scripts.Services;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace NET.efilnukefesin.Unity.Base.Services
{
    public static class UnityObjectServiceExtensions
    {
        #region InstantiatePrefab
        public static GameObject InstantiatePrefab(this IObjectService Service, GameObject Prefab, Transform Parent)
        {
            GameObject result = default;
            result = ((UnityObjectService)Service).Instantiate(Prefab, Parent);
            return result;
        }
        #endregion InstantiatePrefab

        #region InstantiatePrefab
        public static GameObject InstantiatePrefab(this IObjectService Service, GameObject Prefab, Transform Parent, Vector3 Position)
        {
            GameObject result = default;
            UnityObjectService castedService = (UnityObjectService)Service;
            if (castedService != null)
            {
                result = castedService.Instantiate(Prefab, Parent, Position);
            }
            return result;
        }
        #endregion InstantiatePrefab
    }
}
