using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
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
        //the Unity text will be continuously updated with the value of player health
        healthText.text = player.GetHealth().ToString();
        //update the healthBar with player Health
        healthBar.value = player.GetHealth();
    }
}
