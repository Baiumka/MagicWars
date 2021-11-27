using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchButton : MonoBehaviour
{
    [SerializeField]
    public int teamSize;
    public Button button;

    public void MakeButtonEnable()
    {
        button.enabled = true;
        button.gameObject.GetComponent<Image>().color = Color.white;
    }

    public void MakeButtonDisable()
    {
        button.enabled = false;
        button.gameObject.GetComponent<Image>().color = Color.gray;
    }
}
