using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour {
   // X Rotation Block Variables
    public float rotationEuler = 1f;

    // HookShot block variables
    public float speed = 10f;
    public LayerMask shotLayer;

    public bool Moving;
    private Vector3 target;

    public Text winText;
    public Text loseText;
    public Text shotText;

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

    public AudioSource StickAS;

    public AudioSource DieAS;

    public AudioSource WallAS;

    public GameObject UpUI;

    public GameObject DownUI;

    public float winTimer;
    float delayTime = 5f;
    bool didWin = false;

    public float jumpCounter;
    public float jumpLimit;
    public ParticleSystem PS;
    public Color landColor;
    public Color deathColor;

    void Start () 
	{
        DieAS.Play();
        rotation = Vector3.zero;
        respawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        rb = GetComponentInParent<Rigidbody>();
        AimingLine = GetComponentInChildren<LineRenderer>();
        wall = FindObjectOfType<DeathWallMovement>();
        lastrot = gameObject.transform.rotation;
        jumpCounter = 0;
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
            ++jumpCounter;
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
		if(!Moving)
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
            UpUI.SetActive(false);
            DownUI.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Space) && Moving == false && transform.hasChanged)
        {
            ++jumpCounter;
            transform.hasChanged = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) & AC.Blocking == false)
        {
            Moving = true;
        }

        //Hookshot Movement block
        if (Moving & BounceOffWall == false)
        {
            transform.position += transform.forward/2.5f;
        }

        if (didWin == true)
        {
            winTimer += Time.deltaTime;
            if (winTimer >= delayTime)
            {
                StartCoroutine("WinRestart");
            }
        }

        if (jumpCounter > jumpLimit)
        {
            loseText.gameObject.SetActive(true);
            winTimer += Time.deltaTime;
            if (winTimer >= delayTime)
            {
                StartCoroutine("LoseRestart");
            }
        }
        shotText.text = "Shots: " + jumpCounter + "/" + jumpLimit;
    }
    private void OnParticleCollision(GameObject other)
    {
        Respawn();
        Debug.Log("Particle collisions");
    }

    void OnCollisionEnter(Collision other)
    {
        if(Moving)
        {
            PS.Play();
        };
        ParticleSystem.MainModule mainModule = PS.main;
        mainModule.startColor = landColor;
        Moving = false;
        UpUI.SetActive(true);
        DownUI.SetActive(true);
        if (other.gameObject.tag == "Wall2")
		{
            BounceOffWall = true;
            Moving = true;
            StickAS.Play();
            
        }
        else
        {
            BounceOffWall = false;
            WallAS.Play();
        }
		lasthit = other.gameObject;
        Debug.Log("called");

        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Ceiling")
        {
            jumpCounter = jumpCounter + 3;
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death_Plane")) //Large death trigger
        {
            ParticleSystem.MainModule mainModule = PS.main;
            mainModule.startColor = landColor;
            PS.Play();
            rb.velocity = Vector3.zero;
            Invoke("Respawn", 1f);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            Win();
        }
    }

    void Respawn()
    {
       
        wall.Respawn();
        SceneManager.LoadScene("LevelOne");
        transform.position = respawnPosition.position;
        Moving = false;
        winText.gameObject.SetActive(false);
    }

    void Win()
    {
        winText.gameObject.SetActive(true);
        didWin = true;
    }

    IEnumerator WinRestart()
    {
        winText.gameObject.SetActive(false);
        SceneManager.LoadScene("LevelOne");
        yield return null;
    }

    IEnumerator LoseRestart()
    {
        loseText.gameObject.SetActive(false);
        SceneManager.LoadScene("LevelOne");
        yield return null;
    }
}


