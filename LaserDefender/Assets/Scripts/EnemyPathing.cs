using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
     WaveConfig waveConfig;
    [SerializeField] List<Transform> waypoints;
    

    //target waypoint where we want to go
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        waypoints = waveConfig.GetWaypoints();

        //set the starting position of the enemyPath to the waypointIndex (0)
        transform.position = waypoints[waypointIndex].transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMoveOnPath();
    }

    //setting up a WaveConfig
    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        waveConfig = waveConfigToSet;
    }

    private void EnemyMoveOnPath()
    {
        
        //if not yet reached last waypoint

        if (waypointIndex < waypoints.Count)
        {
            //the waypoint where we want to go
            var targetPosition = waypoints[waypointIndex].transform.position;
            
            //to make sure Z-axis does not interfere
            targetPosition.z = 0f;
            
            //move at this speed each frame
            var movementThisFrame = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            this.transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            //if we reached our targetPosition
            if (transform.position == targetPosition)
            {
                //increment wayPointIndex to the next
                waypointIndex++;
            }

        }
        else //if last waypoint reached
        {
            Destroy(gameObject);
        }
    }
}
