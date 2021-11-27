using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AppliedBuff buff;
    bool isShowInfo = false;

    public void SetBuff(AppliedBuff buff)
    {
        this.buff = buff;
    }

    private void OnGUI()
    {
        GUI.skin.box.wordWrap = true;
        GUI.skin.box.alignment = TextAnchor.UpperLeft;
        if (isShowInfo)
        {
            float x = transform.position.x;
            if (x + 400 > Screen.width) x = x - 400;
            string color = "ff0000";
            if (buff.buff.isGood) color = "00dd00";
            string buffDesr = buff.buff.discription;
            buffDesr = buffDesr.Replace("{X}", buff.value.ToString());
            string text = "<color=#" + color + ">" + buff.buff.displayName + "</color>\n\n<color=#ffffff>" + buffDesr + "\nДлительность: " + buff.turns + "</color>";
            GUI.Box(new Rect(x, Screen.height - transform.position.y, 400, 100), text);
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



}
