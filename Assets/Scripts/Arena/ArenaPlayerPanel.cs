using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArenaPlayerPanel : MonoBehaviour
{
    ArenaPlayer arenaPlayerInfo;

    public float normalHeight;
    public float openedHeight;
    public bool isOpen;

    [SerializeField]
    Text playerNickName;
    [SerializeField]
    Button PanelBoutton;
    public void SetPlayerInfo(ArenaPlayer player)
    {
        arenaPlayerInfo = player;
        DisplayArenaPlayer();
        Main.client.onArenaPlayerUpdated += UpdatePlayerPanel;
        PanelBoutton.onClick.AddListener(ShowInfo);
    }

    void ShowInfo()
    {
        Rect rect = gameObject.GetComponent<RectTransform>().rect;
        Text statText = transform.Find("StatText").GetComponent<Text>();
        if (isOpen)
        {
            statText.gameObject.SetActive(false);
            rect.height = normalHeight;
        }
        else
        {
            rect.height = openedHeight;
            statText.gameObject.SetActive(true);
        }
        isOpen = !isOpen;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);
        transform.Find("StatText").GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);
    }

    void UpdatePlayerPanel(ArenaPlayer updatedPlayer)
    {
        if (updatedPlayer == arenaPlayerInfo) DisplayArenaPlayer();
    }


    void DisplayArenaPlayer()
    {   
        playerNickName.text = arenaPlayerInfo.name;
        if(arenaPlayerInfo == ArenaPlayer.currentArenaPlayer)
        {
            playerNickName.color = Color.green; 
        }

        Slider hpSlider = gameObject.transform.Find("namePanel/HPBar").GetComponent<Slider>();
        hpSlider.maxValue = arenaPlayerInfo.maxHP;
        hpSlider.value = arenaPlayerInfo.hp;

        transform.Find("StatText").GetComponent<Text>().text = GetPlayerStats();

    }

    string GetPlayerStats()
    {
        string statsText = "";
        statsText += "Здоровье: " + arenaPlayerInfo.hp + "/" + arenaPlayerInfo.maxHP + "\n";
        statsText += "Защита: " + arenaPlayerInfo.defend + "\n";
        statsText += "Уворот: " + arenaPlayerInfo.evade + "%\n";
        statsText += "Точность: " + arenaPlayerInfo.accuracy + "%\n\n";
        statsText += "Крит. шанс: " + arenaPlayerInfo.critChance + "%\n";
        statsText += "Множетель: х" + arenaPlayerInfo.critRate + "";

        return statsText;
    }


}
