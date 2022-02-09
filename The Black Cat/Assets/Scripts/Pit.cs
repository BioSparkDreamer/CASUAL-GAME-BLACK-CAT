using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.TakeDamage(1);

            if (PlayerHealthController.instance.currentHealth > 0)
            {
                GameManager.instance.Respawn();
            }
        }
    }
}
