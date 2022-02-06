using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health, soulsToAdd;
    public GameObject enemyObject;
    public GameObject deathEffect;

    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(int damageToDeal)
    {
        health -= damageToDeal;

        if (health <= 0)
        {
            health = 0;
            Destroy(enemyObject);
            GameManager.instance.UpdateSoulsCount(soulsToAdd);
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
        }
    }
}
