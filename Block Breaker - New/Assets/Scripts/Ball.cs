using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //we put a Paddle object in the Ball since they will be connected
    [SerializeField] Paddle myPaddle;
    [SerializeField] float xVelocity = 2f;
    [SerializeField] float yVelocity = 15f;

    bool hasStarted = false;

    Vector2 paddleToBallDistance;

    // Start is called before the first frame update
    void Start()
    {
       //this will save the Distance between the Ball and the Paddle
       paddleToBallDistance = this.transform.position - myPaddle.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle(); 
            LaunchOnMouseClick();
        }
        
    }

    //this method is done to refactor the code.
    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(myPaddle.transform.position.x, myPaddle.transform.position.y);
        //set the Ball's position to the Paddle's position + the distance between them
        transform.position = paddlePosition + paddleToBallDistance;
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) //left click
        {
            hasStarted = true;
            
            //access Rigidbody2D of the ball and give it a velocity
            //to be able to give it a force upwards
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted == true)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }



}
