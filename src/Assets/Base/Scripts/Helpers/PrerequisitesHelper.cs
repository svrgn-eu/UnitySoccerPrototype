using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base.Helpers
{
    public static class PrerequisitesHelper
    {
        #region Methods

        #region CheckRequiredAttributes
        public static bool CheckRequiredAttributes(BaseBehaviour BehaviourToCheck)
        {
            bool result = false;

            Type behaviourType = BehaviourToCheck.GetType();
            result = behaviourType.FieldsWithAttributeHasValue<RequiredAttribute>(BehaviourToCheck);

            if (!result)
            {
                //TODO: required field is empty, add log or error entry
            }
            else
            {
                //TODO: required field has a value, add log or error entry
                result = true;
            }

            return result;
        }
        #endregion CheckRequiredAttributes

        #endregion Methods
    }
}
