using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//-----------------------------------------------------------------------------
// <summary>
// AssetBundleのビルドを行う
// </summary>
// <author>
// Katsumasa Kimura
// </author>
//-----------------------------------------------------------------------------
public static class AssetBundleEditor {
	
	[MenuItem("AssetBundle/Build")]
	static void Build(){
		var path = AssetBundleOutputPath ();
		if (!System.IO.Directory.Exists (path)) {
			System.IO.Directory.CreateDirectory (path);
		}
		BuildPipeline.BuildAssetBundles (path, BuildAssetBundleOptions.ChunkBasedCompression,EditorUserBuildSettings.activeBuildTarget);
	}

	/// <summary>
	/// AssetBundleを出力するパスを取得する
	/// </summary>
	/// <returns>The bundle output path.</returns>
	static string AssetBundleOutputPath(){		
		var path = Application.streamingAssetsPath + "/AssetBundle";


		switch (EditorUserBuildSettings.activeBuildTarget) {
		case BuildTarget.StandaloneWindows:
		case BuildTarget.StandaloneWindows64:
			return path + "/Windows";
		
		case BuildTarget.StandaloneOSX:
			return path + "/OSX";

		case BuildTarget.Android:
			return path + "/Android";

		case BuildTarget.iOS:
			return path + "/iOS";

		default:
			return path + "etc";
		}
			
	}

}
