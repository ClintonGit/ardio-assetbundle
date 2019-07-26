using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class detail : MonoBehaviour {

	public Animator anim;
	public Text txt_head;
	public Text txt_detail;
	public touch_btn tb;
    public AudioSource au;
    public AudioClip open;
    public AudioClip close;

	public void Open(string head,string detail){
		anim.Play("Open");
		txt_head.text = head;
		txt_detail.text = detail;
        au.Stop();
        au.clip = open;
        au.Play();
	}

	public void Close(){
		anim.Play("Close");
		txt_head.text = "";
		//tb.Close ();
        au.Stop();
        au.clip = close;
        au.Play();
    }


}
