using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using Sfs2X.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : MonoBehaviour
{
    [SerializeField]
    Button confirmButton;
    [SerializeField]
    Button exitButton;

    [SerializeField]
    InputField login;
    [SerializeField]
    InputField pass;
    [SerializeField]
    InputField nick;
    [SerializeField]
    InputField mail;

    [SerializeField]
    InputField ip;
    [SerializeField]
    InputField port;

    SmartFox sfs;
    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        exitButton.onClick.AddListener(OnCancel);
    }

    void OnConfirm()
    {
        if (login.text.Length < 4)
        {
            Main.interfaceManager.ShowErrorMessage("Слишком короткий логин");
            return;
        }
        if (pass.text.Length < 6)
        {
            Main.interfaceManager.ShowErrorMessage("Слишком короткий пароль");
            return;
        }
        if (nick.text.Length < 1)
        {
            Main.interfaceManager.ShowErrorMessage("Слишком короткий пароль");
            return;
        }
        if (mail.text.Length < 5)
        {
            Main.interfaceManager.ShowErrorMessage("Слишком короткий адрес почты");
            return;
        }

        Regex reg = new Regex(@"\b[^_+.+][-\w.]+@([a-z0-9\.-]+)\.([a-z\.]{2,6})$\b", RegexOptions.IgnoreCase);
        MatchCollection mc = reg.Matches(mail.text);
        if (mc.Count <= 0)
        {
            Main.interfaceManager.ShowErrorMessage("Неверный адрес почты");
            return;
        }

        //Регистрация пошла...
        Connect();

    }

    void Connect()
    {
        if(sfs == null)
        sfs = new SmartFox();
        

        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);

        sfs.Connect(ip.text, Convert.ToInt32(port.text));
    }

    void OnCancel()
    {
        Main.interfaceManager.ShowLoginWindow();
    }

    void OnConnection(BaseEvent e)
    {
        if ((bool)e.Params["success"])
        {
            sfs.Send(new Sfs2X.Requests.LoginRequest("", "", "Signup"));
        }
    }

    private void OnLogin(BaseEvent e)
    {
        ISFSObject request = new SFSObject();
        request.PutUtfString("login", login.text);
        request.PutUtfString("pass", pass.text);
        request.PutUtfString("mail", mail.text);
        request.PutUtfString("nickname", nick.text);

        sfs.Send(new ExtensionRequest("$SignUp.Submit", request));

    }

    private void OnLoginError(BaseEvent e)
    {
        DisconnectSignUp();
        Main.interfaceManager.ShowMessage("SignUp LogIn Error");
    }

    void OnExtensionResponse(BaseEvent evt)
    {
        string cmd = (string)evt.Params["cmd"];
        ISFSObject objIn = (SFSObject)evt.Params["params"];        
        if (cmd == "$SignUp.Submit")
        {
            if (objIn.ContainsKey("errorMessage"))
            {
                string msg = "Ошибка регистрации: " + objIn.GetUtfString("errorMessage");
                Main.interfaceManager.ShowMessage(msg);
                Debug.Log(msg);
                
            }
            if (objIn.ContainsKey("success"))
            {              
                login.text = "";
                pass.text = "";
                nick.text = "";
                mail.text = "";

                Main.interfaceManager.ShowLoginWindow();

                Main.interfaceManager.ShowMessage("Успешно.");
               
            }
            DisconnectSignUp();
        }
    }

    private void DisconnectSignUp()
    { 
        if(sfs != null)
        {
            sfs.Disconnect();
            sfs = null;
        }
    }


    private void OnApplicationQuit()
    {
        DisconnectSignUp();
    }

    void Update()
    {
        if (sfs != null) sfs.ProcessEvents();
    }

}
