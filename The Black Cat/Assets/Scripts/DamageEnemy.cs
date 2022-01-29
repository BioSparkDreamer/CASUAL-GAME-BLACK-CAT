using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    public int damageToDeal;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damageToDeal);
        }
    }
}
