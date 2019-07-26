using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class loadAsset : MonoBehaviour {


	public string nameAsset;
	public string path;
	public Text txt;

	public GameObject loading_obj;
	public Text loading;
	public GameObject content;
	private AssetBundle assets = null;

	void Start(){
		StartCoroutine (DownloadAsset());
		//StartCoroutine(LoadAssetBundle (path));
	}


	public IEnumerator DownloadAsset(){
		WWW www = new WWW (path);
		yield return www;
		AssetBundle assetBundle = www.assetBundle;
		Debug.LogWarning (assetBundle);
		Instantiate(assetBundle.LoadAsset(nameAsset));
	}
}



