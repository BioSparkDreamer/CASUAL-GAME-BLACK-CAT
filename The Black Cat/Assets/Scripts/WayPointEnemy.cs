using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointEnemy : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed;
    public Transform[] wayPoints;
    float distanceToPoint;
    int nextWayPoint = 0;

    void Start()
    {

    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        distanceToPoint = Vector2.Distance(transform.position, wayPoints[nextWayPoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, moveSpeed * Time.deltaTime);

        if (distanceToPoint < 0.2f)
        {
            Vector3 currentRotation = transform.eulerAngles;

            currentRotation.z += wayPoints[nextWayPoint].transform.eulerAngles.z;
            transform.eulerAngles = currentRotation;
            nextWayPoint++;

            if (nextWayPoint == wayPoints.Length)
            {
                nextWayPoint = 0;
            }
        }
    }
}
