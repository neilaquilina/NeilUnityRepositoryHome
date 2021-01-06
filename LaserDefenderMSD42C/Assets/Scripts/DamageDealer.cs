using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    //returns the amount of damage
    public int GetDamage()
    {
        return damage;
    }

    //destroys the gameObject
    public void Hit()
    {
        Destroy(gameObject);
    }
}
