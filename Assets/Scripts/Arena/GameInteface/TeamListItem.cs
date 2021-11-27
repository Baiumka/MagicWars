using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TeamListItem : MonoBehaviour
{
    public float normalHeight;
    public float openedHeight;

    bool isOpen;
    string playerName;
    public string getName() { return playerName; }

    void Start()
    {      
        this.transform.Find("namePanel").GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Rect rect = gameObject.GetComponent<RectTransform>().rect;
        Text statText = transform.Find("StatText").GetComponent<Text>();
        if (isOpen) 
        {           
            statText.gameObject.SetActive(false);
            rect.height = normalHeight;
        }
        else 
        {
            rect.height = openedHeight;           
            statText.gameObject.SetActive(true);
        }
        isOpen = !isOpen;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);
        transform.Find("StatText").GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);
        
        
    }

    public void SetHealth(int hp, int maxHp)
    {
        Slider hpSlider = gameObject.transform.Find("namePanel/HPBar").GetComponent<Slider>();
        hpSlider.maxValue = maxHp;
        hpSlider.value = hp;
    }

    public void SetName(string name, Color color)
    {
        Text text = transform.Find("namePanel/Name").GetComponent<Text>();
        text.text = name;
        text.color = color;
        playerName = name;
    }

    public void SetStatText(string statText)
    {
        transform.Find("StatText").GetComponent<Text>().text = statText;
    }
}
