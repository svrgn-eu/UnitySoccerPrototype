using NET.efilnukefesin.Lib.Common.Interfaces.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NET.efilnukefesin.Unity.Base.Ai
{
    public class PositionInfo : IBaseObject
    {
        #region Properties

        public Vector3 PositionOfObject { get; private set; }
        public Vector3 RightOfObject { get; private set; }
        public Vector3 OwnPosition { get; private set; }
        public Vector3 OwnRight { get; private set; }
        public string SemanticalName { get; private set; }

        #endregion Properties

        #region Construction

        public PositionInfo(Vector3 PositionOfObject, Vector3 RightOfObject, string SemanticalName, Vector3 OwnPosition, Vector3 OwnRight)
        {
            this.PositionOfObject = PositionOfObject;
            this.RightOfObject = RightOfObject;
            this.OwnPosition = OwnPosition;
            this.OwnRight = OwnRight;
            this.SemanticalName = SemanticalName;
        }

        #endregion Construction

        #region Methods

        #region GetDelta: returns the delta vector between both vectors
        /// <summary>
        /// returns the delta vector between both vectors
        /// </summary>
        /// <returns>the delta vector between both vectors</returns>
        public Vector3 GetDelta()
        {
            Vector3 result = this.OwnPosition - this.PositionOfObject;
            return result;
        }
        #endregion GetDelta

        #endregion Methods
    }
}
