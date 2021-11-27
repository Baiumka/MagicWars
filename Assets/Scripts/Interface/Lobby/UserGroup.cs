using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGroup 
{

    public static UserGroup currentGroup;

    int _id;
    List<LobbyUser> _groupUsers;
    LobbyUser _leader;

    public int id { get => _id; set => _id = value; }
    public List<LobbyUser> groupUsers { get => _groupUsers; set => _groupUsers = value; }
    public LobbyUser leader { get => _leader; set => _leader = value; }

    public UserGroup(int id, LobbyUser leader)
    {
        _groupUsers = new List<LobbyUser>();
        this._id = id;
        this._leader = leader;
    }

    public static void ClearUserGroupList()
    {
        currentGroup = null;
    }



}
