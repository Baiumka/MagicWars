using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaTeamPanel : MonoBehaviour
{
    ArenaTeam arenaTeamInfo;

    [SerializeField]
    Text idText;
    [SerializeField]
    GameObject playerListPanel;
    [SerializeField]
    GameObject playerPanelPrefab;
    public void SetTeamInfo(ArenaTeam team)
    {
        arenaTeamInfo = team;
        DisplayArenaTeam();
    }

    void DisplayArenaTeam()
    {
        idText.text = arenaTeamInfo.id.ToString();
        foreach(Transform child in playerListPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ArenaPlayer player in arenaTeamInfo.players)
        {
            GameObject playerPanel = GameObject.Instantiate(playerPanelPrefab, playerListPanel.transform);
            playerPanel.GetComponent<ArenaPlayerPanel>().SetPlayerInfo(player);
        }
    }
}
