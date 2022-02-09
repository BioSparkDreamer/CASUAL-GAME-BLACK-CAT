using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    [Header("Variables")]
    public Animator fadeScreen;
    public string levelToLoad;

    void Start()
    {
        FadeIn();
    }

    void Update()
    {

    }

    public void FadeIn()
    {
        fadeScreen.SetBool("FadeIn", true);
    }

    public void FadeOut()
    {
        fadeScreen.SetBool("FadeOut", true);
        StartCoroutine(FadeOutCO());
    }

    public IEnumerator FadeOutCO()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LoadingScreen>().LoadScene(levelToLoad);
    }
}
