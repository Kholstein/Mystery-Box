using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomMovement : MonoBehaviour {
    public float rotationMax = 80f;
    public float rotationMin = -80f;

    float rotationCap = 25f;
    float rotationMove = 0f;
    public float rotationSpeed = 10f;
    bool rotationDirection;

    Vector3 rotation;
    Quaternion FinalQuaternion;

    void Start () {
        

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(rotationDirection)
        {
            if(rotationMove < rotationCap)
            {
                rotationMove += rotationSpeed * Time.fixedDeltaTime;
            }
            else
            {
                rotationDirection = false;
                rotationCap = Random.Range(rotationMin, 0f);
            }
        }
        else
        {
            if(rotationMove > rotationCap)
            {
                rotationMove -= rotationSpeed * Time.fixedDeltaTime;
            }
            else
            {
                rotationDirection = true;
                rotationCap = Random.Range(0f, rotationMax);
            }
        }

        rotation = new Vector3(rotationMove + 90, 0, 0);
        FinalQuaternion = Quaternion.Euler(rotation);
        transform.rotation = FinalQuaternion;

    }
}
