using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [Header("Health Related Variables")]
    public bool isHealth, isFullHealth;
    public int healthToAdd, healthToFullyRestore;

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
            if (isHealth && PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.RestoreHealth(healthToAdd);
                Destroy(gameObject);
            }
            if (isFullHealth && PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.RestoreHealth(healthToFullyRestore);
                Destroy(gameObject);
            }
        }
    }
}
