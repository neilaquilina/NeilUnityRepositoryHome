using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GoalPost goalPost;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        goalPost = FindObjectOfType<GoalPost>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = goalPost.GetScore().ToString();
    }
}
