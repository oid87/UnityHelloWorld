#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;

static class JenkinsBuilder
{
    [MenuItem("Auto Builder/Build Win64")]
    public static void PerformBuildWin64()
    {
        var path = string.Format("Builds/Win64/{0:yyyy-MM-dd-hh-mm-ss}", DateTime.Now);

        if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
        {
            path = GetBatchOutputPath();

            if (path == null)
            {
                throw new ArgumentException("-output is not set");
            }
        }

        PerformBuild(path, BuildTarget.StandaloneWindows64);
    }
    static void PerformBuild(string path, BuildTarget target)
    {
        var scenes = EditorBuildSettings.scenes.Where((v) => { return v.enabled; }).Select((v) => { return v.path; });
        BuildPipeline.BuildPlayer(scenes.ToArray(), path, target, BuildOptions.None);
    }

    static string GetBatchOutputPath()
    {
        var args = Environment.GetCommandLineArgs();
        for (int ii = 0; ii < args.Length; ++ii)
        {
            if (args[ii].ToLower() == "-output")
            {
                if (ii + 1 < args.Length)
                {
                    return args[ii + 1];
                }
            }
        }

        return null;
    }
}
#endif