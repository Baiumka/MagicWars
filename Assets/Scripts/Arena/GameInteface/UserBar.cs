using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserBar : MonoBehaviour
{
    [SerializeField]
    GameObject buffPrefab;

    public ArenaPlayer arenaPlayerInfo;

    public void SetInfo(ArenaPlayer arenaPlayer)
    {
        arenaPlayerInfo = arenaPlayer;
        gameObject.transform.Find("Name").GetComponent<Text>().text = arenaPlayerInfo.name;
    }


    public void UpdateUserBarHealth()
    {
        Slider hpSlider = gameObject.transform.Find("HP").GetComponent<Slider>();
        hpSlider.maxValue = arenaPlayerInfo.maxHP;
        hpSlider.value = arenaPlayerInfo.hp;
        gameObject.transform.Find("HP/Text").GetComponent<Text>().text = arenaPlayerInfo.hp + "/" + arenaPlayerInfo.maxHP;
    }

    public void DrawBuffs()
    {
        Transform buffPanel = gameObject.transform.Find("buffPanel");
        foreach (Transform child in buffPanel)
        {
            Destroy(child.gameObject);
        }
        foreach (AppliedBuff ab in arenaPlayerInfo.buffList)
        {
            GameObject newBuff = GameObject.Instantiate(buffPrefab, buffPanel);
            newBuff.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Buffs/" + ab.buff.name);
            newBuff.transform.Find("Text").GetComponent<Text>().text = ab.turns.ToString();
            newBuff.GetComponent<BuffTooltip>().SetBuff(ab);
        }
    }


}
