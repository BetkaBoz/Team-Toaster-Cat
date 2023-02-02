using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransparentObject : MonoBehaviour
{
    public GameObject block;
    public int size;
    public int pointCost;
    public int count;

    public TMP_Text text;
    public Image image;

    public void Awake()
    {
        text.text = "" + count;
        image.color = new Color(1f, 1f, 1f, 0.5f) ;
    }

    public void Selected(bool isSelected)
    {
        if(isSelected)
            image.color = new Color(1f, 1f, 1f, 1f);
        else
            image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void decreaseAmount()
    {
        count--;
        text.text = ""+count;
    }
}
