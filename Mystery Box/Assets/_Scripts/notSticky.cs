using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add this script to the player and add a tag to the object that is going to be slippery

//we will have to adjust the speed on the script in the inspector tab depending on height of the player

//note the higher the speed the more the player will drop so if a wall is close 
//I think it would be a good idea to make duplicates of the slippery wall and adjust different speeds for them

//to the bottom death plane, the player will appear out of sight and not die

//another issue is that the player may teleport to the other side of the wall

//the code that was commented out was stuff I was testing

public class notSticky : MonoBehaviour {
    Rigidbody rb;
    public int speed; //25f is default

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Wall2" || collision.collider.tag == "Projectile")
        {
            //the function I made
            //Drop();
            //I dont know if line 35 does anything but I just left it as it just cause

            rb.useGravity = true;
            rb.isKinematic = false;
            
            //line 38 was fun to mess around with, the player would spin out of control along with the aiming indicator
            //rb.isKinematic = false;

            //line 42 is was causes the transform of the player to move down on the Y axis by -10
            //then I multiplied it by the speed (important) and time
            //transform.Translate(new Vector3(0, -10, 0) * Time.deltaTime * speed);
        }
        else
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    //I made a function to make the player drop and put it on the OnCollisionEnter function
    //void Drop()
    //{
    //    transform.Translate(Vector3.down * Time.deltaTime * speed);
    //}

    void Update()
    { //this code caused the player to drop every frame, so the player would continually fall

        //if (gameObject.tag.Equals("Wall2"))
        //{
        //    transform.Translate(Vector3.down * Time.deltaTime * speed);
        //}
    }
}