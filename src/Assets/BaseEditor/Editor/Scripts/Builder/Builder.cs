using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class Builder
{
    #region Constants

    private static string scenePathSuffix = ".unity";

    #endregion Constants

    #region Methods

    #region Windows: creates a full build for the windows platform
    /// <summary>
    /// creates a full build for the windows platform
    /// </summary>
    /// <returns>0 if succeeded, other codes when failed</returns>
    public static int Windows()
    {
        // see also: https://smashriot.com/unity-build-automation-with-jenkins/#unity-build-setup
        int result = -1;

        //parse incoming parameters
        BuilderCommandLineProperties builderCommandLineProperties = BuilderCommandLineProperties.GetCommandLineProperties();
        Debug.Log($"Builder.Windows(): Environment.CurrentDirectory - {Environment.CurrentDirectory}");  //something like Builder.Windows(): Environment.CurrentDirectory - C:\Jenkins\workspace\Unity\Unity IsoMultiplayerDemo alpha\src
        
        if (result == -1 && !builderCommandLineProperties.IsError)
        {
            try
            {
                List<string> sceneNames = new List<string>();

                string scenePathPrefix = $"Assets/{builderCommandLineProperties.UnityProjectName}/Scenes/";

                string startSceneName = $"{scenePathPrefix}{builderCommandLineProperties.StartSceneName}{Builder.scenePathSuffix}";
                sceneNames.Add(startSceneName);
                Debug.Log($"Builder.Windows(): added starting scene '{startSceneName}'");

                foreach (string sceneName in Builder.GetAllScenes(Path.Combine(Environment.CurrentDirectory, scenePathPrefix)))
                {
                    string sceneNameInProjectPath = Path.Combine(scenePathPrefix, sceneName.Replace("..\\", ""));
                    if (!sceneNameInProjectPath.Equals(startSceneName))
                    {
                        sceneNames.Add($"{sceneNameInProjectPath}");
                        Debug.Log($"Builder.Windows(): discovered and added Scene '{sceneNameInProjectPath}'");
                    }
                }

                string pathToDeploy = Path.Combine(builderCommandLineProperties.Path, $"{builderCommandLineProperties.UnityProjectName}.exe");
                Debug.Log($"Builder.Windows(): pathToDeploy - {pathToDeploy}");

                BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
                buildPlayerOptions.scenes = sceneNames.ToArray();
                buildPlayerOptions.locationPathName = pathToDeploy;
                buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
                buildPlayerOptions.options = BuildOptions.None;

                BuildReport buildReport= BuildPipeline.BuildPlayer(buildPlayerOptions);
                BuildSummary summary = buildReport.summary;

                switch (summary.result)  //check for the result
                {
                    case BuildResult.Unknown:
                        Debug.Log("Builder.Windows(): Unknown build result");
                        result = 1;
                        break;
                    case BuildResult.Succeeded:
                        Debug.Log("Builder.Windows(): Build succeeded: " + summary.totalSize + " bytes");
                        result = 0;
                        break;
                    case BuildResult.Failed:
                        Debug.Log("Builder.Windows(): Build failed");
                        result = 1;
                        break;
                    case BuildResult.Cancelled:
                        Debug.Log("Builder.Windows(): Build cancelled");
                        result = 1;
                        break;
                    default:
                        result = -1;
                        break;
                }

                if (result == 0)
                {
                    //build successful
                    System.Console.WriteLine("[JenkinsBuild] Build Success: Time:" + summary.totalTime + " Size:" + summary.totalSize + " bytes");
                }
                else
                {
                    System.Console.WriteLine("[JenkinsBuild] Build Failed: Time:" + summary.totalTime + " Total Errors:" + summary.totalErrors);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Builder.Windows(): Exception thrown - {ex.Message}");
                result = 2;
            }
        }

        return result;
    }
    #endregion Windows

    //TODO: move to helper class
    #region GetAllScenes
    private static List<string> GetAllScenes(string path)
    {
        List<string> result = new List<string>();
        foreach (string file in Directory.EnumerateFiles(path, $"*{Builder.scenePathSuffix}", SearchOption.AllDirectories))
        {
            string relativeFilepath = Builder.GetRelativePath(file, path);
            Debug.Log($"Builder.GetAllScenes(): found file under '{path}': '{file}' with relative path '{relativeFilepath}'");
            result.Add(relativeFilepath);
        }
        return result;
    }
    #endregion GetAllScenes

    //TODO: move to helper class
    #region GetRelativePath
    private static string GetRelativePath(string filespec, string folder)
    {
        Uri pathUri = new Uri(filespec);
        // Folders must end in a slash
        if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            folder += Path.DirectorySeparatorChar;
        }
        Uri folderUri = new Uri(folder);
        return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
    }
    #endregion GetRelativePath

    #endregion Methods
}
