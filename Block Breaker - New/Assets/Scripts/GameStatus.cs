using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{

    [SerializeField] int currentScore = 0;

    [SerializeField] int pointsPerBlockDestoyed = 10;

    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {

        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        //if there is more than 1 GameStatus object
        if (gameStatusCount > 1)
        {
            //destroy the last one
            Destroy(gameObject);
        }
        else
        {
            //keep it 
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        scoreText.text = currentScore.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestoyed;
        scoreText.text = currentScore.ToString();
    }
}
