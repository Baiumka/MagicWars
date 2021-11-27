using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X;
using Sfs2X.Logging;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using System;

public class Client : MonoBehaviour
{
    [SerializeField]
    string zoneName;

    private SmartFox sfs;
    private string nickname;
    private string password;
    private bool isAlreadyFullLogin;


    public delegate void LoginHandler(string nickName);
    public delegate void StringHandler(string str);
    public delegate void JoinLobbyHandler(List<LobbyUser> lobbyUserList);
    public delegate void LobbyUserHandler(LobbyUser lobbyUser);
    public delegate void ErrorHandler(string message);
    public delegate void InviteHandler(int groupId, LobbyUser sender);
    public delegate void GroupHandler(UserGroup userGroup);
    public delegate void GroupMembersHandler(List<LobbyUser> members, LobbyUser leader);
    public delegate void VoidHandler();
    public delegate void FoundPlayersHandler(int quantity, int maxSize);
    public delegate void ArenaPlayerHandler(ArenaPlayer player);
    public delegate void SkillHandler(Skill skill);
    public delegate void BuffHandler(Buff buff);
    public delegate void ComboHandler(Skill mainSkill, List<Skill> combo);
    public delegate void EnemySpellHandler(List<Skill> combo);
    public delegate void IntHandler(int i);
    public delegate void SpellHandler(List<UsedSkill> usedSkills);
    public delegate void EvadeHandler(int chance, double random, string dealer);
    public delegate void AddTimeHadler(int value, string dealer);
    public delegate void MissHandler(int chance, double random);
    public delegate void DamageHandler(int hp, int def, string dealer, bool isCrit, bool isIce);
    public delegate void EffectDamageHandler(int hp, string reason, string dealer, bool isIce);
    public delegate void TakeBuffHandler(string buff, int turns, string dealer);
    public delegate void ClearHandler(int type, string dealer);
    public delegate void HealHandler(int hp, bool isIce);

    public event VoidHandler onPlayerLogin;
    public event LobbyUserHandler onUserEnterLobby;
    public event LobbyUserHandler onUserExitLobby;
    public event JoinLobbyHandler onJoinLobby;
    public event ErrorHandler onUserMakeError;
    public event VoidHandler onUserCreateGroup;
    public event InviteHandler onUserReceiveInvite;
    public event LobbyUserHandler onUserCanceledInvite;
    public event LobbyUserHandler onMemberLeftGroup;
    public event VoidHandler onUserUpdateGroup;
    public event VoidHandler onUserJoinGroup;
    public event VoidHandler onUserKickedFromGroup;
    public event VoidHandler onSearchGameStarted;
    public event VoidHandler onSearchGameStoped;
    public event FoundPlayersHandler onFoundPlayersUpdated;
    public event VoidHandler onUserCanLoad;
    public event VoidHandler onGameStarted;
    public event ArenaPlayerHandler onPlayerStatsUpdated;
    public event VoidHandler onGameWon;
    public event VoidHandler onGameLost;
    public event VoidHandler onReturnToLobby;
    public event VoidHandler onUserDisconnect;
    public event SkillHandler onRecivedNewSkill;
    public event BuffHandler onRecivedNewBuff;
    public event ComboHandler onRecivedNewCombo;
    public event ArenaPlayerHandler newEnemyFound;
    public event VoidHandler onBeginMyTurn;
    public event VoidHandler onBeginEnemyTurn;
    public event IntHandler onFightSecondsTick;
    public event ArenaPlayerHandler onArenaPlayerUpdated;
    public event SpellHandler onSpellUpdated;
    public event SkillHandler onUserCastOnEnemy;
    public event SkillHandler onUserCastOnSelf;
    public event SkillHandler onEnemyCastOnUser;
    public event SkillHandler onEnemyCastOnSelf;
    public event IntHandler onEnemyUpdatedSpell;
    public event VoidHandler onUserDead;
    public event VoidHandler onEnemyDead;
    public event VoidHandler onUserGoToShadow;
    public event VoidHandler onEnemyGoToShadow;
    public event StringHandler onSkillWasUseless;
    public event VoidHandler onUserChengeEnemy;
    public event VoidHandler onEnemyChengeEnemy;
    public event EvadeHandler onUserEvade;
    public event EvadeHandler onEnemyEvade;
    public event AddTimeHadler onUserAddedTime;
    public event AddTimeHadler onEnemyAddedTime;
    public event MissHandler onUserMissed;
    public event MissHandler onEnemyMissed;
    public event DamageHandler onUserTakeDamage;
    public event DamageHandler onEnemyTakeDamage;
    public event EffectDamageHandler onUserTakeEffectDamage;
    public event EffectDamageHandler onEnemyTakeEffectDamage;
    public event TakeBuffHandler onUserTakeBuff;
    public event TakeBuffHandler onEnemyTakeBuff;
    public event ClearHandler onUserClear;
    public event ClearHandler onEnemyClear;
    public event HealHandler onUserTakeHeal;
    public event HealHandler onEnemyTakeHeal;
    public event VoidHandler SpellKnown;
    public event EnemySpellHandler EnemySpellKnown;
    public bool IsAlreadyFullLogin { get => isAlreadyFullLogin; }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sfs != null) sfs.ProcessEvents();
    }

    public void Connect(string ip, int port, string nick, string pass)
    {
        sfs = new SmartFox();
        ConfigData cfg = new ConfigData();
        cfg.Host = ip;
        cfg.Port = port;
        cfg.Zone = zoneName;

        nickname = nick;
        password = pass;

        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        //connection.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        //sfs.AddEventListener(SFSBuddyEvent.BUDDY_LIST_INIT, OnBuddyListInit);
        // sfs.AddEventListener(SFSBuddyEvent.BUDDY_ADD, OnBuddyListUpdate);
        sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
        //sfs.AddEventListener(SFSEvent.ROOM_ADD, OnRoomAdded);
        sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
        sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
        //sfs.AddEventListener(SFSBuddyEvent.BUDDY_ERROR, OnBuddyError);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
        //sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
        Debug.Log(ip + ":" + port + " z:" + zoneName);
        sfs.Connect(cfg);
    }

    private void OnLogin(BaseEvent e)
    {      
        onPlayerLogin?.Invoke(); 
    }

    private void OnConnection(BaseEvent e)
    {
        Debug.Log(nickname);
        sfs.Send(new Sfs2X.Requests.LoginRequest(nickname, password));
    }   

    private void OnConnectionLost(BaseEvent e)
    {
        
        try
        {
            sfs.Disconnect();
            sfs = null;
        }
        catch
        {
            Application.Quit();
        }        
        onSearchGameStoped.Invoke();
        onUserKickedFromGroup.Invoke();
        onUserDisconnect?.Invoke();

        LobbyUser.ClearLobbyUserList();
        UserGroup.ClearUserGroupList();
    }

    private void OnExtensionResponse(BaseEvent e)
    {
        string cmd = (string)e.Params["cmd"];
        SFSObject dataObject = (SFSObject)e.Params["params"];
        //Debug.Log("Recive command: " + cmd);
        switch (cmd)
        {
            case "UpdateSkillValue":
                string skillName = dataObject.GetUtfString("name");
                string enemyDiscription = dataObject.GetUtfString("enemyDiscription");
                string friendlyDiscription = dataObject.GetUtfString("friendlyDiscription");
                Skill.GetSkillByName(skillName).UpdateDesriprion(enemyDiscription, friendlyDiscription);
                break;
            case "NextTurn":
                if (!ArenaPlayer.currentArenaPlayer.isDead)
                {
                    int turn = dataObject.GetInt("Turn");
                    ArenaTeam arenaTeam = ArenaTeam.GetTeamById(turn);
                    if (arenaTeam.players.Contains(ArenaPlayer.currentArenaPlayer)) onBeginMyTurn?.Invoke();
                    else onBeginEnemyTurn?.Invoke();
                }
                break;
            case "TimeFight":
                if (!ArenaPlayer.currentArenaPlayer.isDead)
                {
                    int seconds = dataObject.GetInt("seconds");
                    int id = dataObject.GetInt("id");
                    onFightSecondsTick?.Invoke(seconds);
                }
                break;
            case "YouHaveNewEnemy":
                string player = dataObject.GetUtfString("YourEnemy");
                ArenaPlayer arenaPlayer = ArenaPlayer.GetPlayerByName(player);
                newEnemyFound?.Invoke(arenaPlayer);
                break;

            case "ComboSkills":
                int count = dataObject.GetInt("count");
                Skill skill = Skill.GetSkillByName(dataObject.GetUtfString("comboSkill"));
                List<Skill> comboSkills = new List<Skill>();
                for (int i = 0; i < count; i++)
                {
                    comboSkills.Add(Skill.GetSkillByName(dataObject.GetUtfString("Skill_" + i)));
                }                
                onRecivedNewCombo?.Invoke(skill, comboSkills);
                break;
        
            case "NewSkill":
                skillName = dataObject.GetUtfString("name");
                string displayName = dataObject.GetUtfString("displayName");
                string displayCombo = dataObject.GetUtfString("displayCombo");
                enemyDiscription = dataObject.GetUtfString("enemyDiscription");
                friendlyDiscription = dataObject.GetUtfString("friendlyDiscription");
                string color = dataObject.GetUtfString("color");
                int tier = dataObject.GetInt("tier");
                Skill recivedSkill = new Skill(skillName, displayName, displayCombo, enemyDiscription, friendlyDiscription, color, tier);
                onRecivedNewSkill?.Invoke(recivedSkill);
                break;
            case "NewBuff":
                string buffName = dataObject.GetUtfString("name");
                string displayBuffName = dataObject.GetUtfString("displayName");
                string discr = dataObject.GetUtfString("discription");
                bool isGood = dataObject.GetBool("isGood");
                Buff recivedBuff = new Buff(buffName, displayBuffName, discr, isGood);
                onRecivedNewBuff?.Invoke(recivedBuff);
                break;

            case "UpdatePlayerStats":

                string name = dataObject.GetUtfString("name");
                ArenaPlayer foundPlayer = ArenaPlayer.GetPlayerByName(name);

                int hp = dataObject.GetInt("hp");
                int maxHP = dataObject.GetInt("maxHP");
                int defend = dataObject.GetInt("defend");
                int evade = dataObject.GetInt("evade");
                int accuracy = dataObject.GetInt("accuracy");
                int critChance = dataObject.GetInt("critChance");
                int critRate = dataObject.GetInt("critRate");
                bool isDead = dataObject.GetBool("isDead");
                if (foundPlayer == null)
                {
                    string message = "Найде несуществующий игрок: " + name;
                    onUserMakeError?.Invoke(message);
                    Debug.Log(message);
                }
                else
                {
                    foundPlayer.hp = hp;
                    foundPlayer.maxHP = maxHP;
                    foundPlayer.defend = defend;
                    foundPlayer.evade = evade;
                    foundPlayer.accuracy = accuracy;
                    foundPlayer.critChance = critChance;
                    foundPlayer.critRate = critRate;
                    foundPlayer.isDead = isDead;
                    onPlayerStatsUpdated?.Invoke(foundPlayer);
                }
                foundPlayer.ClearBuff();
                int buffCount = dataObject.GetInt("buffCount");
                for (int i = 0; i < buffCount; i++)
                {
                    AppliedBuff ab = new AppliedBuff(Buff.GetBuffByName(dataObject.GetUtfString("buffName_" + i)), dataObject.GetInt("buffTurn_" + i));
                    ab.value = dataObject.GetInt("buffValue_" + i);
                    foundPlayer.AddBuff(ab);
                }
                onArenaPlayerUpdated?.Invoke(foundPlayer);
                break;
            case "YouWin":
                onGameWon?.Invoke();
                break;
            case "YouLoose":
                onGameLost?.Invoke();
                break;
            case "ReturnToLobby":
                onReturnToLobby?.Invoke();
                break;
            case "StartGame":
                count = dataObject.GetInt("count");
                ArenaTeam.ClearArenaPlayers();
                for (int i = 0; i < count; i++)
                {
                    player = dataObject.GetUtfString("name_" + i);
                    int team = dataObject.GetInt("team_" + i);
                    if (player == sfs.MySelf.Name)
                    {
                        arenaPlayer = new ArenaPlayer(player, true);
                    }
                    else
                    {
                        arenaPlayer = new ArenaPlayer(player);
                    }
                    ArenaTeam.AddPlayerToTeam(arenaPlayer, team);                   
                }     
                onGameStarted?.Invoke();
                break;
            case "LoadUsersCount":

               /* for (User loadedUser_i : loadedUsers)
                {
                    respObj.putUtfString("name_" + i, loadedUser_i.getName());
                    i++;
                }
                respObj.putInt("count", i);
                respObj.putInt("maxSlot", maxSlots);
                */ 
                break;
            case "YouCanLoad":
                Skill.skillList.Clear();
                onUserCanLoad.Invoke();
                break;
            case "StopSearchGame":
                onSearchGameStoped.Invoke();
                break;
            case "StartSearchGame":
                onSearchGameStarted.Invoke();
                break;
            case "FoundSearchersForGame":
                int quantity = dataObject.GetInt("quantity");
                int maxSize = dataObject.GetInt("maxSize");
                onFoundPlayersUpdated.Invoke(quantity, maxSize);
                break;
            case "GroupCreated":
                int groupId = dataObject.GetInt("group");
                UserGroup newGroup = new UserGroup(groupId, LobbyUser.currentUser);
                newGroup.groupUsers.Add(LobbyUser.currentUser);
                UserGroup.currentGroup = newGroup;
                onUserCreateGroup?.Invoke();
                break;
            case "InviteUser":
                int invitedGroupId = dataObject.GetInt("group");
                int sender = dataObject.GetInt("sender");
                LobbyUser foundUser = LobbyUser.GetLobbyUserById(sender);
                onUserReceiveInvite?.Invoke(invitedGroupId, foundUser);
                break;

            case "CanceledInvite":
                int invitedUser = dataObject.GetInt("invitedUser");
                foundUser = LobbyUser.GetLobbyUserById(invitedUser);
                onUserCanceledInvite?.Invoke(foundUser);
                break;

            case "GroupMembers":
                int groupSize = dataObject.GetInt("size");
                List<LobbyUser> memberNames = new List<LobbyUser>(); 
                for (int i = 0; i < groupSize; i ++)
                {
                    int member = dataObject.GetInt(i+ "_member");
                    foundUser = LobbyUser.GetLobbyUserById(member);
                    memberNames.Add(foundUser);
                }
                int leader = dataObject.GetInt("leader");
                groupId = dataObject.GetInt("groupId");
                LobbyUser leaderUser = LobbyUser.GetLobbyUserById(leader);

                
                if (UserGroup.currentGroup == null)
                {
                    UserGroup userGroup = new UserGroup(groupId, leaderUser);
                    userGroup.groupUsers = memberNames;
                    UserGroup.currentGroup = userGroup;

                    onUserJoinGroup?.Invoke();                   
                }
                else
                {
                    UserGroup.currentGroup.groupUsers = memberNames;
                    UserGroup.currentGroup.leader = leaderUser;

                    onUserUpdateGroup?.Invoke();
                }
                
                break;

            case "KickUser":
                UserGroup.currentGroup = null;
                onUserKickedFromGroup?.Invoke();
                onUserMakeError?.Invoke("Вы покинули группу.");
                break;
            case "UserLeft":
                int leftUser = dataObject.GetInt("userId");
                foundUser = LobbyUser.GetLobbyUserById(leftUser);
                onMemberLeftGroup?.Invoke(foundUser);                
                break;
            case "UpdateSkillList":
                count = dataObject.GetInt("count");
                List<UsedSkill> reciveSpell = new List<UsedSkill>();
                for (int i = 0; i < count; i++)
                {
                    reciveSpell.Add(new UsedSkill(Skill.GetSkillByName(dataObject.GetUtfString("Skill_" + i)), dataObject.GetBool("Used_" + i)));
                }
                onSpellUpdated?.Invoke(reciveSpell);                
                break;
            case "ServerError":
                string msg = dataObject.GetUtfString("msg");
                onUserMakeError?.Invoke(msg);
                //Debug.Log("Неопознанная команда: " + cmd);
                break;
            case "CastSkill":
                skillName = dataObject.GetUtfString("skill");
                bool isAttack = dataObject.GetBool("isAttack");
                Skill castSkill = Skill.GetSkillByName(skillName);
                if (isAttack) onUserCastOnEnemy?.Invoke(castSkill);
                else onUserCastOnSelf?.Invoke(castSkill);
                break;
            case "EnemyCastSkill":
                skillName = dataObject.GetUtfString("skill");
                isAttack = dataObject.GetBool("isAttack");
                castSkill = Skill.GetSkillByName(skillName);
                if (isAttack) onEnemyCastOnUser?.Invoke(castSkill);
                else onEnemyCastOnSelf?.Invoke(castSkill);
                break;
            case "EnemySkillCount":
                count = dataObject.GetInt("count");
                onEnemyUpdatedSpell?.Invoke(count);
                break;
//#########################################################################################
            case "YouDead":
                ArenaPlayer.currentArenaPlayer.isDead = true;
                onUserDead?.Invoke();
                break;

            case "YourEnemyDead":
                onEnemyDead?.Invoke();
                break;

            case "UselessSkill":
                string skillStrName = dataObject.GetUtfString("skill");
                onSkillWasUseless?.Invoke(skillStrName);
                break;
            case "Shadow":
                onUserGoToShadow?.Invoke();
                break;
            case "EnemyShadow":
                onEnemyGoToShadow?.Invoke();
                break;
            case "ChengeEnemy":
                if (!ArenaPlayer.currentArenaPlayer.isDead)
                {
                    onUserChengeEnemy?.Invoke();
                }
                break;
            case "EnemyChengeEnemy":
                if (!ArenaPlayer.currentArenaPlayer.isDead)
                {
                    onEnemyChengeEnemy?.Invoke();
                }
                break;
            case "EvadeSkill":
                int chance = dataObject.GetInt("chance");
                double random = dataObject.GetDouble("random");
                string dealer = dataObject.GetUtfString("dealer");
                onUserEvade?.Invoke(chance, random, dealer);
                break;
            case "EnemyEvadeSkill":
                chance = dataObject.GetInt("chance");
                random = dataObject.GetDouble("random");
                dealer = dataObject.GetUtfString("dealer");
                onEnemyEvade?.Invoke(chance, random, dealer);
                break;
            case "AddTime":
                int value = dataObject.GetInt("chance");
                dealer = dataObject.GetUtfString("dealer");
                onUserAddedTime?.Invoke(value, dealer);
                break;
            case "EnemyAddTime":
                value = dataObject.GetInt("chance");
                dealer = dataObject.GetUtfString("dealer");
                onEnemyAddedTime?.Invoke(value, dealer);
                break;
            case "MissSkill":
                chance = dataObject.GetInt("chance");
                random = dataObject.GetDouble("random");
                onUserMissed?.Invoke(chance, random);
                break;
            case "EnemyMissSkill":
                chance = dataObject.GetInt("chance");
                random = dataObject.GetDouble("random");
                onEnemyMissed?.Invoke(chance, random);
                break;
            case "TakeDamage":
                hp = dataObject.GetInt("hp");
                bool isCrit = dataObject.GetBool("isCrit");
                bool isIce = dataObject.GetBool("isIce");
                int def = dataObject.GetInt("def");
                dealer = dataObject.GetUtfString("dealer");
                onUserTakeDamage?.Invoke(hp, def, dealer, isCrit, isIce);
                break;
            case "EnemyTakeDamage":
                hp = dataObject.GetInt("hp");
                isCrit = dataObject.GetBool("isCrit");
                isIce = dataObject.GetBool("isIce");
                def = dataObject.GetInt("def");
                dealer = dataObject.GetUtfString("dealer");
                onEnemyTakeDamage?.Invoke(hp, def, dealer, isCrit, isIce);
                break;
            case "TakeBuff":
                string buff = dataObject.GetUtfString("buff");
                int turns = dataObject.GetInt("turns");
                dealer = dataObject.GetUtfString("dealer");
                onUserTakeBuff?.Invoke(buff, turns, dealer);
                break;
            case "EnemyTakeBuff":
                buff = dataObject.GetUtfString("buff");
                turns = dataObject.GetInt("turns");
                dealer = dataObject.GetUtfString("dealer");
                onEnemyTakeBuff?.Invoke(buff, turns, dealer);
                break;
            case "TakeEffectDamage":
                hp = dataObject.GetInt("hp");
                string reason = dataObject.GetUtfString("reason");
                dealer = dataObject.GetUtfString("dealer");
                isIce = dataObject.GetBool("isIce");
                onUserTakeEffectDamage?.Invoke(hp, reason, dealer, isIce);
                break;
            case "EnemyTakeEffectDamage":
                hp = dataObject.GetInt("hp");
                reason = dataObject.GetUtfString("reason");
                dealer = dataObject.GetUtfString("dealer");
                isIce = dataObject.GetBool("isIce");
                onEnemyTakeEffectDamage?.Invoke(hp, reason, dealer, isIce);
                break;
            case "Clear":
                int type = dataObject.GetInt("type");
                dealer = dataObject.GetUtfString("dealer");
                onUserClear?.Invoke(type, dealer);
                break;
            case "EnemyClear":
                type = dataObject.GetInt("type");
                dealer = dataObject.GetUtfString("dealer");
                onEnemyClear?.Invoke(type, dealer);
                break;
            case "TakeHeal":
                hp = dataObject.GetInt("hp");
                isIce = dataObject.GetBool("isIce");
                onUserTakeHeal?.Invoke(hp, isIce);
                break;
            case "EnemyTakeHeal":
                hp = dataObject.GetInt("hp");
                isIce = dataObject.GetBool("isIce");
                onEnemyTakeHeal?.Invoke(hp, isIce);
                break;
            case "EnemyKnowYourSpell":
                SpellKnown?.Invoke();
                break;
            case "EnemySpell":
                count = dataObject.GetInt("count");
                List<Skill> enemySkill = new List<Skill>();
                for (int i = 0; i < count; i++)
                {
                    enemySkill.Add(Skill.GetSkillByName(dataObject.GetUtfString("Skill_" + i)));
                }               
                EnemySpellKnown?.Invoke(enemySkill);
                break;
            default:
                onUserMakeError?.Invoke("Неопознанная команда: " + cmd);
                Debug.Log("Неопознанная команда: " + cmd);
                break;
        }
    }

    private void OnRoomJoin(BaseEvent e)
    {
        Room room = (Room)e.Params["room"];
        if (room.Name.Equals("The Lobby"))
        {
            List<LobbyUser> lobbyUsers = GetCuurentLobbyUserList();
            onJoinLobby?.Invoke(lobbyUsers);
            isAlreadyFullLogin = true;
        }                       
    }

    private void OnUserEnterRoom(BaseEvent e)
    {
        Room room = (Room)e.Params["room"];        
        if (room.Name.Equals("The Lobby"))
        {
            User user = (User)e.Params["user"];
            LobbyUser lobbyUser = new LobbyUser(user.Name, GetRating(user), user.Id);
            onUserEnterLobby?.Invoke(lobbyUser);
        }        
    }

    private void OnUserExitRoom(BaseEvent e)
    {
        Room room = (Room)e.Params["room"];
        if (room.Name.Equals("The Lobby"))
        {
            User user = (User)e.Params["user"];
            LobbyUser lobbyUser = new LobbyUser(user.Name, GetRating(user), user.Id);
            onUserExitLobby?.Invoke(lobbyUser);
        }
    }

    private int GetRating(User user)
    {
        int rait = 0;
        try
        {
            rait = user.GetVariable("Rating").GetIntValue();
        }
        catch
        {

        }
        return rait;
    }
//==========================================================================================================================================================================================
    
    public bool IsConnected()
    {
        if (sfs == null) return false;
        return sfs.IsConnected;
    }

    public void InviteUserToGroup(LobbyUser user)
    {
        SFSObject obj = new SFSObject();
        obj.PutInt("userId", user.id);
        sfs.Send(new ExtensionRequest("InviteUser", obj, sfs.LastJoinedRoom));
    }

    public void AcceptGroupInvite(int inviteGroupId)
    {
        SFSObject obj = new SFSObject();
        obj.PutInt("groupId", inviteGroupId);
        sfs.Send(new ExtensionRequest("AcceptInvite", obj, sfs.LastJoinedRoom));
    }
    public void CancelGroupInvite(int inviteGroupId)
    {
        SFSObject obj = new SFSObject();
        obj.PutInt("groupId", inviteGroupId);
        sfs.Send(new ExtensionRequest("CancelInvite", obj, sfs.LastJoinedRoom));
    }

    public void LeaveGroup()
    {
        SFSObject obj = new SFSObject();
        sfs.Send(new ExtensionRequest("LeaveGroup", obj, sfs.LastJoinedRoom));
    }

    public void EducationStart()
    {
        SFSObject obj = new SFSObject();       
        sfs.Send(new ExtensionRequest("EducationStart", obj, sfs.LastJoinedRoom));
    }

    public void SearchGame(int teamSize)
    {
        SFSObject obj = new SFSObject();
        obj.PutInt("teamSize", teamSize);
        sfs.Send(new ExtensionRequest("SearchGame", obj, sfs.LastJoinedRoom));
    }

    public void StopSearch()
    {
        SFSObject obj = new SFSObject();        
        sfs.Send(new ExtensionRequest("StopSearch", obj, sfs.LastJoinedRoom));
    }

    public void ArenaLoaded()
    {
        SFSObject obj = new SFSObject();
        sfs.Send(new ExtensionRequest("ArenaLoaded", obj, sfs.LastJoinedRoom));
    }
    
    public void GiveDamageToPlayer(ArenaPlayer player)
    {
        SFSObject obj = new SFSObject();
        obj.PutUtfString("name", player.name);
        sfs.Send(new ExtensionRequest("GiveDamageToPlayer", obj, sfs.LastJoinedRoom));
    }

    public void SendSkip()
    {
        SFSObject obj = new SFSObject();
        sfs.Send(new ExtensionRequest("SkipTurn", obj, sfs.LastJoinedRoom));
    }

    public void UseSpell(bool isAttack)
    {
        /*if (gameInterface.isEducation && gameInterface.educationStep < 6)
        {
            if (gameInterface.educationStep == 5)
            {
                if (!isAttack)
                {
                    gameInterface.ShowErrorMessage("Атакуйте противника!");
                    return;
                }
                else
                {
                    gameInterface.educationStep++;
                }
            }
        }*/
        SFSObject obj = new SFSObject();
        obj.PutBool("isAttack", isAttack);
        sfs.Send(new ExtensionRequest("UseSpell", obj, sfs.LastJoinedRoom));
    }

    public void KickGroupUser(int userId)
    {
        SFSObject obj = new SFSObject();
        obj.PutInt("userId", userId);
        sfs.Send(new ExtensionRequest("KickGroupUser", obj, sfs.LastJoinedRoom));
    }

    public void GetComboList(Skill skill)
    {
        SFSObject obj = new SFSObject();
        obj.PutUtfString("skill", skill.name);
        sfs.Send(new ExtensionRequest("GetComboSkills", obj, sfs.LastJoinedRoom));
    }

    public void UseSkill(string skillName)
    {

        /*if (gameInterface.isEducation && gameInterface.educationStep < 6)
        {
            if (gameInterface.educationStep == 1)
            {
                if (skillName != "Fire")
                {
                    gameInterface.ShowErrorMessage("Для начала, используйте огонь.");
                    return;
                }
                else
                {
                    gameInterface.educationStep++;
                }
            }
            else if (gameInterface.educationStep == 4)
            {
                if (skillName != "Water")
                {
                    gameInterface.ShowErrorMessage("Теперь используйте элемент: Вода.");
                    return;
                }
                else
                {
                    gameInterface.educationStep++;
                }
            }
            else
            {
                return;
            }
        }*/
        SFSObject obj = new SFSObject();
        obj.PutUtfString("name", skillName);
        sfs.Send(new ExtensionRequest("UseSkill", obj, sfs.LastJoinedRoom));
    }
    //==========================================================================================================================================================================================
    public List<LobbyUser> GetCuurentLobbyUserList()
    {
        User me = sfs.MySelf;
        Room room = sfs.LastJoinedRoom;
        List<LobbyUser> lobbyUsers = new List<LobbyUser>();
        foreach (User user in room.UserList)
        {
            bool isMe = false;
            if (me == user) isMe = true;

            LobbyUser lobbyUser = new LobbyUser(user.Name, GetRating(user), user.Id, isMe);
            lobbyUsers.Add(lobbyUser);
        }
        return lobbyUsers;
    }

    
}
