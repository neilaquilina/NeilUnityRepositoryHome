using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;

    Level level;

    void Start()
    {
        //find the object of type Level and save it to level
        level = FindObjectOfType<Level>();

        //add 1 to the breakableBlocks
        level.CountBreakableBlocks();
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(this.gameObject);
        level.BlockDestroyed();
   }

   
}
