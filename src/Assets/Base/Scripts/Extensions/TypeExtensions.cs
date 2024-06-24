using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Unity.Base
{
    public static class TypeExtensions
    {
        #region Methods

        #region FieldsWithAttributeHasValue
        public static bool FieldsWithAttributeHasValue<AttributeType>(this Type type, object Instance) where AttributeType : Attribute
        {
            bool result = false;

            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                object[] attrs = field.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    AttributeType attribute = attr as AttributeType;
                    if (attribute != null)
                    {
                        string propName = field.Name;
                        bool hasValue = field.GetValue(Instance) != null;
                        if (!hasValue)
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }
        #endregion FieldsWithAttributeHasValue

        #endregion Methods
    }
}
