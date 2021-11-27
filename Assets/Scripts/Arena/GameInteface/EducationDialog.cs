using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EducationDialog : MonoBehaviour
{
    string dialogText;
    Text dialogField;
    Button dialogButton;
    Image dialogArrow;

    private void Awake()
    {
        dialogField = transform.Find("Text").GetComponent<Text>();
        dialogButton = transform.Find("Button").GetComponent<Button>();
        dialogButton.onClick.AddListener(transform.parent.GetComponent<EducationWindow>().OnClickNext);
        dialogArrow = transform.Find("Arrow").GetComponent<Image>();
    }

    public void SetText(string text)
    {
        dialogText = text;
        dialogField.text = dialogText;
    }

    public void ShowButton(bool show)
    {
        dialogButton.gameObject.SetActive(show);
    }

    public void SetArrow(float x, float y, float a)
    {
        if (x == 0 && y == 0 && a == 0)
        {
            dialogArrow.gameObject.SetActive(false);
            return;
        }
        dialogArrow.gameObject.SetActive(true);
        dialogArrow.rectTransform.localPosition = new Vector3(x, y, 0);
        dialogArrow.rectTransform.localEulerAngles = new Vector3(0,0,a);
    }

    public void HideArrow()
    {
        dialogArrow.gameObject.SetActive(false);
    }

    
}
