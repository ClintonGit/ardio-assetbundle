using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage_tranfrom : MonoBehaviour {
	public GameObject Obj;
	public float zoomSpeed = 0.01f;
	public float SpeedPosition;
	public float scaleMin = 1f;
	public float scaleMax = 2f;


	public bool zoom;
	
	public bool isLimitY;
	public bool isTrackball;
    float limitY_min = 0;
	public float limitY_max = 90;
	public float h;
	public float w;
	public float speedOneFingerDragX = 0.1f;
	public float speedOneFingerDragY = 0.1f;
	public float speedTwoFingerDragX = 0.01f;
	public float speedTwoFingerDragY = 0.2f;

    [Header("Rotate")]
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;
    public bool Invert;

    [Header("Flip")]
    public bool isDragWithTwoFinger;


    private bool isdrag = false;
	private bool tap, swipeleft, swiperight, swipeup, swipedown;
	private Vector2 startTouch, swipeDelta;

	public Vector2 SwipeDelta { get { return swipeDelta; } }
	public bool SwipeLeft { get { return swipeleft; } }
	public bool SwipeRight { get { return swiperight; } }
	public bool SwipeUp { get { return swipeup; } }
	public bool SwipeDown { get { return swipedown; } }

   

	private void Reset() {
		startTouch = swipeDelta = Vector2.zero;
	}






		void Update(){
		if (Input.touchCount == 2) {

			if (zoom == true) {
				
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			Obj.transform.localScale = new Vector3(Mathf.Clamp (Obj.transform.localScale.x - deltaMagnitudeDiff* zoomSpeed, scaleMin, scaleMax),
			Mathf.Clamp (Obj.transform.localScale.y - deltaMagnitudeDiff* zoomSpeed, scaleMin, scaleMax),
				Mathf.Clamp (Obj.transform.localScale.z - deltaMagnitudeDiff* zoomSpeed, scaleMin, scaleMax));
			}
			


		}

        if (Input.touchCount == 1) {

            Debug.LogWarning("Touch");

            Touch touch = Input.GetTouch(0);
            w = speedOneFingerDragX * touch.deltaPosition.x;

            if (rotateX)
            {
                if (Invert)
                {
                    Obj.transform.Rotate(-w, 0, 0, Space.World);
                }
                else {
                    Obj.transform.Rotate(w, 0, 0, Space.World);
                }
               
            }

            if (rotateY)
            {
                if (Invert)
                {
                    Obj.transform.Rotate(0, -w, 0, Space.World);
                }
                else
                {
                    Obj.transform.Rotate(0, w, 0, Space.World);
                }

               
            }

            if (rotateZ)
            {
                if (Invert)
                {
                    Obj.transform.Rotate(0, 0, -w, Space.World);
                }
                else
                {
                    Obj.transform.Rotate(0, 0, w, Space.World);
                }

            }
        }

		/*if ( Input.touchCount == 1 )
		{
				Touch touch = Input.GetTouch (0);

				if ( isDragWithTwoFinger )
				{
					if ( touch.phase == TouchPhase.Moved )
					{
						w = speedOneFingerDragX * touch.deltaPosition.x;

						if ( invertX )
						{
							Obj.transform.Rotate (0, w, 0, Space.World);
						}
						else
						{
							Obj.transform.Rotate (0, -w, 0, Space.World);
						}
					}
				}
				else
				{
					if ( isTrackball )
					{
						if ( touch.phase == TouchPhase.Moved )
						{
							w = speedOneFingerDragX * touch.deltaPosition.x;
							h = speedOneFingerDragY * touch.deltaPosition.y;

							if ( invertX )
							{
								if ( invertY )
								{
									Obj.transform.Rotate (h, w, 0, Space.World);

								}
								else
								{

									Obj.transform.Rotate (-h, w, 0, Space.World);

								}
							}
							else
							{
								if ( invertY )
								{

									Obj.transform.Rotate (h, -w, 0, Space.World);
								}
								else
								{
									Obj.transform.Rotate (-h, -w, 0, Space.World);
								}
							}
						}
					}
					else
					{
						if ( touch.phase == TouchPhase.Moved )
						{
							w = speedOneFingerDragX * touch.deltaPosition.x;
							h = speedOneFingerDragY * touch.deltaPosition.y;

							if ( invertX )
							{
							Obj.transform.Rotate (0 , w, 0, Space.World);
							}
							else
							{
							Obj.transform.Rotate (0, -w, 0, Space.World);
							}
						}
					}
				}

			
			}else if(Input.touchCount == 2 && isDragWithTwoFinger)
		{
			Touch touch1 = Input.GetTouch(0);
Touch touch2 = Input.GetTouch(1);
			
			if(touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
			{
				Vector2 midpoint = (touch1.deltaPosition + touch2.deltaPosition) / 2;
h = speedTwoFingerDragY* midpoint.y;
w = speedTwoFingerDragX* midpoint.x;

//Obj.transform.Translate(Vector3.right* w, Space.World);

				if(invertY)
				{
					Obj.transform.Rotate (-h, 0, 0, Space.World);
				}
				else
				{
					Obj.transform.Rotate (h, 0, 0, Space.World);
				}
			}
		}*/
	}
}
		
	
