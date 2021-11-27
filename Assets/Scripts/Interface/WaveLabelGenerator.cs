using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLabelGenerator : MonoBehaviour
{
    List<WaveLabel> waveSequence = new List<WaveLabel>();

    [SerializeField]
    GUISkin lobbyGuiSkin;
    void OnGUI()
    {
        if (waveSequence != null)
        {
            if (waveSequence.Count > 0)
            {
                int i = 0;
                foreach (WaveLabel errorLabel in waveSequence)
                {
                    Rect position = new Rect((Screen.width / 2) - 210, Screen.height - 100 - errorLabel.h + i * 5, 420f, 65f);
                    GUIContent content;
                    if (errorLabel.sprite != null) content = new GUIContent(errorLabel.mesasge, errorLabel.sprite.texture);
                    else content = new GUIContent(errorLabel.mesasge);
                    GUI.Label(position, content, lobbyGuiSkin.GetStyle("label"));
                    i++;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (waveSequence != null)
        {
            if (waveSequence.Count > 0)
            {
                for (int i = 0; i < waveSequence.Count; i++)
                {
                    if(waveSequence[i].UpWaweLabel(2))//Возвращает true, если WaveLabel достигла макс. высоты.
                    {
                        waveSequence.Remove(waveSequence[i]);
                    }
                }
            }
        }
    }

    public void ShowErrorMessage(string message)
    {
        waveSequence.Add(new WaveLabel("<color=#ff0000>Ошибка:</color> " + message));
    }

    public void ShowMessage(string message)
    {
        waveSequence.Add(new WaveLabel(message));
    }
}
