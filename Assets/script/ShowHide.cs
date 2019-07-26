using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHide : MonoBehaviour {

	public GameObject content;

	public void Hide(){
		content.SetActive (false);
	}

	public void Show(){
		content.SetActive (true);
	}

}
