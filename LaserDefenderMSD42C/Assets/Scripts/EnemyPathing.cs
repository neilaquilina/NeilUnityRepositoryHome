using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] List<Transform> waypoints;
    
    [SerializeField] WaveConfig waveConfig;

    //saves the waypoint in which we want to go
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //get the List waypoints from WaveConfig
        waypoints = waveConfig.GetWaypoints();
        
        //set the start position of Enemy to the 1st waypoint position
        transform.position = waypoints[waypointIndex].transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        //   0, 1, 2      <=    2
        if (waypointIndex <= waypoints.Count - 1)
        {
            //set the targetPosition to the waypoint where we want to go
            var targetPosition = waypoints[waypointIndex].transform.position;
            
            //make sure that z axis = 0
            targetPosition.z = 0f;

            var enemyMovement = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            //move Enemy from current position to targetPosition, at enemyMovement speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            //if enemy reaches the target position
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }

        //if enemy reaches last waypoint
        else
        {
            
            Destroy(gameObject);
        }
    }

    //set up a WaveConfig
    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }
        

}
