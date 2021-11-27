using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDialog : MonoBehaviour
{
    [SerializeField]
    SkillButton currentMainSkill;
    [SerializeField]
    Transform comboPanel;
    [SerializeField]
    GameObject skillButtonPrefab;

    [SerializeField]
    Button yesButton;
    [SerializeField]
    Button noButton;
    
    public void ShowDialog(Skill mainSkill, List<Skill> combo)
    {        
        gameObject.SetActive(true);

        yesButton.onClick.AddListener(PressedYesButton);
        noButton.onClick.AddListener(PressedNoButton);
        

        foreach (Transform child in comboPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (Skill s in combo)
        {
            GameObject skillButton = GameObject.Instantiate(skillButtonPrefab, comboPanel);
            Destroy(skillButton.GetComponent<Button>());
            skillButton.GetComponent<SkillButton>().SetSkill(s);
        }
        currentMainSkill.SetSkill(mainSkill);
        gameObject.SetActive(true);
    }


    void PressedYesButton()
    {
        CloseDialog();
    }
    void PressedNoButton()
    {
        CloseDialog();
    }

    void CloseDialog()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
