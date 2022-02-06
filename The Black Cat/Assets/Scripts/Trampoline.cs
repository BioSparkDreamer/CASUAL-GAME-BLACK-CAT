using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline Variables")]
    public float bounceForce;
    public float stayUpTime;
    private float stayUpCounter;
    private SpriteRenderer theSR;
    public Sprite downSprite, upSprite;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Changing Sprite of Trampoline depending on stayUpCounter
        if (stayUpCounter > 0)
        {
            stayUpCounter -= Time.deltaTime;

            //Change sprite to down sprite
            if (stayUpCounter <= 0)
            {
                theSR.sprite = downSprite;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            theSR.sprite = upSprite;
            stayUpCounter = stayUpTime;

            Rigidbody2D player = other.GetComponent<Rigidbody2D>();
            player.velocity = new Vector2(player.velocity.x, bounceForce);
        }
    }
}
