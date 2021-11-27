using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLobbyUserPanel : MonoBehaviour
{

    [SerializeField]
    Text nickNameText;

   

    public void SetUserInfo(LobbyUser lobbyUser)
    {
        DisplayUserInfo(lobbyUser);
    }

    void DisplayUserInfo(LobbyUser lobbyUser)
    {
        nickNameText.text = lobbyUser.nickName;
        
    }

}
