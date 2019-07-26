using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockSystem : MonoBehaviour {

    public manage mn;
    public GameObject ListLock;
    public GameObject ObjLock;
    public GameObject MarkerObj;
    public GameObject MarkerObjMain;
    public string namemarker;
    public bool Locks;
    public GameObject close_btn;
    public Toggle lock_btn;
    public GameObject btn_link;
    


    public void Lock() {

        if (lock_btn.isOn) {
            Locks = true;
           // ListLock.GetComponent<manage_tranfrom>().enabled = true;
            ObjLock.transform.parent = ListLock.transform;
			MarkerObjMain.GetComponent<manage>().objlock.transform.localPosition = new Vector3(0,0,0);
			MarkerObjMain.GetComponent<manage>().objlock.transform.localEulerAngles = new Vector3(0,0,0);
			MarkerObjMain.GetComponent<manage>().objlock.transform.localScale = new Vector3(1,1,1);
            close_btn.SetActive(true);
        }

      
        
    }


    public void UnLock() {
		//ListLock.GetComponent<manage_tranfrom> ().enabled = false;
        if (Locks == true) {
            ObjLock.transform.parent = MarkerObj.transform;
            mn.obj.SetActive(false);
            Locks = false;

        }

        close_btn.SetActive(false);
        btn_link.GetComponent<scenceManage>().linkurl = null;
        btn_link.SetActive(false);
        Reset();
    }


    public void Reset()
    {
		ListLock.transform.localPosition = new Vector3(0,0,0);
		ListLock.transform.localEulerAngles = new Vector3(0,0,0);
		ListLock.transform.localScale = new Vector3(1,1,1);
		MarkerObjMain.GetComponent<manage>().objlock.transform.localPosition = new Vector3(0,0,0);
		MarkerObjMain.GetComponent<manage>().objlock.transform.localEulerAngles = new Vector3(0,0,0);
		MarkerObjMain.GetComponent<manage>().objlock.transform.localScale = new Vector3(1,1,1);
		mn.ResetModel();
           
    }
}
