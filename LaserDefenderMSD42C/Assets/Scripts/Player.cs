using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 500;

    //a variable that can be edited from Unity    
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.7f;

    [SerializeField] GameObject laserPrefab;

    [SerializeField] float laserFiringTime = 0.3f;

    [SerializeField] AudioClip playerDeathSound;
    //allows the variable to be set in the Inspector from 0 to 1
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.75f;

    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.1f;

    float xMin, xMax, yMin, yMax;

    Coroutine fireCoroutine;

    bool coroutineStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    public int GetHealth()
    {
        return health;
    }

    //reduces health whenever Player collides with a gameObject 
    //which has a DamageDealer component
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //access the DamageDealer class from "otherObject" which hits player
        //and reduce health accordingly
        DamageDealer dmgDealer = otherObject.gameObject.GetComponent<DamageDealer>();

        //if there is no dmgDealer in otherObject, end the method
        if (!dmgDealer) // if (dmgDealer == null)
        {
            return;
        }

        ProcessHit(dmgDealer);

    }

    //whenever ProcessHit() is called, send us the DamageDealer details
    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage();

        if (health <= 0)
        {

            Die();
        }
    }

    private void Die()
    {
        //destroy player
        Destroy(gameObject);
       
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);

        //find object of type Level in Hierarchy and run its method LoadGameOver()
        FindObjectOfType<Level>().LoadGameOver();

    }

    //Coroutine example:
    //private IEnumerator PrintAndWait()
    //{
    //    print("Message 1");
    //    //pause coroutine execution for 10 seconds
    //    yield return new WaitForSeconds(10);
    //    print("Message 2 after 10 seconds");
    //}

    private IEnumerator FireContinuously()
    {
        while (true) //while coroutine is running
        {
            //create a Laser Prefab, at the Player position
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            //gives velocity to each laser in the y-axis
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 15f);

            //play clip when Player fires
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

            //wait for x amount of seconds
            yield return new WaitForSeconds(laserFiringTime);

        }
    }
     

    private void Fire()
    {
        //whenever I fire
        if (Input.GetButtonDown("Fire1"))
        {
            if (!coroutineStarted) //if (coroutineStarted == false)
            {
                fireCoroutine = StartCoroutine(FireContinuously());
                coroutineStarted = true;
            }
        }

        //when button is released
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
            coroutineStarted = false;
        }
    }

    //setup the boundaries according to the camera
    private void SetUpMoveBoundaries()
    {
        //get the camera from Unity
        Camera gameCamera = Camera.main;

        //xMin = 0  xMax = 1
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //yMin = 0   yMax = 1
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    //moves the spaceship around
    private void Move()
    {
        //deltaX is updated with the movement in the x-axis (left and right)
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed ; 
        
        //newXPos = current x-pos of Player
        //+ difference in X by keyboard Input
        var newXPos = transform.position.x + deltaX;
        //clamps the newXPos between xMin and xMax
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);

        //the same as above but in the y-axis
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = transform.position.y + deltaY;
        newYPos = Mathf.Clamp(newYPos, yMin, yMax);

        //update the position of the Player
        this.transform.position = new Vector2(newXPos, newYPos);

        
            
    }

   

}
