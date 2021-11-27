using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public delegate void VoidHandler();
    public delegate void ProgressHandler(float percent);

    public event VoidHandler onUserStartLoadScene;
    public event VoidHandler onLobbyLoaded;
    public event ProgressHandler onSceneChangeLoadedPercent;

    private const string LOBBY_SCENE = "SmartFoxLobbyScene";
    private const string ARENA_SCENE = "ArenaScene";


    public void SubscribeEvents()
    {
        Main.client.onUserCanLoad += LoadArenaScene;
        Main.client.onReturnToLobby += ReturnToLobby;
        Main.client.onUserDisconnect += ReturnToLobby;
    }

    private void Start()
    {
        
    }

    private void LoadArenaScene()
    {
        onUserStartLoadScene?.Invoke();
        StartCoroutine(LoadSceneAsync(ARENA_SCENE));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == ARENA_SCENE)
        {
            
        }
        if (scene.name == LOBBY_SCENE)
        {            

        }
    }

    void ReturnToLobby()
    {
        onUserStartLoadScene?.Invoke();
        StartCoroutine(LoadSceneAsync(LOBBY_SCENE));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(scene);
        while (!oper.isDone)
        {
            float progress = Mathf.Clamp01(oper.progress / .9f);
            onSceneChangeLoadedPercent?.Invoke(progress);
            yield return null;
        }
        if (oper.isDone)
        {
            switch (scene)
            {
                case ARENA_SCENE:
                    Main.client.ArenaLoaded();
                    break;
                case LOBBY_SCENE:
                    onLobbyLoaded?.Invoke();
                    break;
            }
        }
    }

}
