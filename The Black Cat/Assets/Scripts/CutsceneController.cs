using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    [Header("Object Variables")]
    public Animator catAnimator;
    public SpriteRenderer theCatSR;
    public Animator playerAnimator;
    public FadeScreen fader;

    [Header("Cutscene Part 1 Variables")]
    public Transform movePoint1;
    public Transform catPoint, playerPoint;
    public float moveSpeed;
    public bool cutScene1HasEnded;

    [Header("Cutscene Part 2 Variables")]
    public bool cutScene2HasEnded;
    public float waitToRun;

    [Header("Cutscene Part 3 Variables")]
    public bool cutScene3HasEnded;
    public Transform movePoint2;
    public float waitToFade;

    void Start()
    {

    }

    void Update()
    {
        if (!cutScene1HasEnded)
        {
            catPoint.transform.position = Vector3.MoveTowards(catPoint.transform.position, movePoint1.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(catPoint.transform.position, movePoint1.position) < .1f)
            {
                theCatSR.flipX = false;
                AudioManager.instance.PlaySFXAdjusted(8);
                catAnimator.SetBool("Sit", true);
                cutScene1HasEnded = true;
                AudioManager.instance.PlaySFX(9);
            }
        }

        if (cutScene1HasEnded == true && !cutScene2HasEnded)
        {
            playerAnimator.SetBool("Rise", true);
            waitToRun -= Time.deltaTime;

            if (waitToRun <= 0)
            {
                cutScene2HasEnded = true;
            }

        }

        if (cutScene2HasEnded == true && !cutScene3HasEnded)
        {
            playerAnimator.SetBool("Move", true);
            playerPoint.transform.position = Vector3.MoveTowards(playerPoint.transform.position, movePoint2.transform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(playerPoint.transform.position, movePoint2.position) < .1f)
            {
                cutScene3HasEnded = true;
                fader.FadeOut();
            }
        }


    }
}
