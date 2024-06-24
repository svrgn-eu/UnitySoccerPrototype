﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

public static class SerializedObjectExtensions
{
    #region GetSerializedValue
    public static T GetSerializedValue<T>(this SerializedProperty property)
    {
        // from https://forum.unity.com/threads/get-a-general-object-value-from-serializedproperty.327098/
        object @object = property.serializedObject.targetObject;
        string[] propertyNames = property.propertyPath.Split('.');

        List<string> propertyNamesClean = new List<String>();

        for (int i = 0; i < propertyNames.Count(); i++)
        {
            if (propertyNames[i] == "Array")
            {
                if (i != (propertyNames.Count() - 1) && propertyNames[i + 1].StartsWith("data"))
                {
                    int pos = int.Parse(propertyNames[i + 1].Split('[', ']')[1]);
                    propertyNamesClean.Add($"-GetArray_{pos}");
                    i++;
                }
                else
                    propertyNamesClean.Add(propertyNames[i]);
            }
            else
                propertyNamesClean.Add(propertyNames[i]);
        }
        // Get the last object of the property path.
        foreach (string path in propertyNamesClean)
        {
            if (path.StartsWith("-GetArray"))
            {
                string[] split = path.Split('_');
                int index = int.Parse(split[split.Count() - 1]);
                IList l = (IList)@object;
                @object = l[index];
            }
            else
            {
                @object = @object.GetType()
                    .GetField(path, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .GetValue(@object);
            }
        }

        return (T)@object;
    }
    #endregion GetSerializedValue
}

