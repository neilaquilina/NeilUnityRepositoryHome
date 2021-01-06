using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100f;

    [SerializeField] float shotCounter;

    [SerializeField] float minTimeBetweenShots = 0.2f;

    [SerializeField] float maxTimeBetweenShots = 3f;

    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = 0.3f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;

    [SerializeField] AudioClip enemyDeathSound;
    //allows the variable to be set in the Inspector from 0 to 1
    [SerializeField] [Range(0, 1)] float enemyDeathSoundVolume = 0.75f;

    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.25f;

    [SerializeField] int scoreValue = 50;

    //reduces health whenever enemy collides with a gameObject 
    //which has a DamageDealer component
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        //access the DamageDealer class from "otherObject" which hits enemy
        //and reduce health accordingly
        DamageDealer dmgDealer = otherObject.gameObject.GetComponent<DamageDealer>();

        //if there is no dmgDealer in otherObject, end the method
        if (!dmgDealer) // if (dmgDealer == null)
        {
            return;
        }

        ProcessHit(dmgDealer);

    }

    //whenever ProcessHit() is called, send us the DamageDealer details
    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //destroy enemy
        Destroy(gameObject);
        //instantiate explosion effects
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);

        //destroy after 1 sec
        Destroy(explosion, explosionDuration);

        //add scoreValue to GameSession score
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
    }

    void Start()
    {
        //generate a random number
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);    
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        //every frame reduce the amount of time for shot
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0f)
        {
            EnemyFire();
            //reset shotCounter
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }
    }

    private void EnemyFire()
    {
        //spawn an enemyLaser at Enemy position
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        //shoot laser downwards: -enemyLaserSpeed
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
        //play clip when Enemy fires
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

}
