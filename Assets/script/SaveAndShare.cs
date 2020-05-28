using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SA.CrossPlatform.Social;
using SA.CrossPlatform.UI;
using SA.Foundation.Templates;
using SA.Foundation.Utility;
using SA.CrossPlatform.App;


public class SaveAndShare : MonoBehaviour {

	public Texture2D Screenshot;
	public Texture2D hello_texture;
	public Texture2D darawTexgture = null;
	private GUIStyle style;
	private GUIStyle style2;
	public Texture2D textureForPost;

	public GameObject CanvasUI;
	public GameObject CanvasChoice;
	public GameObject Share;
	public RawImage img;
	public Sprite imgs;



	public void SaveTogal() {
        //Generating sample red texture with 32x32 resolution
        var gallery = UM_Application.GalleryService;
        gallery.SaveImage(Screenshot, "sample_black_image.png", (result) =>
        {
            if (result.IsSucceeded)
            {
                UM_DialogsUtility.ShowMessage("Result", "Sharing Completed.");
            }
            else
            {
                Debug.Log("Failed to save an Image: " + result.Error.FullMessage);
            }
        });
    }


	public void SaveToGallery(){

	   var permission = NativeGallery.SaveImageToGallery( Screenshot, "SaveImageSuccess", "Image.png" );
	   Debug.LogWarning(permission);
	   
	}

	public void CloseCapture(){
		EnableUI ();
		Share.SetActive (false);
	}
	

	public void DisbleUI() {
		CanvasUI.SetActive(false);
//		CanvasChoice.SetActive (false);

	}

	public void EnableUI() { 
		CanvasUI.SetActive(true);
	}


	public void Capture(){
		DisbleUI();
		Screenshot = new Texture2D(Screen.width, Screen.height);
		StartCoroutine(CaptureScreen());

	}
    

	IEnumerator CaptureScreen() {
		yield return new WaitForEndOfFrame();
		Screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
		Screenshot.Apply();
		img.texture = Screenshot;
		Share.SetActive (true);
	}

    /*
	public void GetGallery() { 
			UM_Camera.Instance.OnImagePicked += OnImage;
			UM_Camera.Instance.GetImageFromGallery();
	}
    */

    public void ShareSystem() {
        var client = UM_SocialService.SharingClient;
        client.SystemSharingDialog(MakeSharingBuilder(), PrintSharingResult);
    }

    private UM_ShareDialogBuilder MakeSharingBuilder()
    {
        var builder = new UM_ShareDialogBuilder();
        builder.AddImage(Screenshot);
        return builder;
    }

    public void CaptureAndShare() {
		StartCoroutine(PostScreenshot());
	}

    public static void PrintSharingResult(SA_Result result)
    {
        if (result.IsSucceeded)
        {
            //UM_DialogsUtility.ShowMessage("Result", "Sharing Completed.");
            Debug.Log("Sharing Completed.");
        }
        else
        {
            //UM_DialogsUtility.ShowMessage("Result", "Failed to share: " + result.Error.FullMessage);
            Debug.Log("Failed to share: " + result.Error.FullMessage);
        }
    }


    private IEnumerator PostScreenshot()
	{
	yield return new WaitForEndOfFrame();
	int width = Screen.width;
	int height = Screen.height;
	Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
	tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
	tex.Apply();
	//UM_ShareUtility.ShareMedia("Title", "Some text to share", tex);

	Destroy(tex);}

    
}
