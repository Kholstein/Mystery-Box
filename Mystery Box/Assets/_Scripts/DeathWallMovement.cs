using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWallMovement : MonoBehaviour
{

    public float speed;
    public Transform respawnPosition;

    void FixedUpdate()
    {

        transform.position += Vector3.forward * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //Large death trigger
        {
            SceneManager.LoadScene("LevelOne");
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.position = respawnPosition.position;

    }
}