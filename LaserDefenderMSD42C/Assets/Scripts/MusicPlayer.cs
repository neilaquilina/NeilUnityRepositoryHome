using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //runs even before Start()
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        //GetType(): gets the type of Object attached to this script: MusicPlayer
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            //if there is more than MusicPlayer, destroy the last one
            Destroy(gameObject);
        }
        else
        {
            //do not destroy the MusicPlayer when changing scenes
            DontDestroyOnLoad(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
