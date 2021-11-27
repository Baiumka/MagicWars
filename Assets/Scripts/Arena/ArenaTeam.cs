using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTeam 
{
    public int id;
    public List<ArenaPlayer> players;

    public static List<ArenaTeam> arenaTeams = new List<ArenaTeam>();
    
    public ArenaTeam (int id)
    {
        players = new List<ArenaPlayer>();
        this.id = id;
    }


    public static void ClearArenaPlayers()
    {
        foreach(ArenaTeam team in arenaTeams)
        {
            team.players.Clear();
        }
        arenaTeams.Clear();
    }

    public static void AddPlayerToTeam(ArenaPlayer player, int teamId)
    {
        ArenaTeam newTeam = GetTeamById(teamId);
        if (newTeam == null)
        {
            newTeam = new ArenaTeam(teamId);
            arenaTeams.Add(newTeam);
        }
        newTeam.players.Add(player);
    }

    public static ArenaTeam GetTeamById(int id)
    {
        foreach(ArenaTeam team in arenaTeams)
        {
            if(team.id == id)
            {
                return team;
            }
        }
        return null;
    }

}
