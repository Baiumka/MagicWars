using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField]
    Scrollbar scroll;

    [SerializeField]
    Button _left;
    [SerializeField]
    Button _right;

    [SerializeField]
    float value;

    void Start()
    {
        scroll.value = 0;
        scroll.interactable = false;
        scroll.direction = Scrollbar.Direction.RightToLeft;
    
        _left.onClick.AddListener(OnClickLeft);
        _right.onClick.AddListener(OnClickRight);
    }

    private void OnClickRight()
    {
        scroll.value += value;
    }

    private void OnClickLeft()
    {
        scroll.value -= value;
    }
}
