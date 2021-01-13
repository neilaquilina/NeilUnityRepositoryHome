using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    //updates the text in UI
    Text healthText;
    Player player;
    Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();

        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = player.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();

        healthBar.value = player.GetHealth();
    }
}
