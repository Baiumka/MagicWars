using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UserLabelTile : MonoBehaviour
{
    [SerializeField]
    protected Text userNickNameTextField;

    public LobbyUser lobbyUserInfo;

    public void SetUser(LobbyUser lobbyUser)
    {
        lobbyUserInfo = lobbyUser;
        DisplayUserInfo();
    }

    protected abstract void DisplayUserInfo();

    

 
}
