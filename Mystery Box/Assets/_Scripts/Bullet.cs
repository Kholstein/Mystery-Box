using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 7f;
    Rigidbody rb;
    public Transform player;
    Vector3 moveDirection;
    private Vector3 target;

    private void Start()
    { //might need to change the axis for this piece of code since the camera will be in a 2D view
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y);
        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y);
        //target = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Vector3 targetPosition = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z);
        //transform.LookAt(targetPosition);

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

//Ignore all the code in green, I was testing different ways to get the bullet to follow the player

//   public float speed = 7f;
//   private Transform player;
//   private Vector3 target;

//void Start () {
//       player = GameObject.FindGameObjectWithTag("Player").transform;
//       target = new Vector3(player.position.x, player.position.z);
//}

//void Update () {
//       transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
//       if (transform.position.x == target.x && transform.position.z == target.z)
//       {
//           DestroyProjectile();
//       }
//}

//   //private void OnTriggerEnter(Collider other)
//   //{
//   //    if (other.CompareTag("Player"))
//   //    {
//   //        DestroyProjectile();
//   //    }
//   //}

//   void DestroyProjectile()
//   {
//       Destroy(gameObject);
//   }
