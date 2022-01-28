using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("CheckPoint Variables")]
    public SpriteRenderer theSR;
    public Sprite cpHit, cpNotHit;

    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.DeactiveCheckPoints();
            theSR.sprite = cpHit;

            GameManager.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckPoint()
    {
        theSR.sprite = cpNotHit;
    }
}
