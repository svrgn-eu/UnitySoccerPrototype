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
    public class RelativePositionClassificationRule : BaseClassificationRule
    {
        #region Properties

        private float thresholdForMiddle = .25f;

        #endregion Properties

        #region Construction
        public RelativePositionClassificationRule(IObjectService ObjectService)
            : base(ObjectService)
        {

        }
        #endregion Construction

        #region Methods

        #region Classify
        public override IEnumerable<IClassifiedData> Classify(IInformation Information)
        {
            IClassifiedData result = default;
            if (Information.Source.Equals("PositionInputProcessor"))
            {
                PositionInfo informationValue = (Information.Data as PositionInfo);

                if (informationValue != null)
                {
                    result = this.ObjectService.Create<ClassifiedData>("RelativePositionInput", Information);
                    string classValue = "";

                    Vector3 delta = informationValue.GetDelta();

                    Vector3 normalizedDelta = Vector3.Scale(delta, informationValue.OwnRight);  // .Scale() multiplies each component of a vector to the corresponding component of the other vector (e.g. a.x * b.x, a.y * b.y, a.z * b.z)
                    if (normalizedDelta.x < 0f)
                    {
                        classValue = "Right";
                    }
                    else
                    {
                        classValue = "Left";
                    }

                    float thresholdNeededValue = Math.Abs(normalizedDelta.x);
                    if (thresholdNeededValue < this.thresholdForMiddle)
                    {
                        classValue = "Front";
                    }

                    result.SetClassValue(classValue);
                }
            }
            yield return result;
        }
        #endregion Classify

        #endregion Methods

        #region Events

        #endregion Events
    }
}
