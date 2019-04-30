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

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize > 2)
                zoomSize -= 1;

            gameObject.transform.Translate(_camZoomSpeed * Vector3.forward * Time.deltaTime);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoomSize < 4)
                zoomSize += 1;

            gameObject.transform.Translate(_camZoomSpeed * Vector3.back * Time.deltaTime);
        }
    }
}
