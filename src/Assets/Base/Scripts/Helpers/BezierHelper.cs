using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.Helpers
{
    public static class BezierHelper
    {
        // adaption from http://www.theappguruz.com/blog/bezier-curve-in-games

        #region Methods

        #region CalculateLinearBezierPoint: Calculate a point on a linear Bezier 'curve'
        /// <summary>
        /// Calculate a point on a linear Bezier 'curve'
        /// </summary>
        /// <param name="t">0-1, indicates the position of the result between Point0 and Point1</param>
        /// <param name="Start">The first and starting point of the line</param>
        /// <param name="End">The last and ending point of the line</param>
        /// <returns></returns>
        public static Vector3 CalculateLinearBezierPoint(float t, Vector3 Start, Vector3 End)
        {
            Vector3 result = Start + t * (End - Start);
            return result;
        }
        #endregion CalculateLinearBezierPoint

        #region CalculateQuadraticCurveBezierPoint
        public static Vector3 CalculateQuadraticCurveBezierPoint(float t, Vector3 Start, Vector3 End, Vector3 Pivot)
        {
            // B(t) = (1-t)²P0 + 2(1-t)tP1 + t²P2
            //          uSquared    u        tSquared
            Vector3 result;

            float u = 1f - t;
            float tSquared = t * t;
            float uSquared = u * u;
            result = uSquared * Start + 2 * u * t * Pivot + tSquared * End;

            return result;
        }
        #endregion CalculateQuadraticCurveBezierPoint

        #region CalculateCubicCurveBezierPoint
        public static Vector3 CalculateCubicCurveBezierPoint(float t, Vector3 Start, Vector3 End, Vector3 Pivot0, Vector3 Pivot1)
        {
            // B(t) = (1-t)³P0 + 3(1-t)²tP1 + 3(1-t)t²P2 + t³P3
            //         uCubed      uSquared       u     tCubed
            Vector3 result;

            float u = 1f - t;
            float tSquared = t * t;
            float tCubed = tSquared * t;
            float uSquared = u * u;
            float uCubed = uSquared * u;
            result = uCubed * Start + 3 * uSquared * t * Pivot0 + 3 * u * tSquared * Pivot1 + tCubed * End;

            return result;
        }
        #endregion CalculateCubicCurveBezierPoint

        #endregion Methods
    }
}