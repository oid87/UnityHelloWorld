using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Builder : Editor
{

    [MenuItem("Build/APK")]
    public static void Build()
    {
        BuildTarget buildTarget = BuildTarget.Android;
        // 切換到Android平台
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

        List<string> levels = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            // 獲取有效的場景
            levels.Add(scene.path);
        }

        // 打包出 APK 名
        string apkName = string.Format("./{0}.apk", "Test");
        // 執行打包
	    //string res = BuildPipeline.BuildPlayer(levels.ToArray(), apkName, buildTarget, BuildOptions.None);
	    BuildPipeline.BuildPlayer(levels.ToArray(), apkName, buildTarget, BuildOptions.None);
	    
        AssetDatabase.Refresh();
    }
}