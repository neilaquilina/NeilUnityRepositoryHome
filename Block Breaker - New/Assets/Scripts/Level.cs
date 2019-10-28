using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] int breakableBlocks;

    SceneLoader sceneLoader;

    //when all blocks are destroyed load next scene
    public void BlockDestroyed()
    {
        breakableBlocks--;

        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    //add up all blocks
    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
