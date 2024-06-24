using NET.efilnukefesin.Lib.Common.Interfaces.Ai;
using NET.efilnukefesin.Lib.Common.Interfaces.Services;
using NET.efilnukefesin.Lib.Common.Services.Ai.DataClassificationRules;
using NET.efilnukefesin.Lib.Common.Services.Ai.Objects;
using NET.efilnukefesin.Unity.Base.Ai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Base.Scripts.Classes.Ai
{
    public class DistanceClassificationRule : BaseClassificationRule
    {
        #region Properties

        #endregion Properties

        #region Construction
        public DistanceClassificationRule(IObjectService ObjectService)
            : base(ObjectService)
        {

        }
        #endregion Construction

        #region Methods

        #region Classify
        public override IEnumerable<IClassifiedData> Classify(IInformation Information)
        {
            IClassifiedData resultX = default;
            IClassifiedData resultY = default;
            IClassifiedData resultZ = default;
            if (Information.Source.Equals("PositionInputProcessor"))
            {
                PositionInfo informationValue = (Information.Data as PositionInfo);

                if (informationValue != null)
                {
                    resultX = this.ObjectService.Create<ClassifiedData>("DistanceInputX", Information);
                    resultY = this.ObjectService.Create<ClassifiedData>("DistanceInputY", Information);
                    resultZ = this.ObjectService.Create<ClassifiedData>("DistanceInputZ", Information);

                    // return one classified data set per axis
                    Vector3 delta = informationValue.GetDelta();

                    resultX.SetClassValue(this.ClassifyDistance(delta.x));
                    resultY.SetClassValue(this.ClassifyDistance(delta.y));
                    resultZ.SetClassValue(this.ClassifyDistance(delta.z));
                }
            }
            yield return resultX;
            yield return resultY;
            yield return resultZ;
        }
        #endregion Classify

        #region ClassifyDistance
        private string ClassifyDistance(float Value)
        {
            string result = string.Empty;

            float value = Math.Abs(Value);

            //TODO: revisit values
            if (value > 15f)
            {
                result = "Far";
            }
            else if (value < 2f)
            {
                result = "Close";
            }
            else
            {
                result = "Medium";
            }

            return result;
        }
        #endregion ClassifyDistance

        #endregion Methods

        #region Events

        #endregion Events
    }
}
