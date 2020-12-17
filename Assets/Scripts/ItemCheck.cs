using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCheck : MonoBehaviour
{
    [SerializeField] private Image imageTrue;
    [SerializeField] private Image imageFalse;

    private int index;

    public int Index => index;
    public void ShowCheck(bool isCheck)
    {
        if (isCheck)
        {
            imageTrue.gameObject.SetActive(isCheck);
        }
        else
        {
            imageFalse.gameObject.SetActive(!isCheck);
        }
    }

    public void SetIndex(int value)
    {
        index = value;
    }

    public void Hide()
    {
        imageTrue.gameObject.SetActive(false);
        imageFalse.gameObject.SetActive(false);
    }
}
