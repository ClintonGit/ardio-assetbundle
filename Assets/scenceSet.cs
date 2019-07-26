using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenceSet : MonoBehaviour {

	public bool portrait;

	void Start () {
		if (portrait) {
			Screen.orientation = ScreenOrientation.Portrait;	
		} else {
			Screen.orientation = ScreenOrientation.LandscapeLeft;	
		}
	}
	

}
