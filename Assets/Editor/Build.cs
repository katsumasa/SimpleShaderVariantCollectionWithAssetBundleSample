using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;


public class Build : MonoBehaviour
{
#if ENABLE_SHADER_LIVE_LINK
    [MenuItem("Build/Android(ShaderLivelinkSupport)")]
    public static void BuildAndroid()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/SampleScene.unity"};
        buildPlayerOptions.locationPathName = "binary.apk";
        buildPlayerOptions.target = BuildTarget.Android;

        // ShaderLivelinkSupportÇÕDevelopmentÇ∆ConnectToHostÇÃóºï˚Ç∆ÇÃëgÇ›çáÇÌÇπÇ™ïKê{
        buildPlayerOptions.options = BuildOptions.AutoRunPlayer|BuildOptions.Development|BuildOptions.ShaderLivelinkSupport| BuildOptions.ConnectToHost;
        

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            Debug.Log("outputpath " + summary.outputPath);
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
#endif
}
