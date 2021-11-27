using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadWindow : MonoBehaviour
{
    [SerializeField]
    Slider progressBar;
    [SerializeField]
    Text progressText;

    bool isLoaded;
    
    void Start()
    {
        
    }

    public void SubscribeEvents()
    {
        Main.sceneLoader.onSceneChangeLoadedPercent += UpdateProgressBar;
    }

    void UpdateProgressBar(float progress)
    {
        if (progressBar != null)
        {
            progressBar.value = progress;
            if (progress >= progressBar.maxValue)
            {
                if (!isLoaded)
                {
                    isLoaded = true;
                    progressText.text = "Ожидание игроков";
                }
            }
        }
    }
}
