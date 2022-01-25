using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [Header("Enemy Movement Variables")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;

    [Header("Object Variables")]
    public SpriteRenderer theSR;
    public Rigidbody2D theRB;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        movingRight = true;
        leftPoint.parent = null;
        rightPoint.parent = null;
    }

    void Update()
    {
        if (movingRight)
        {
            theSR.flipX = true;
            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            theSR.flipX = false;
            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }
    void FixedUpdate()
    {
        if (movingRight == true)
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else if (movingRight == false)
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }
    }
}
