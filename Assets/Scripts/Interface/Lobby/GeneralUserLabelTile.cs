using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUserLabelTile : UserLabelTile
{
    [SerializeField]
    Text userRatingTextField;
    [SerializeField]
    Button userAttackButton;
    [SerializeField]
    Button userInviteButton;


    private void Start()
    {
        if (userInviteButton != null) userInviteButton.onClick.AddListener(InviteUserToGroup);
    }

    void InviteUserToGroup()
    {
        Main.client.InviteUserToGroup(lobbyUserInfo);
    }

    protected override void DisplayUserInfo()
    {
        userNickNameTextField.text = lobbyUserInfo.nickName;
        userRatingTextField.text = lobbyUserInfo.rating.ToString();

        if (lobbyUserInfo == LobbyUser.currentUser)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 0.92f, 0.016f, 0.7f);
            GameObject.Destroy(userAttackButton.gameObject);
            GameObject.Destroy(userInviteButton.gameObject);
            userAttackButton = null;
            userInviteButton = null;
        }
    }
}
