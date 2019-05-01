using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Zoom : MonoBehaviour {

    public float zoomSize = 5;
    public float _camZoomSpeed = 10f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            

            if (transform.position.x >= 15 && transform.position.x <= 30)
            {
                transform.Translate(deltaMagnitudeDiff * Vector3.forward * Time.deltaTime);
            }
            if (transform.position.x <= 15)
            {
                transform.position = new Vector3(16, transform.position.y, transform.position.z);
            }
            if (transform.position.x >= 30)
            {
                transform.position = new Vector3(29, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.x >= 15)
        {
            if (zoomSize > 2)
                zoomSize -= 1;

            transform.Translate(_camZoomSpeed * Vector3.forward * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.x <= 30)
        {
            if (zoomSize < 4)
                zoomSize += 1;

            transform.Translate(_camZoomSpeed * Vector3.back * Time.deltaTime);
        }
    }
}
