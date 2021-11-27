using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{ 

    [SerializeField]
    LobbyWindow lobbyWindow;
    [SerializeField]
    LoginWindow loginWindow;
    [SerializeField]
    RegisterWindow registerWindow;
    [SerializeField]
    LoadWindow loadWindow;
    [SerializeField]
    GameWindow gameWindow;
    [SerializeField]
    WaveLabelGenerator waveLabelGenerator;

    [SerializeField]
    GameObject dialogPrefab;
    [SerializeField]
    GameObject comboDialogPrefab;
    [SerializeField]
    GUISkin lobbyGuiSkin;
    public void SubscribeEvents()
    {        
        Main.client.onPlayerLogin += ShowLobbyWindow;
        Main.client.onUserMakeError += ShowErrorMessage;
        Main.client.onUserCanceledInvite += ShowUserCancelInviteMessage;
        Main.client.onUserReceiveInvite += ShowInviteDialog;
        Main.sceneLoader.onUserStartLoadScene += ShowLoadWindow;
        Main.sceneLoader.onLobbyLoaded += ShowLobbyWindow;
        Main.client.onGameStarted += DrawStartGame;
        Main.client.onRecivedNewCombo += ShowCombos;
        Main.client.EnemySpellKnown += ShowEnemySpell;

        lobbyWindow.SubscribeEvents();
        gameWindow.SubscribeEvents();
        loadWindow.SubscribeEvents();
    }

    void Start()
    {
        ShowLoginWindow();       
    }



    void DrawStartGame()
    {
        Debug.Log("DrawStartGame");
        gameWindow.DrawTeams();
        ShowGameWindow();
    }

    void HideAllWindows()
    {
        lobbyWindow.gameObject.SetActive(false);
        registerWindow.gameObject.SetActive(false);
        loginWindow.gameObject.SetActive(false);
        loadWindow.gameObject.SetActive(false);
        gameWindow.gameObject.SetActive(false);
    }

    void ShowGameWindow()
    {

        HideAllWindows();
        gameWindow.gameObject.SetActive(true);
    }

    public void ShowLoginWindow()
    {
        HideAllWindows();
        loginWindow.gameObject.SetActive(true);
    }

    public void ShowRegisterWindow()
    {
        HideAllWindows();
        registerWindow.gameObject.SetActive(true);
    }

    void ShowLobbyWindow()
    {

        if (Main.client.IsConnected())
        {
            HideAllWindows();
            lobbyWindow.gameObject.SetActive(true);
        }
        else
        {
            ShowLoginWindow();
        }
    }

    void ShowLoadWindow()
    {
        HideAllWindows();
        loadWindow.gameObject.SetActive(true);
    }


    private void Update()
    {
        
    }

    private void OnGUI()
    {
        /*
         * Rect position = new Rect((Screen.width / 2), 0, 500, 1000);

        string lobbyUsersIds = "";
        foreach(LobbyUser user in LobbyUser.lobbyUsersList)
        {            
            lobbyUsersIds += "<color='#000000'>" + user.nickName + "</color>" + "(" + "<color='#ffff00'>" + user.id + "</color>" + ")\n";
        }        
        GUI.Label(position, lobbyUsersIds);
        */
    }

    void ShowInviteDialog(int groupId, LobbyUser sender)
    {
        GameObject newDialog = GameObject.Instantiate(dialogPrefab, this.transform);
        LobbyDialog lobbyDialog = newDialog.GetComponent<LobbyDialog>();

        //Так и не придумал как это реализовать по нормальному...вот что получилось....
        StartCoroutine(lobbyDialog.ShowDialog("<b>" + sender.nickName + "</b> приглашает вас в группу!", LobbyDialog.DialogType.dtQuest, () => OnInviteAccepted(lobbyDialog)));
        lobbyDialog.inviteGroupId = groupId;
    }

    void ShowEnemySpell(List<Skill> combo)
    {
        GameObject newDialog = GameObject.Instantiate(comboDialogPrefab, this.transform);
        ComboDialog lobbyDialog = newDialog.GetComponent<ComboDialog>();
        lobbyDialog.ShowDialog(Skill.GetSkillByName("Show"), combo);
    }

    void ShowCombos(Skill mainSkill, List<Skill> combo)
    {
        GameObject newDialog = GameObject.Instantiate(comboDialogPrefab, this.transform);
        ComboDialog lobbyDialog = newDialog.GetComponent<ComboDialog>();
        lobbyDialog.ShowDialog(mainSkill, combo);
    }

    void OnInviteAccepted(LobbyDialog lobbyDialog)
    {
        if (lobbyDialog.result == LobbyDialog.DialogResult.btYes)
        {
            Main.client.AcceptGroupInvite(lobbyDialog.inviteGroupId);
        }
        else
        {
            Main.client.CancelGroupInvite(lobbyDialog.inviteGroupId);
        }
        Destroy(lobbyDialog.gameObject);
    }

    public void ShowErrorMessage(string message)
    {
        waveLabelGenerator.ShowErrorMessage(message);
    }
    public void ShowMessage(string message)
    {
        waveLabelGenerator.ShowMessage(message);
    }

    void ShowUserCancelInviteMessage(LobbyUser user)
    {
        waveLabelGenerator.ShowMessage("<color='#aaaa00'><b>" + user.nickName + "</b></color> отклонил ваше приглашение.");
    }

    


}
