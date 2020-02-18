using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SandBox : MonoBehaviour
{
    AssetBundle assetBundleCommonShaders;    
    GameObject[] gameObjects;
    string[] assetBundleNames =
    {
        "assetbundlecube",
        "assetbundlesphere",
        "assetbundlecylinder",
    };

    string[] assetNames =
    {
        "CubePrefab",
        "SpherePrefab",
        "CylinderPrefab",
    };


    // Start is called before the first frame update
    void Start()
    {
        string fpath;
        // 共通ShaderのAssetBundleをLoadだけしておく
        fpath = GetAssetBundlePath() + "/assetbundlecommonshaders";
        assetBundleCommonShaders = AssetBundle.LoadFromFile(fpath);

        // 3DオブジェクトのPrefabをAssetBundleから読み込んでInstantiateする        
        gameObjects = new GameObject[assetNames.Length];
        for(var i = 0; i < assetBundleNames.Length; i++) {
            fpath = GetAssetBundlePath() + "/" + assetBundleNames[i];
            var assetBundle = AssetBundle.LoadFromFile(fpath);        
            var prefab = assetBundle.LoadAsset(assetNames[i]);
            gameObjects[i] = Instantiate(prefab) as GameObject;
            gameObjects[i].transform.localPosition = new Vector3(-1.0f + i, 0, 0);
            assetBundle.Unload(false);
        }        
    }


    // 後始末
    private void OnDestroy()
    {
        if(gameObjects != null)
        {
            for(var i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null)
                {
                    GameObject.Destroy(gameObjects[i]);
                    gameObjects[i] = null;
                }
            }
            gameObjects = null;
        }

        if(assetBundleCommonShaders != null)
        {
            assetBundleCommonShaders.Unload(false);
            assetBundleCommonShaders = null;
        }
    }


    // AssetBundleが格納しているフォルダーへのパスを取得する
    string GetAssetBundlePath()
    {
        var path = Application.streamingAssetsPath + "/AssetBundle";
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                return path + "/Windows";
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                return path + "/OSX";
            case RuntimePlatform.Android:
                return path + "/Android";
            case RuntimePlatform.IPhonePlayer:
                return path + "/iOS";
            default:
                return path + "etc";
        }
    }
}
