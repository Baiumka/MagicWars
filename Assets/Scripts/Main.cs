using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    Client myClient;
    [SerializeField]
    InterfaceManager myInterfacManager;
    [SerializeField]
    SceneLoader mySceneLoader;


    public static Client client;
    public static InterfaceManager interfaceManager;
    public static SceneLoader sceneLoader;

    void Start()
    {
        //Порядок крайне важон
        //1.Клиент
        //2.Загрузчик сцен
        //3.Интерфейс
        if(client == null)
        {
            myClient.gameObject.SetActive(true);
            client = myClient;
            DontDestroyOnLoad(client.gameObject);
        }
        if (sceneLoader == null)
        {
            mySceneLoader.gameObject.SetActive(true); 
            sceneLoader = mySceneLoader;
            DontDestroyOnLoad(sceneLoader.gameObject);
            sceneLoader.SubscribeEvents();
        }
        if (interfaceManager == null)
        {
            myInterfacManager.gameObject.SetActive(true);
            interfaceManager = myInterfacManager;
            DontDestroyOnLoad(interfaceManager.gameObject);
            interfaceManager.SubscribeEvents();
        }
        
    }

}
