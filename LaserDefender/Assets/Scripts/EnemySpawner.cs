using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //a list of WaveConfigs
    [SerializeField] List<WaveConfig> waveConfigs;
    
    //we start always from Wave 0
    [SerializeField] int startingWave = 0;

    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            //start the coroutine that spawns all enemies in our currentWave
            yield return StartCoroutine(SpawnAllWaves());
        }
        //when coroutine SpawnAllWaves finishes check if looping == true
        while (looping);
        
    }

    private IEnumerator SpawnAllWaves()
    {
        //this will loop from startingWave until we reach the last within our List
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            //the coroutine will wait for all enemies in Wave to spawn
            //before yielding and going to the next loop
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //when calling this Corotuine, we need to specify which WaveConfig we want to spawn
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        //spawns an enemy until enemyCount == GetNumberOfEnemies()
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn the enemy from 
            //at the position specified by the waveConfig waypoints
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            //the wave will be selected from here and the enemy applied to it
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
