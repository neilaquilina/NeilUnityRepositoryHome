using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;

    [SerializeField] GameObject blockSparklesVFX;

    [SerializeField] int maxHits;

    [SerializeField] int timesHit;

    [SerializeField] Sprite[] hitSprites;

    Level level;

    void Start()
    {
        //find the object of type Level and save it to level
        level = FindObjectOfType<Level>();

        if (tag == "Breakable")
        {
            //add 1 to the breakableBlocks
            level.CountBreakableBlocks();
        }
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
        if (tag == "Breakable")
        {
            
            timesHit++;
            if (timesHit >= maxHits)
            {
                FindObjectOfType<GameStatus>().AddToScore();
                AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
                TriggerSparklesVFX();
                Destroy(this.gameObject);
                level.BlockDestroyed();
            }
            else
            {
                ShowNextHitSprite();
            }
            
        }
        
   }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

   
}
