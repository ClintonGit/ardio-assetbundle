using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenceManage : MonoBehaviour {

    public string linkurl;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void GotoScence(string name) {
        Application.LoadLevel(name);
    }

    public void GotoLink(string url)
    {
        Application.OpenURL(url);
    }

    public void DynamicLink() {
        Application.OpenURL(linkurl);
    }


}
