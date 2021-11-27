using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCountTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isShowInfo = false;

    private void OnGUI()
    {        
            
            if (isShowInfo)
            {
            GUI.skin.box.wordWrap = true;
            GUI.skin.box.alignment = TextAnchor.UpperLeft;
            float x = transform.position.x;
                if (x + 400 > Screen.width) x = x - 400;
                string text = "Этот счетчик отображает количество элементов в заклинании текущего противника.";
                GUI.Box(new Rect(x, Screen.height - transform.position.y, 300, 50), text);
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
