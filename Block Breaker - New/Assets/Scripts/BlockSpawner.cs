using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    Object[] blocks;

    GameObject randomBlock;

    Vector2 blockPosition, startingBlockPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingBlockPosition = transform.position;
        LoadBlocksFromResources();
        PrintBlocksOnXAndY();

    }

    private void PrintBlocksOnXAndY()
    {
        for (int y = 5; y > 1; y--)
        {
            //print block from left to right
            for (int x = 1; x < 15; x += 2)
            {
                GetRandomBlock();
                SpawnBlocks();
                blockPosition.x += 2;
            }
            //set x position to starting position
            blockPosition.x = startingBlockPosition.x;
            //set the y position to below the previous position
            blockPosition.y -= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadBlocksFromResources()
    {
        blocks = Resources.LoadAll("Blocks");
        blockPosition = transform.position;
    }

    private void GetRandomBlock()
    {
        int randomNumber = Random.Range(0, blocks.Length);

        randomBlock = (GameObject) blocks[randomNumber];

    }

    private void SpawnBlocks()
    {
        Instantiate(randomBlock, blockPosition, transform.rotation);
        

    }
}
