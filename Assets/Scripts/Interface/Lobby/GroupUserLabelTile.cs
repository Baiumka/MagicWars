using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupUserLabelTile : UserLabelTile
{
    [SerializeField]
    Button kickButton;
    [SerializeField]
    Image leaderImage;
    [SerializeField]
    Button leaveButton;
    [SerializeField]
    Image leaveButtonBackgroundImage;

    protected override void DisplayUserInfo()
    {
        userNickNameTextField.text = lobbyUserInfo.nickName;
        if(lobbyUserInfo == LobbyUser.currentUser)
        {
            leaveButtonBackgroundImage.gameObject.SetActive(true);
        }

        if(UserGroup.currentGroup.leader == lobbyUserInfo)
        {
            leaderImage.gameObject.SetActive(true);
        }
        else
        {
            if(UserGroup.currentGroup.leader == LobbyUser.currentUser)
            {
                kickButton.gameObject.SetActive(true);
            }
        }
    }
    private void Start()
    {
        leaveButton.onClick.AddListener(OnClickLeaveButton);
        kickButton.onClick.AddListener(OnClickKickButton);
    }

    private void OnClickLeaveButton()
    {
        Main.client.LeaveGroup();
    }

    private void OnClickKickButton()
    {
        Main.client.KickGroupUser(lobbyUserInfo.id);
    }
}
