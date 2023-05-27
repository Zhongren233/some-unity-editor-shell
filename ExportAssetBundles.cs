using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            const string assetBundleDirectory = "Assets/AssetBundles";
            if(!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            var assetBundleManifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
                BuildAssetBundleOptions.None, 
                BuildTarget.iOS);
            Debug.Log(assetBundleManifest.GetAssetBundleHash("3dlive/livemotions/dateplanatoz"));

        }
    }
}