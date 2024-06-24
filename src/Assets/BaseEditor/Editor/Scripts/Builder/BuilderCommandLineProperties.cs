using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BuilderCommandLineProperties
{
    #region Properties

    public string AppName { get; private set; }
    public string UnityProjectName { get; private set; }
    public string StartSceneName { get; private set; }
    public string Path { get; private set; }
    public bool IsError { get; private set; } = true;

    #endregion Properties

    #region Construction

    public BuilderCommandLineProperties(bool IsError, string AppName = null, string UnityProjectName = null, string StartSceneName = null, string Path = null)
    {
        this.IsError = IsError;
        if (!this.IsError)
        {
            this.AppName = AppName;
            this.UnityProjectName = UnityProjectName;
            this.StartSceneName = StartSceneName;
            this.Path = Path;
        }
    }

    #endregion Construction

    #region Methods

    #region GetCommandLineProperties
    public static BuilderCommandLineProperties GetCommandLineProperties()
    {
        BuilderCommandLineProperties result = default;

        string appName = string.Empty;
        string unityProjectName = string.Empty;
        string startSceneName = string.Empty;
        string targetDir = string.Empty;

        // find: -executeMethod
        //   +1: Builder.Windows_Jenkins
        //   +2: Project Name (Jenkins)
        //   +3: Exe Name and Unity Subfolder name
        //   +4: Start Scene name
        //   +5: /Users/Shared/Jenkins/Home/jobs/VRDungeons/builds/47/output -> Output path
        string[] args = System.Environment.GetCommandLineArgs();

        int numberOfArgs = 3;
        
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-executeMethod")
            {
                if (i + numberOfArgs + 1 < args.Length)
                {
                    // name of Builder method is args[i+1]
                    appName = args[i + 2];
                    Debug.Log($"BuilderCommandLineProperties.GetCommandLineProperties(): AppName - {appName}");
                    unityProjectName = args[i + 3];
                    Debug.Log($"BuilderCommandLineProperties.GetCommandLineProperties(): UnityProjectName - {unityProjectName}");
                    startSceneName = args[i + 4];
                    Debug.Log($"BuilderCommandLineProperties.GetCommandLineProperties(): StartSceneName - {startSceneName}");
                    targetDir = args[i + 5];
                    Debug.Log($"BuilderCommandLineProperties.GetCommandLineProperties(): TargetDir - {targetDir}");
                    i += numberOfArgs;
                }
                else
                {
                    result = new BuilderCommandLineProperties(true);
                    Debug.LogError($"BuilderCommandLineProperties.GetCommandLineProperties(): Incorrect Parameters for -executeMethod Format: -executeMethod Builder.Windows_Jenkins <app name> <output dir>");
                }
            }
        }

        if (!string.IsNullOrEmpty(appName) || !string.IsNullOrEmpty(targetDir))
        {
            result = new BuilderCommandLineProperties(false, appName, unityProjectName, startSceneName, targetDir);
        }

        return result;
    }
    #endregion GetCommandLineProperties

    #endregion Methods
}
