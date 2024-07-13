using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Runtime.Assets.Runtime.Scripts.MigrateToHelpers
{
    public static class LayerMaskHelper
    {
        #region Contains: Returns true, if a layer is being within the given layermask
        /// <summary>
        /// Returns true, if a layer is being within the given layermask <see cref="https://discussions.unity.com/t/check-if-layer-is-in-layermask/16007"/>
        /// </summary>
        /// <param name="layermask">the layermask to check</param>
        /// <param name="layer">the layer</param>
        /// <returns>true, if the layer is contained in the layer mask, otherwise false</returns>
        internal static bool Contains(LayerMask layermask, int layer)
        {
            bool result = false;
            if (layermask == (layermask | (1 << layer)))
            {
                result = true;
            }
            return result;
        }
        #endregion Contains
    }
}
