using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosition = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        //Mathf.Clamp: clamps the value of mousePosition between
        //minX and maxX
        float limitMousePosition = Mathf.Clamp(mousePosition, minX, maxX);
        Vector2 paddlePosition = new Vector2(limitMousePosition, transform.position.y);
        
        transform.position = paddlePosition;
    }
}
