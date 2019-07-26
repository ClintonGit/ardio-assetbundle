using UnityEngine;
using System.Collections;
using Vuforia;

public class auto_focus : MonoBehaviour {

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		bool focusModeSet = CameraDevice.Instance.SetFocusMode( 
		CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		
		if (!focusModeSet) {
			Debug.Log("Failed to set focus mode (unsupported mode).");
		}
		
	}
}