using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransparentObject : MonoBehaviour
{
    public GameObject block;
    public int size;
    public int pointCost;
    public int count;

    public TMP_Text text;

    public void Awake()
    {
        text.text = "" + count;
    }
    public void decreaseAmount()
    {
        count--;
        text.text = ""+count;
    }
}
