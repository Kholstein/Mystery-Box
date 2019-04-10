using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
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

    void Respawn()
    {
        transform.position = respawnPosition.position;
        
    }
}