using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SidePanelButton : MonoBehaviour
{
    public bool isShow = true;
    public float diference;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        isShow = !isShow;
        if (isShow)
        {
            transform.parent.localPosition = new Vector3(transform.parent.localPosition.x - diference, transform.parent.localPosition.y, transform.parent.localPosition.z);
        }
        else
        { 
            transform.parent.localPosition = new Vector3(transform.parent.localPosition.x + diference, transform.parent.localPosition.y, transform.parent.localPosition.z);
        }
        transform.Find("Arrow").Rotate(new Vector3(0f, 0f, 180f));


    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
