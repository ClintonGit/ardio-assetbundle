using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {
	public Animator anim;
	public bool Open;
	public void PlayAnim (){
		if (Open) {
			anim.Play ("Close");
			Open = false;
		} else {
			anim.Play ("Open");
			Open = true;
		}

	}
}
