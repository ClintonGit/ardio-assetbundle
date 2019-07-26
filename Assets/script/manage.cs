using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMP;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.Video;
using Vuforia;
using System.IO;

public class manage : MonoBehaviour, ITrackableEventHandler
{
    [Header("ManageAR")]
    public GameObject mainObj;
    public GameObject obj;
	public string namemarker;
	public LockSystem ls;
    public GameObject btn_link;
    public string link;
	public bool expire;
    public Transform tranobj;
	public GameObject Marker;
	public GameObject objlock;
    [Header("Video")]
    public bool isVdo;
    public VideoPlayer mp;
    public string Path_VDO;
    public string folder;
    public string urlDownload;
    public string namefile;
    [Header("ServerConfig")]
    public string urlLoad = "http://ardio.cgexportthai.com/api/load";
	public string urlCheck = "http://ardio.cgexportthai.com/api/check";
	public string urlLink = "http://ardio.cgexportthai.com/api/check";
    [Header("Audio")]
    public bool sound;
    public AudioSource au;
    [Header("AssetBundle")]
    public bool AssetBundleLoad;
    public string urlAsset;
    private AssetBundle assets = null;
    public GameObject content;
    public string namebundle;
    public int version;
    public GameObject Process;
    public Text txtProcess;
    private GameObject model;
 


    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;




    void Start() {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        SetReset ();
        ls.mn = this;
		StartCoroutine (Check());
    }


    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        /*Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);*/

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            StartCoroutine(Fond());
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Lost();
        }
        else
        {

            Lost();
        }
    }




    void SetReset()
    {
       
    }


    public void ResetModel()
    {
        objlock.transform.localEulerAngles = new Vector3(0, 0, 0);
        objlock.transform.localScale = new Vector3(1, 1, 1);

    }




	public IEnumerator Fond() {

		yield return Check ();

        if (AssetBundleLoad) {
            if (content == null)
            {
                yield return DownloadAsset();
            }
        }

       
       
        if (expire) {
			if (ls.Locks == true ) { 
				ls.UnLock();
			}

			ls.mn = this;
			ls.MarkerObjMain = this.gameObject;
			obj.SetActive(true);
			ls.namemarker = "";
			ls.namemarker = namemarker;
			ls.ObjLock = objlock;
			ls.MarkerObj = obj;
			StartCoroutine (Visit());
		}

		if(sound){
			au.Play ();
		}

        if (isVdo)
        {
            mp.Play();
        }

    }

    public void Lost() {

        if (ls.lock_btn.isOn) {
            if (namemarker == ls.namemarker)
            {
                ls.Lock();
            }
            else
            {
               // btn_link.SetActive(false);
                //btn_link.GetComponent<scenceManage>().linkurl = null;
                ResetModel();
                obj.SetActive(false);
				if(sound == true){
					au.Stop ();
				}
            }
        }
        else {
           // btn_link.GetComponent<scenceManage>().linkurl = null;
           // btn_link.SetActive(false);
            //ResetModel();
            obj.SetActive(false);
			if(sound == true){
				au.Stop ();
			}
        }




    }

    public void Stop() { 
     //au.Stop();
    }

    public void close(){

         if(isVdo == true){
            mp.url = Path_VDO;
            mp.Play();
			mp.Stop ();
        }


		if (ls.Locks == true ) { 
			ls.UnLock();
           
        }

    }


	public IEnumerator Visit(){
		WWWForm form = new WWWForm();
		form.AddField("id",namemarker);
		UnityWebRequest www = UnityWebRequest.Post(urlLoad, form);
		yield return www.SendWebRequest();

		//Debug.LogWarning (www.downloadedBytes);

		if(www.isNetworkError || www.isHttpError) {
			Debug.LogError(www.error);
		}
		else {
			Debug.LogWarning("Send");
		}
	}


    public IEnumerator DownloadAsset() {

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            urlAsset = urlAsset + "/IOS/" + namebundle;
        }
        else {
            urlAsset = urlAsset + "/Android/" + namebundle;
        }

        WWW request = WWW.LoadFromCacheOrDownload(urlAsset, version);


        while (!request.isDone)
        {
            Process.SetActive(true);
            txtProcess.text = "ดาวน์โหลดโมเดล  "+((int)(request.progress * 100.0f)).ToString() + " %";
            yield return null;
        }

        if (request.error == null)
        {
            assets = request.assetBundle;
            yield return Instantiates();
            Debug.LogWarning("SetAsset");

        }
        else
        {
            Debug.LogError(request.error);
        }
        Process.SetActive(false);
        yield return null;

    }


    public IEnumerator Instantiates() {

        if (isVdo)
        {
            yield return LoadVideo();
        }


        Process.SetActive(true);
        txtProcess.text = "รอสักครู่...";
        content = Instantiate(assets.LoadAssetAsync(namebundle).asset) as GameObject;
        content.transform.parent = mainObj.transform;
        content.GetComponent<manageObject>().mn = this;



        obj = content;
        if (expire) {
            content.GetComponent<manageObject>().SetObject();
            if (isVdo)
            {
                mp.url = Path_VDO;
            }
                content.transform.localPosition = new Vector3(0, 0, 0);
            content.transform.localEulerAngles = new Vector3(0, 0, 0);
            content.transform.localScale = new Vector3(1, 1, 1);
        }
        Process.SetActive(false);
        assets.Unload(false);
        yield return null;
    }


	public IEnumerator Check(){


		WWWForm form = new WWWForm();
		form.AddField("id",namemarker);
		UnityWebRequest www = UnityWebRequest.Post(urlCheck, form);
		yield return www.SendWebRequest();

		Debug.Log (www.downloadHandler.text);
		int value = Convert.ToInt32 (www.downloadHandler.text);

		if (value == 1) {
            //yield return DownloadAsset();
			expire = true;	
		} else {
			expire = false;	
		}


		if(www.isNetworkError || www.isHttpError) {
			Debug.LogError(www.error);
		}
		else {
			Debug.LogWarning("SendCheck");
			
		}
	}


    public IEnumerator LoadVideo()
    {

        Process.SetActive(true);
        string PathFolder = Application.persistentDataPath + "/" + folder;
        string PathFile = Application.persistentDataPath + "/" + folder + "/" + namefile;
        Debug.Log(PathFolder);
        if (System.IO.Directory.Exists(PathFolder))
        {

            if (System.IO.File.Exists(PathFile))
            {
                var fileinfo = new System.IO.FileInfo(PathFile);
                // Debug.LogWarning (fileinfo.Length);

                if (fileinfo.Length > 5000000)
                {
                    Debug.Log("HaveFile");
                    txtProcess.text = "HaveFile";
                    Process.SetActive(false);
                    PlayVideo();
                    yield return null;
                }
                else
                {
                    Debug.Log("No File");
                    txtProcess.text = "No File";
                    yield return LoadContent();
                }

            }
            else
            {
                Debug.Log("No File");
                txtProcess.text = "No File";
                yield return LoadContent();
            }

        }
        else
        {
            Directory.CreateDirectory(PathFolder);
            Debug.Log("Create Folder");

            if (System.IO.File.Exists(PathFile))
            {
                var fileinfo = new System.IO.FileInfo(PathFile);
                if (fileinfo.Length > 5000000)
                {
                    Debug.Log("HaveFile");
                    txtProcess.text = "HaveFile";
                    PlayVideo();
                }
                else
                {
                    Debug.Log("No File");
                    txtProcess.text = "No File";
                    yield return LoadContent();
                }

            }
            else
            {
                Debug.Log("No File");
                txtProcess.text = "No File";
                yield return LoadContent();
            }
        }


    }

    public IEnumerator LoadContent()
    {


        string PathFile = Application.persistentDataPath + "/" + folder + "/" + namefile;

        WWW request = new WWW(urlDownload);

        while (!request.isDone)
        {
           
            Debug.Log(request.progress * 100);
            txtProcess.text = "ดาวน์โหลดวีดีโอ " + ((int)(request.progress * 100.0f)).ToString() + " %";
            yield return null;
        }
        Debug.LogWarning(request.bytes);
        File.WriteAllBytes(PathFile, request.bytes);
        txtProcess.text = "รอโหลดวีดีโอ " + ((int)(request.progress * 100.0f)).ToString() + " %";
        StartCoroutine(LoadVideo());
    }


    public void PlayVideo()
    {
        Process.SetActive(true);
        string PathFolder = Application.persistentDataPath + "/" + folder;
        string PathFile = Application.persistentDataPath + "/" + folder + "/" + namefile;

        Debug.Log(PathFolder);
        if (System.IO.Directory.Exists(PathFolder))
        {

            if (System.IO.File.Exists(PathFile))
            {
                var fileinfo = new System.IO.FileInfo(PathFile);
                Debug.LogWarning(fileinfo.Length);
                if (fileinfo.Length > 1000000)
                {
                    Path_VDO = PathFile;
                    Process.SetActive(false);
                }
                else
                {
                    StartCoroutine(LoadVideo());
                }

            }

        }
    }


}

