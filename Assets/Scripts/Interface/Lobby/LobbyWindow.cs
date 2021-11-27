using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyWindow : MonoBehaviour
{
    [SerializeField]
    Text playerListHead;

    [SerializeField]
    GameObject generalUserLabelPrefab;
    [SerializeField]
    GameObject groupUserLabelPrefab;
    [SerializeField]
    Transform lobbyUserList;
    [SerializeField]
    Transform userGroupList;
    [SerializeField]
    Button refreshButton;
    [SerializeField]
    MatchMakingPanel matchMakingPanel;

    [SerializeField]
    CurrentLobbyUserPanel currentLobbyUserPanel;
    
    

    List<GeneralUserLabelTile> shownGeneralLobbyUsersTileList;
    List<GroupUserLabelTile> shownGroupUsersPrefabs;
    List<Action> lobbyUserActionList;

    public void SubscribeEvents()
    {
        refreshButton.onClick.AddListener(RefreshLobbyUserList);

        Main.client.onJoinLobby += CommandToReloadUserList;
        Main.client.onUserEnterLobby += CommandToAddUserPrefab;
        Main.client.onUserExitLobby += CommandToRemoveUserPrefab;

        Main.client.onUserCreateGroup += ShowUserGroup;
        Main.client.onUserUpdateGroup += UpdateGroupList;
        Main.client.onMemberLeftGroup += RemoveMemberFromGroupList;
        Main.client.onUserJoinGroup += ShowUserGroup;
        Main.client.onUserKickedFromGroup += CloseUserGroup;
        matchMakingPanel.SubscriveEvents();

        lobbyUserActionList = new List<Action>();
        shownGeneralLobbyUsersTileList = new List<GeneralUserLabelTile>();

    }

    void Start()
    {
        userGroupList.gameObject.SetActive(false);
        if(Main.client.IsAlreadyFullLogin)
        {
            //Доп. проверка на логин пользователя т.к. иногда окно не успевает загрузиться, а событие уже произошло.
            lobbyUserActionList.Add(() => ReloadUserList(Main.client.GetCuurentLobbyUserList()));
        }
    }

    private void Update()
    {
        if(lobbyUserActionList != null)
        {
            if(lobbyUserActionList.Count > 0)
            {
                lobbyUserActionList[0]();
                lobbyUserActionList.Remove(lobbyUserActionList[0]);
            }
        }
    }

    private void UpdateGroupList()
    {
        ClearGrpupList();
        foreach (LobbyUser member in UserGroup.currentGroup.groupUsers)
        {
            //Рисуем префаб
            GameObject userPrefab = GameObject.Instantiate(groupUserLabelPrefab, userGroupList);
            GroupUserLabelTile lobbyUserPrefab = userPrefab.GetComponent<GroupUserLabelTile>();
            lobbyUserPrefab.SetUser(member);
            if(shownGroupUsersPrefabs == null) shownGroupUsersPrefabs = new List<GroupUserLabelTile>();
            shownGroupUsersPrefabs.Add(lobbyUserPrefab);
        }

        matchMakingPanel.UpdateBlocksSearchButtons(UserGroup.currentGroup.groupUsers.Count);
    }

    void RefreshLobbyUserList()
    {
        lobbyUserActionList.Add(() => ReloadUserList(Main.client.GetCuurentLobbyUserList()));        
    }

    void ShowUserGroup()
    {
        userGroupList.gameObject.SetActive(true);        
        UpdateGroupList();
        matchMakingPanel.UpdateBlocksSearchButtons(UserGroup.currentGroup.groupUsers.Count);
    }

    void RemoveMemberFromGroupList(LobbyUser member)
    {
        GroupUserLabelTile found = null;
        foreach (GroupUserLabelTile prefab in shownGroupUsersPrefabs)
        {
            if (prefab.lobbyUserInfo.nickName.Equals(member.nickName))
            {
                found = prefab;
                break;
            }
        }
        if (found != null)
        {
            shownGroupUsersPrefabs.Remove(found);
            Destroy(found.gameObject);
        }
        
        matchMakingPanel.UpdateBlocksSearchButtons(UserGroup.currentGroup.groupUsers.Count);
    }

    void CloseUserGroup()
    {
        shownGroupUsersPrefabs = null;
        
        foreach (Transform child in userGroupList)
        {
            Destroy(child.gameObject);
        }
        userGroupList.gameObject.SetActive(false);
        matchMakingPanel.UpdateBlocksSearchButtons(1);
    }


    void AddUserPrefab(LobbyUser user)
    {
        GeneralUserLabelTile found = null;
        foreach (GeneralUserLabelTile tile in shownGeneralLobbyUsersTileList)
        {
            if (tile.lobbyUserInfo.nickName.Equals(user.nickName))
            {
                found = tile;
                break;
            }
        }
        if (found == null)
        {
            GameObject userPrefab = GameObject.Instantiate(generalUserLabelPrefab, lobbyUserList);
            GeneralUserLabelTile lobbyUserTile = userPrefab.GetComponent<GeneralUserLabelTile>();
            lobbyUserTile.SetUser(user);
            shownGeneralLobbyUsersTileList.Add(lobbyUserTile);
        }
    }

    void CommandToReloadUserList(List<LobbyUser> lobbyUsers)
    {
        lobbyUserActionList.Add(() => ReloadUserList(lobbyUsers));
    }

    void CommandToRemoveUserPrefab(LobbyUser lobbyUser)
    {
        lobbyUserActionList.Add(() => RemoveUserPrefab(lobbyUser));
    }

    void CommandToAddUserPrefab(LobbyUser lobbyUser)
    {
        lobbyUserActionList.Add(() => AddUserPrefab(lobbyUser));
    }


    void RemoveUserPrefab(LobbyUser user)
    {
        GeneralUserLabelTile found = null;
        foreach (GeneralUserLabelTile prefab in shownGeneralLobbyUsersTileList)
        {
            if(prefab.lobbyUserInfo.nickName.Equals(user.nickName))
            {
                found = prefab;
                break;
            }
        }
        if(found != null)
        {
            shownGeneralLobbyUsersTileList.Remove(found);
            Destroy(found.gameObject);
        }
    }

    void ReloadUserList(List<LobbyUser> lobbyUsers)
    {
        ClearUserList();
        foreach(LobbyUser user in lobbyUsers)
        {
            AddUserPrefab(user);
            if (user == LobbyUser.currentUser)
            {
                currentLobbyUserPanel.SetUserInfo(user);
            }
        }
    }

    void ClearUserList()
    {
        shownGeneralLobbyUsersTileList.Clear();
        foreach (Transform item in lobbyUserList.transform)
        {
            GameObject.Destroy(item.gameObject);
        }
    }

    void ClearGrpupList()
    {
        foreach (Transform item in userGroupList.transform)
        {
            GameObject.Destroy(item.gameObject);
        }
    }

}
