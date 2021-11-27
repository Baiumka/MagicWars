using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : MonoBehaviour
{
    [SerializeField]
    InputField ip;
    [SerializeField]
    InputField port;
    [SerializeField]
    InputField login;
    [SerializeField]
    InputField password;
    [SerializeField]
    Button loginButton;
    [SerializeField]
    Button registerButton;

    // Start is called before the first frame update
    void Start()
    {        
        string path = Environment.CurrentDirectory + @"\" + "login.cfg";
        if (File.Exists(path))
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    i++;
                    switch (i)
                    {
                        case 1:
                            ip.text = line;
                            break;
                        case 2:
                            port.text = line;
                            break;
                        case 3:
                            login.text = line;
                            break;
                        case 4:
                            password.text = line;
                            break;
                    }
                }
            }
        }
        loginButton.onClick.AddListener(OnLoginButtonClick);        
        registerButton.onClick.AddListener(OnRegisterButtonClick);        
    }


    void SaveConfig(string ip, string port, string name, string password)
    {
        string writePath = Environment.CurrentDirectory + @"\" + "login.cfg";
        string text = ip + "\n" + port + "\n" + name + "\n" + password;
        try
        {
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(text);
            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.Data);
        }
    }

    void OnLoginButtonClick()
    {
        SaveConfig(ip.text, port.text, login.text, password.text);
        Main.client.Connect(ip.text, Convert.ToInt32(port.text), login.text, password.text);
    }

    void OnRegisterButtonClick()
    {
        Main.interfaceManager.ShowRegisterWindow();
    }

}
