using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUser
{
    public string nickName;
    public int rating;
    public int id;


    public static List<LobbyUser> lobbyUsersList = new List<LobbyUser>();
    public static LobbyUser currentUser;
  

    public LobbyUser(string nickName, int rating, int id, bool isMe = false)
    {
        LobbyUser updatedUser = LobbyUser.GetLobbyUserById(id);
        if (updatedUser != null) lobbyUsersList.Remove(updatedUser);

        this.nickName = nickName;
        this.rating = rating;
        this.id = id;
        if(isMe)
        {
            currentUser = this;
        }

        lobbyUsersList.Add(this);
    }

    public static void ClearLobbyUserList()
    {
        currentUser = null; 
        lobbyUsersList.Clear();
    }

    public static LobbyUser GetLobbyUserById(int id)
    {
        foreach (LobbyUser user in lobbyUsersList)
        {
            if (user.id == id)
            {
                return user;
            }
        }
        return null;
    }

   
}
