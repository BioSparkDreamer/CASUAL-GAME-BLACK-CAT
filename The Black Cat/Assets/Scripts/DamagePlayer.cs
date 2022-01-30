using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [Header("Damage Variables")]
    public int damageToDeal;
    public int knockBackForceX, knockBackForceY;
    public bool canKnockBack;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.TakeDamage(damageToDeal);
            if (canKnockBack)
            {
                PlayerController.instance.KnockBack(knockBackForceX, knockBackForceY);
            }
        }
    }
}
