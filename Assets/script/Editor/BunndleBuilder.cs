#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BunndleBuilder : Editor {
	[MenuItem("Window/AssetBundle/BuildAssetBundles")]

	static void BuildAllAssetBundles(){
		BuildPipeline.BuildAssetBundles ("Assets/ExportAsset/Android",BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.Android);

	}
}
#endif