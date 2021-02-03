using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballer : MonoBehaviour
{
    

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    
    
    
    [Header("Laser")]
    [SerializeField] GameObject ballPrefab;
    [SerializeField] float ballSpeed = 10f;
    [SerializeField] float ballShootPeriod = 2f;

    

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine fireCoroutine;

    bool coroutineStart = false;
    

    // Start is called before the first frame update
    void Start()
    {
        SetupMoveBoundaries();
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }


    private void Fire()
    {
        //we receive an input from the player
        //and fire a laser 

        //if a button linkedd to Fire1 is pressed
        if(Input.GetButtonDown("Fire1"))
        {
            if (!coroutineStart)
            {
                fireCoroutine = StartCoroutine(FireContinuously());
                coroutineStart = true;
            }
            

        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
            coroutineStart = false;
        }
    }

    IEnumerator FireContinuously()
    {
        //while coroutine is running, keep it running
        while (true)
        {
            //create a prefab at the current position with a standard rotation
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
            var randomX = Random.Range(-2f, 2f);
            
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(randomX, ballSpeed);

            yield return new WaitForSeconds(ballShootPeriod);
        }
        
    }

    private void Move()
    {
        //depending on player keys pressed the Player ship will move

        //move left and right
        var deltaX = Input.GetAxis("Horizontal")* Time.deltaTime * moveSpeed;
        Debug.Log(deltaX);
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        
        //move up and down
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        Debug.Log(deltaY);
        var newYPos = Mathf.Clamp( transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }


    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
