using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
   // X Rotation Block Variables
    public float rotationEuler = 1f;

    // HookShot block variables
    public float speed = 10f;
    public LayerMask shotLayer;

    public bool Moving;
    private Vector3 target;

    // X Rotation calculation Variables
    Vector3 rotation;
    Quaternion FinalQuaternion;

	public GameObject lasthit;

    // Empty Object That Is Respawn Point
    public Transform respawnPosition;

    // Rigidbody to stop movement on death.
    Rigidbody rb;

    void Start () 
	{
        rotation = Vector3.zero;
        respawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        rb = GetComponentInParent<Rigidbody>();
    }
	
	
	void Update () 
	{
		transform.position = new Vector3(0, transform.position.y, transform.position.z);

        // X Rotation Block
		if(Input.GetAxisRaw("Vertical") != 0)
        {
            rotation.x += (Input.GetAxis("Vertical") * rotationEuler);
            Debug.Log(rotation);
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
            // rotation applied
        }
        else
        {
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
        }

		if (Input.GetKeyDown(KeyCode.Space))
        {
			Moving = true;
		}

        //Hookshot Movement block
        if (Moving)
        {
            transform.position += transform.forward;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != lasthit)
		{
			Moving = false;
        }
		lasthit = other.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death_Plane")) //Large death trigger
        {
            rb.velocity = Vector3.zero;
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPosition.position;
        Moving = false;
    }
}


