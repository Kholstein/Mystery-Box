﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour {
   // X Rotation Block Variables
    public float rotationEuler = 1f;

    // HookShot block variables
    public float speed = 10f;
    public LayerMask shotLayer;

    public bool Moving;
    private Vector3 target;

    public Text winText;

    // X Rotation calculation Variables
    Vector3 rotation;
    Quaternion FinalQuaternion;

	public GameObject lasthit;

    // Empty Object That Is Respawn Point
    public Transform respawnPosition;

    // Rigidbody to stop movement on death.
    Rigidbody rb;

    // LineRenderer for aiming with
    LineRenderer AimingLine;

    public AimCheck AC;
    private DeathWallMovement wall; //reset wall
    bool OnUpDown, OnDownDown;
    Quaternion lastrot;

    bool BounceOffWall;

    void Start () 
	{
        rotation = Vector3.zero;
        respawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        rb = GetComponentInParent<Rigidbody>();
        AimingLine = GetComponentInChildren<LineRenderer>();
        wall = FindObjectOfType<DeathWallMovement>();
        lastrot = gameObject.transform.rotation;
    }

    //MOBILE INPUT DO NOT TOUCH

    public void OnUpRotDown()
    {
        OnUpDown = true;
    }

    public void OnUpRotUp()
    {
        OnUpDown = false;
    }

    public void OnDownRotDown()
    {
        OnDownDown = true;
    }

    public void OnDownRotUp()
    {
        OnDownDown = false;
    }

    public void Fire()
    {
        if (AC.Blocking == false)
        {
			Moving = true;
		}
    }
    //MOBILE INPUT DO NOT TOUCH

	
	
	void Update () 
	{
		transform.position = new Vector3(0, transform.position.y, transform.position.z);

        AimingLine.enabled = !Moving;

        if(BounceOffWall)
        {
            Moving = true;
        }

        // X Rotation Block
		if(Input.GetAxisRaw("Vertical") != 0 && !Moving)
        {
            rotation.x += (Input.GetAxis("Vertical") * rotationEuler);
            //Debug.Log(rotation);
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
            // rotation applied
        }
        else
        {
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
        }

        //MOBILE INPUT DO NOT TOUCH
        if(OnUpDown == true)
        {
            rotation.x += (-1 * rotationEuler);
            //Debug.Log(rotation);
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
            // rotation applied
        }
        if(OnUpDown == false)
        {
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
        }
        if(OnDownDown == true)
        {
            rotation.x += (1 * rotationEuler);
            //Debug.Log(rotation);
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
            // rotation applied
        }
        if(OnDownDown == false)
        {
            FinalQuaternion = Quaternion.Euler(rotation);
            transform.rotation = FinalQuaternion;
        }
        //MOBILE INPUT DO NOT TOUCH

		if (Input.GetKeyDown(KeyCode.Space) & AC.Blocking == false)
        {
			Moving = true;
		}

        //Hookshot Movement block
        if (Moving & BounceOffWall == false)
        {
            transform.position += transform.forward/2.5f;
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        Respawn();
        Debug.Log("Particle collisions");
    }

    void OnCollisionEnter(Collision other)
    {
        Moving = false;
        if (other.gameObject.tag == "Wall2")
		{
            BounceOffWall = true;
            Moving = true;
        }
        else
        {
            BounceOffWall = false;
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

        if (other.gameObject.CompareTag("Finish"))
        {
            Win();
        }
    }

    void Respawn()
    {
        wall.Respawn();
        transform.position = respawnPosition.position;
        Moving = false;
        winText.gameObject.SetActive(false);
    }

    void Win()
    {
        winText.gameObject.SetActive(true);
    }
}


