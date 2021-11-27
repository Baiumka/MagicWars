using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    //public Material skybox;
    void Start()
    {
        
    }

    // Update is called once per frame
    float r = 0;
    void Update()
    {
       
        r += 0.01f;
        if (r >= 360) r = 0;
        RenderSettings.skybox.SetFloat("_Rotation", r);
       // this.transform.Rotate(new Vector3(0, -0.01f, 0));
    }
}
