using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class manageObject : MonoBehaviour
{
    [Header("ManageAR")]
    public manage mn;
    [Header("Link")]
    public GameObject[] link;
    [Header("Object")]
    public GameObject obj;
    [Header("Audio")]
    public bool isAudio;
    public AudioSource au;
    [Header("Audio")]
    public bool isVideo;
    public VideoPlayer mp;


    public void SetObject() {
        ShowLink();
        mn.objlock = obj;

        if (isAudio) {
            mn.au = au;
        }

        if (isVideo) {
            mn.mp = mp;
        }
    }


    public void ShowLink()
    {
        for (int i = 0; i < link.Length; i++)
        {
            link[i].SetActive(true);
        }
    }

    public void HideLink()
    {
        for (int i = 0; i < link.Length; i++)
        {
            link[i].SetActive(false);
        }
    }



}
