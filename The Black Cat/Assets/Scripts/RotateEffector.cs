using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEffector : MonoBehaviour
{
    [Header("Platform Effector Variables")]
    public float waitTime;
    private PlatformEffector2D effector;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetButtonUp("Down"))
        {
            waitTime = 0.1f;
            effector.rotationalOffset = 0;
        }
        if (Input.GetButton("Down"))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.1f;
            }
            else
                waitTime -= Time.deltaTime;
        }

        if (Input.GetButton("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
