using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [Header("Object Variables")]
    public Rigidbody2D theRB;

    [Header("Enemy Movement Variables")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;

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
            transform.localScale = Vector3.one;
            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
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
