using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour
{
    [SerializeField] AudioClip goalAudio;

    int score = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        AudioSource.PlayClipAtPoint(goalAudio, transform.position);
        score++;

        if (score >= 10)
        {
            SceneManager.LoadScene("Win");
        }

    }

    public int GetScore()
    {
        return score;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
