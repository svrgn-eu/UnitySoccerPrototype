using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HelpBoxAttribute))]
public class HelpBoxDrawer : PropertyDrawer
{
    //from: https://diegogiacomelli.com.br/unitytips-helpbox-attribute/
    #region Constants

    const float XPadding = 30f;
    const float YPadding = 5f;
    const float DefaultHeight = 20f;
    const float DocsButtonHeight = 20f;

    #endregion Constants

    #region Properties

    float height;

    #endregion Properties

    #region Methods

    #region OnGUI
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = attribute as HelpBoxAttribute;
        CalculateHeight(attr);

        EditorGUI.PropertyField(position, property, label, true);

        position = new Rect(
            XPadding,
            position.y + EditorGUI.GetPropertyHeight(property, label, true) + YPadding,
            position.width - XPadding,
            this.height);

        EditorGUI.HelpBox(position, attr.Text, (MessageType)attr.Type);

        if (!string.IsNullOrEmpty(attr.DocsUrl))
        {
            position = new Rect(
                position.x + position.width - 40,
                position.y + position.height - DocsButtonHeight,
                40,
                DocsButtonHeight);


            if (GUI.Button(position, "Docs"))
            {
                if (attr.DocsUrl.StartsWith("http"))
                {
                    Application.OpenURL(attr.DocsUrl);
                }
                else
                {
                    Application.OpenURL($"https://docs.unity3d.com/ScriptReference/{attr.DocsUrl}");
                }
            }
        }
    }
    #endregion OnGUI

    #region GetPropertyHeight
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true) + height + 10;
    }
    #endregion GetPropertyHeight

    #region CalculateHeight
    private void CalculateHeight(HelpBoxAttribute attr)
    {
        this.height = (attr.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Length + 1) * DefaultHeight;

        if (!string.IsNullOrEmpty(attr.DocsUrl))
        {
            this.height += DocsButtonHeight;
        }
    }
    #endregion CalculateHeight

    #endregion Methods
}

