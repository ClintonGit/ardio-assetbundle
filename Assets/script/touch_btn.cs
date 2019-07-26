using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class touch_btn : MonoBehaviour {

   
	public string Data;
	public string link;
	public string attribute;
    public manageObject mno;

	void Start(){

		StartCoroutine (getLink());
	}



    private void OnMouseDown()
    {
		Application.OpenURL (link);
    }


	public IEnumerator getLink(){

	
		
		WWWForm form = new WWWForm();
		form.AddField("content_id", mno.mn.namemarker);
		form.AddField("attribute",attribute);
		UnityWebRequest www = UnityWebRequest.Post(mno.mn.urlLink, form);

		yield return www.SendWebRequest();

		Debug.LogWarning (www.responseCode);

		if(www.isNetworkError || www.isHttpError) {
			Debug.LogError(www.error);
		}
		else {
			Debug.LogWarning("SendLink");

			Data = www.downloadHandler.text;
			Debug.LogWarning (www.downloadHandler.text);
			JSONObject jsonData = (JSONObject)JSON.Parse(Data);
			link = jsonData ["link"];
		}
	} 

	 


}
