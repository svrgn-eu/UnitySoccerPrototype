using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.Helpers
{
    public static class UnityMathHelper
    {
        #region LissajousCurve
        public static Vector3 LissajousCurve(float theta, float A, float delta, float B)
        {
            Vector3 result = Vector3.zero;
            result.x = Mathf.Sin(theta);
            result.y = A * Mathf.Sin(B * theta + delta);
            return result;
        }
        #endregion LissajousCurve
    }
}
