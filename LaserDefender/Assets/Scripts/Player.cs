using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserFirePeriod = 0.1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine fireCoroutine;
    

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

            fireCoroutine =  StartCoroutine(FireContinuously());

        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        //while coroutine is running, keep it running
        while (true)
        {
            //create a prefab at the current position with a standard rotation
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

            yield return new WaitForSeconds(laserFirePeriod);
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
