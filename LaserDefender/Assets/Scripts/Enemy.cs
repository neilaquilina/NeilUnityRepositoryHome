﻿using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;

    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject enemyLaserPrefab;

    [SerializeField] float enemyLaserSpeed = 10f;

   

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        //every frame reduces the amount of time that the frame takes to run
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            EnemyFire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    //same as Player Fire() method
    private void EnemyFire()
    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        //enemy laser shoots downwards, hence -enemyLaserSpeed
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
    }

    //reduces health whenever enemy collides with a gameObject which has DamageDealer component
    private void OnTriggerEnter2D(Collider2D other)
    {
        //access the Damage Dealer from the "other" object which hit the enemy
        //and depending on the laser damage reduce health
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        //if there is no damageDealer on Collision
        //end the method
        if(!damageDealer)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    //whenever ProcessHit is called send us the DamageDealer details
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //destroy the laser that hits the Enemy
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}