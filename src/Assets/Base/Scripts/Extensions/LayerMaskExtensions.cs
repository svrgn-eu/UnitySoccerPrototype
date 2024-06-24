using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base
{
    public static class LayerMaskExtensions
    {
        #region ToLayer
        public static int ToLayer(this LayerMask layerMask)
        {
            // from: https://answers.unity.com/questions/1288179/layer-layermask-which-is-set-in-inspector.html
            int result = (int)Mathf.Log(layerMask.value, 2);
            return result;
        }
        #endregion ToLayer
    }
}
