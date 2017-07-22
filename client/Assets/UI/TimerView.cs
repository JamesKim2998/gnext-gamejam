using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    public Image Num1;
    public Image Num2;
    public Sprite[] NumSprite = new Sprite[10];

    private int _oldValue = -1;

    public void Set(int value)
    {
        value = Mathf.Clamp(value, 0, 999);
        if (value == _oldValue) return;
        _oldValue = value;
        Num1.sprite = NumSprite[(value / 10) % 10];
        Num2.sprite = NumSprite[(value / 1) % 10];
    }
}
