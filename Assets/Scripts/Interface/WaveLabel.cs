using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveLabel 
{
    private const int MAX_HEIGHT = 300;

    private string _mesasge;
    private int _h;
    private Sprite _sprite;

    public string mesasge { get => _mesasge; set => _mesasge = value; }
    public int h { get => _h; set => _h = value; }
    public Sprite sprite { get => _sprite; set => _sprite = value; }

    public WaveLabel(string message)
    {
        this._mesasge = message;
        this._h = 0;
        this._sprite = null;
    }

    public WaveLabel(string message, Sprite sprite)
    {
        this._mesasge = message;
        this._h = 0;
        this._sprite = sprite;
    }

    public bool UpWaweLabel(int height)
    {
        _h += height;
        if(_h >= MAX_HEIGHT)
        {
            return true;
        }
        else
        {
            return false;
        }
    }





}
