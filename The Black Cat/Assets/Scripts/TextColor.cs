using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextColor : MonoBehaviour
{
    [Header("Text Variables")]
    public TMP_Text text;

    public void Onselect()
    {
        text.color = Color.white;
    }

    public void OnDeselect()
    {
        text.color = Color.black;
    }
    public void OnPointerEnter()
    {
        text.color = Color.white;
    }
    public void OnPointerExit()
    {
        text.color = Color.black;
    }
}
