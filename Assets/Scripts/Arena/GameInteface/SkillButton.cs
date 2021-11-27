using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update

    Skill skill;
    bool isShowInfo = false;

    void Start()
    {
        Button b = this.GetComponent<Button>();
        if(b != null) b.onClick.AddListener(OnClick);
    }

    private void OnGUI()
    {
        if (skill != null)
        {
            GUI.skin.box.wordWrap = true;
            GUI.skin.box.alignment = TextAnchor.UpperLeft;
            if (isShowInfo)
            {
                float x = transform.position.x;
                if (x + 400 > Screen.width) x = x - 400;
                string text = "<color=#" + skill.color + ">" + skill.displayName + "</color>\n";
                if (skill.tier > 1) text += "<color=#ffffff>Комбинация: " + skill.displayCombo + "</color>\n";     
                text += "<color=#00aa00>Применение на себя:</color>\n<color=#ffffff>" + skill.friendlyDiscription + "</color>\n<color=#dd0000>Применение на противника:</color>\n<color=#ffffff>" + skill.enemyDiscription + "</color>";
                text += "\n\n<color=#dddddd><i><Нажмите ПКМ что бы просмотреть все комбинации></i></color>";
                GUI.Box(new Rect(x, Screen.height - transform.position.y - 230, 400, 200), text);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (skill != null)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Main.client.GetComboList(skill);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isShowInfo = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isShowInfo = false;

    }


    private void OnClick()
    {
        Main.client.UseSkill(skill.name);
    }

        

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Skills/" + skill.name);
        name = skill.name + "SkillButton";
        Color timeColor;
        ColorUtility.TryParseHtmlString("#" + skill.color + "ff", out timeColor);
        GetComponent<Image>().color = timeColor;
    }

    public void SetSkill(UsedSkill skill)
    {
        this.skill = skill.skill;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Skills/" + skill.skill.name);
        name = skill.skill.name + "UsedSkillLabel";
        
        if (skill.isUsed || skill.skill.tier == 3)
        {
            Color timeColor;
            ColorUtility.TryParseHtmlString("#" + skill.skill.color + "ff", out timeColor);
            GetComponent<Image>().color = timeColor;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }

        
        
    }

}
