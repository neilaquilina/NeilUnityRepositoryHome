using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    void Awake()
    {
        SetUpSingleton();
    }

    //make sure that only 1 GameSession is running
    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    //add scoreValue to score
    public void AddToScore(int scoreValue)
    {
        
        score += scoreValue;
        print(score);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    
}
