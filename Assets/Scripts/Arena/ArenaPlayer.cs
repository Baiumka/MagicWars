using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPlayer 
{
    public static ArenaPlayer currentArenaPlayer;

    public string name;

    public int hp;
    public int maxHP;
    public bool isDead;
    public int defend;
    public int evade;
    public int accuracy;
    public int critChance;
    public int critRate;

    public List<AppliedBuff> buffList;

    public ArenaPlayer(string name, bool isMe = false)
    {
        this.name = name;
        buffList = new List<AppliedBuff>();
        if (isMe)
        {
            currentArenaPlayer = this;
        }
    }

    public void ClearBuff()
    {
        buffList.Clear();
    }
    public void AddBuff(AppliedBuff newBuff)
    {
        buffList.Add(newBuff);
    }

    public static ArenaPlayer GetPlayerByName(string name)
    {
        foreach (ArenaTeam team in ArenaTeam.arenaTeams)
        {
            foreach(ArenaPlayer player in team.players)
            {
                if (player.name == name) return player;
            }
        }
        return null;
    }

}
