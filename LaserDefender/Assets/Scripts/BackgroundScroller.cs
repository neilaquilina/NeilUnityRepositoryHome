using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    //the speed of the scrolling
    [SerializeField] float backgroundScrollSpeed = 0.02f;
    //the material from the texture
    Material myMaterial;
    //the movement
    Vector2 offSet;

    // Start is called before the first frame update
    void Start()
    {
        //get the material of the background from the Renderer component
        myMaterial = GetComponent<Renderer>().material;
        //will scroll in the y-axis at the speed
        offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //move the material mainTextureOffset by offSet every frame
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
