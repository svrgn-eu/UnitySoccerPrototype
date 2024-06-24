using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using NET.efilnukefesin.Unity.Base;

[CustomEditor(typeof(AiContainerDebugger))]
public class AiContainerDebuggerEditor : Editor
{
    #region Properties

    //SerializedProperty memoryCount;
    private AiContainerDebugger aiContainerDebugger;

    #endregion Properties

    #region Construction

    #endregion Construction

    #region Methods

    #region OnInspectorGUI
    public override void OnInspectorGUI()
    {
        this.aiContainerDebugger = this.target as AiContainerDebugger;
        this.ShowMemoryInfo();
        this.ShowSensorInfo();
        this.ShowDecisionInfo();
        this.ShowActorInfo();
        this.ShowDebugInfo();
    }
    #endregion OnInspectorGUI

    #region ShowMemoryInfo
    private void ShowMemoryInfo()
    {
        this.ShowHeader("Memory Info");
        if (this.aiContainerDebugger.DebugInfos != null)
        {
            EditorGUILayout.IntField("Memory Count", this.aiContainerDebugger.DebugInfos.MemoryCount);
            EditorGUILayout.TextField("Memory last updated at", this.aiContainerDebugger.DebugInfos.MemoryLastUpdatedAt.ToString());
        }
    }
    #endregion ShowMemoryInfo

    #region ShowSensorInfo
    private void ShowSensorInfo()
    {
        this.ShowHeader("Sensor Info");
        if (this.aiContainerDebugger.DebugInfos != null)
        {
            string sensorNames = string.Join("; ", this.aiContainerDebugger.DebugInfos.SensorNames);
            EditorGUILayout.TextField("Sensor Names", sensorNames);
        }
    }
    #endregion ShowSensorInfo

    #region ShowDecisionInfo
    private void ShowDecisionInfo()
    {
        this.ShowHeader("Decision Info");
        if (this.aiContainerDebugger.DebugInfos != null)
        {
            EditorGUILayout.IntField("Decision Tree Depth", this.aiContainerDebugger.DebugInfos.DecisionTreeDepth);
        }
    }
    #endregion ShowDecisionInfo

    #region ShowActorInfo
    private void ShowActorInfo()
    {
        this.ShowHeader("Actor Info");
        if (this.aiContainerDebugger.DebugInfos != null)
        {
            string sensorNames = string.Join("; ", this.aiContainerDebugger.DebugInfos.ActorNames);
            EditorGUILayout.TextField("Actor Names", sensorNames);
        }
    }
    #endregion ShowActorInfo

    #region ShowDebugInfo
    private void ShowDebugInfo()
    {
        this.ShowHeader("Debug Info");
        if (this.aiContainerDebugger.DebugInfos != null)
        {
            EditorGUILayout.TextField("Last Error", this.aiContainerDebugger.DebugInfos.LastErrorMessage);
        }
    }
    #endregion ShowDebugInfo

    #region ShowHeader
    private void ShowHeader(string Text)
    {
        EditorGUILayout.LabelField(Text, EditorStyles.boldLabel);
    }
    #endregion ShowHeader

    #endregion Methods

    #region Events

    #endregion Events


}

