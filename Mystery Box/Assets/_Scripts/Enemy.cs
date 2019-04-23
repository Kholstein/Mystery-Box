using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public GameObject shot;
    //private Rigidbody rb;
    public Transform player;
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public float bulletTime;
    public float destroySpeed;
    GameObject target;
    ParticleSystem turret;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        target = GameObject.Find("Player");
        turret = GetComponent<ParticleSystem>();
    }

    //void FixedUpdate()
    //{
    //    Vector3 vel = rb.velocity;
    //    rb.velocity = vel;
    //}

    void Update()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, target.transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
        float dist = Vector3.Distance(targetPosition, transform.position);

        if (timeBtwShots <= 0 && dist <= 20)
        {
            shot = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
            //Instantiate(turret, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
            Destroy(shot, destroySpeed);
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}

//This code is if we would want the shooting enemy to move

//if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
//{
//    transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
//}
//else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
//{
//    transform.position = this.transform.position;
//}
//else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
//{
//    transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
//}

//this code is for enemy rotation, I couldn't get it to work
//   private float speed = 30;
//   public Rigidbody throwable;
//   RaycastHit hit;

//   public float fieldOfViewAngle = 110f;
//   public bool playerInSight;
//   private SphereCollider col;
//   private GameObject player;

//   private void Awake()
//   {
//       if (hit.collider.gameObject == player)
//       {
//           playerInSight = true;
//           col = GetComponent<SphereCollider>();
//           player = GameObject.FindGameObjectWithTag("Player");
//       }
//   }


//   private void OnTriggerStay(Collider other)
//   {
//       if(other.gameObject == player)
//       {
//           playerInSight = false;
//           Vector3 direction = other.transform.position - transform.position;
//           float angle = Vector3.Angle(direction, transform.forward);

//           if(angle < fieldOfViewAngle * 0.05f)
//           {
//               RaycastHit hit;
//               if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
//               {
//                   if(hit.collider.gameObject == player)
//                   {
//                       playerInSight = true;
//                   }
//               }
//           }
//       }
//   }

//   private void OnTriggerExit(Collider other)
//   {
//       if(other.gameObject == player)
//       {
//           playerInSight = false;
//       }
//   }
