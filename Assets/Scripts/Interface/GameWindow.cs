using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow : MonoBehaviour
{
    [SerializeField]
    GameObject teamPanelPrefab;
    [SerializeField]
    GameObject teamsPanelList;

    [SerializeField]
    GameObject skillButtonPrefab;

    [SerializeField]
    Transform skillPanelContent;

    [SerializeField]
    GameObject winTable;
    [SerializeField]
    GameObject looseTable;

    [SerializeField]
    ArenaTeamPanel myTeam;
    [SerializeField]
    ArenaTeamPanel enemyTeam;

    [SerializeField]
    UserBar myUserBar;
    [SerializeField]
    UserBar enemyUserBar;

    [SerializeField]
    Text turnText;
    [SerializeField]
    Text timeText;
    [SerializeField]
    Color friendColor;
    [SerializeField]
    Color enemyColor;
    [SerializeField]
    Transform spellPanelContent;
    [SerializeField]
    GUISkin nextTurnSkin;
    [SerializeField]
    Text enemySkillCountText;

    int guiTurn = 0;

    private void OnGUI()
    {
        if (guiTurn > 0)
        {
            Rect position = new Rect((Screen.width / 2) - 400, (Screen.height / 4) - guiTurn, 800f, 120f);
            GUIContent content = new GUIContent(turnText.text);
            nextTurnSkin.GetStyle("label").normal.textColor = turnText.color;
            GUI.Label(position, content, nextTurnSkin.GetStyle("label"));
        }
    }

    private void Update()
    {
        if (guiTurn > 0)
        {
            guiTurn--;
        }
    }




    public void SubscribeEvents()
    {
        Main.client.onGameWon += ShowWinTable;
        Main.client.onGameLost += ShowLooseTable;
        Main.client.onRecivedNewSkill += AddNewSkill;
        Main.client.newEnemyFound += ShowCurrentEnemy;
        Main.client.onBeginMyTurn += ShowMyTurn;
        Main.client.onBeginEnemyTurn += ShowEnemyTurn;
        Main.client.onFightSecondsTick += FightSecondsTick;
        Main.client.onArenaPlayerUpdated += UpdateArenaPlayer;
        Main.client.onSpellUpdated += UpdateSpell;
        Main.client.onEnemyUpdatedSpell += UpdateEnemySpell;
        Main.client.onUserDead += Die;

        Main.client.onUserChengeEnemy += ChangeEnemy;
        Main.client.onEnemyChengeEnemy += ChangeEnemy;

    

    }
    //#######################################################################################################################################
    
    
    
    void ChangeEnemy()
    {
        enemyUserBar.gameObject.SetActive(false);
        turnText.text = "Waiting for a new enemy";
        timeText.text = "";
    }

    void Die()
    {
        turnText.text = "Dead";
        turnText.color = Color.red;
    }

    void UpdateEnemySpell(int skillCount)
    {
        enemySkillCountText.text = skillCount.ToString();
    }
    void UpdateSpell(List<UsedSkill> spell)
    {
        foreach (Transform child in spellPanelContent)
        {
            Destroy(child.gameObject);
        }
        foreach (UsedSkill skill in spell)
        {
            GameObject skillButton = GameObject.Instantiate(skillButtonPrefab, spellPanelContent);
            skillButton.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
            skillButton.GetComponent<SkillButton>().SetSkill(skill);
            Destroy(skillButton.GetComponent<Button>());
        }

    }

    void UpdateArenaPlayer(ArenaPlayer arenaPlayer)
    {
        if (arenaPlayer == myUserBar.arenaPlayerInfo)
        {
            myUserBar.UpdateUserBarHealth();
            myUserBar.DrawBuffs();
        }
        if (arenaPlayer == enemyUserBar.arenaPlayerInfo)
        {
            enemyUserBar.UpdateUserBarHealth();
            enemyUserBar.DrawBuffs();
        }
    }

    void ShowCurrentEnemy(ArenaPlayer arenaPlayer)
    {
        enemyUserBar.gameObject.SetActive(true);
        enemyUserBar.SetInfo(arenaPlayer);


    }

    void FightSecondsTick(int seconds)
    {
        timeText.text = seconds.ToString();
    }

    void ShowMyTurn()
    {
        turnText.text = "Your Turn";
        turnText.color = friendColor;
        guiTurn = 50;
    }

    void ShowEnemyTurn()
    {
        turnText.text = "Enemy`s Turn";
        turnText.color = enemyColor;
        guiTurn = 50;
    }

    void HideAllTable()
    {
        winTable.SetActive(false);
        looseTable.SetActive(false);
    }

    void ShowWinTable()
    {
        HideAllTable();
        winTable.SetActive(true);
    }

    public void AddNewSkill(Skill newSkill)
    {
        if (newSkill.tier == 1)
        {
            GameObject skillButton = GameObject.Instantiate(skillButtonPrefab, skillPanelContent);
            skillButton.GetComponent<SkillButton>().SetSkill(newSkill);
        }
    }

    /*public void ShowCombos(Skill skill, List<Skill> combo)
    {

        foreach (Transform child in comboTabContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Skill s in combo)
        {
            GameObject skillButton = GameObject.Instantiate(skillButtonPrefab, comboTabContent.transform);
            Destroy(skillButton.GetComponent<Button>());
            skillButton.GetComponent<SkillButton>().SetSkill(s);
        }
        comboTabMainSkill.SetSkill(skill);
        comboTab.gameObject.SetActive(true);
        Chat.chat.SwitchTab(eChat.COMBO);
    }*/

    void ShowLooseTable()
    {   
        HideAllTable();
        looseTable.SetActive(true);
    }
    public void DrawTeams()
    {
        myUserBar.SetInfo(ArenaPlayer.currentArenaPlayer);

        HideAllTable();
        foreach (Transform child in myTeam.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in enemyTeam.gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ArenaTeam team in ArenaTeam.arenaTeams)
        {
            if (team.players.Contains(ArenaPlayer.currentArenaPlayer))
            {
                myTeam.SetTeamInfo(team);
            }
            else
            {
               enemyTeam.SetTeamInfo(team);
            }           
            
        }
    }
}
