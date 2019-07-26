using UnityEditor;
using UnityEngine;
using LitJson;
using System;
using System.Collections;

public class parseJSON
{
	public string title;
	public string id;
	public ArrayList but_title;
	public ArrayList but_image;
}

public class JSON_D : MonoBehaviour
{
	// Sample JSON for the following script has attached.
	public IEnumerator Start()
	{
		string url = "http://localhost/ARCloud/load_data.php?code=abcd";
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null)
		{
			
		}
		else
		{
			Debug.Log("ERROR: " + www.error);
		}        
	}    
}
