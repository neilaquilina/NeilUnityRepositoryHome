using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    Object[] blocks;

    GameObject randomBlock;

    Vector2 blockPosition;

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocksFromResources();

        for (int i = 0; i < 16; i+=2)
        {
            GetRandomBlock();
            blockPosition.x = i;
            SpawnBlocks(randomBlock);
            
            print(i);
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

    private void SpawnBlocks(GameObject blockToSpawn)
    {
        Instantiate(blockToSpawn, blockPosition, transform.rotation);
        

    }
}
