using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class MatchMakingPanel : MonoBehaviour
{
    [SerializeField]
    GameObject stopPanel;
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    SearchButton[] searchButtons;
    [SerializeField]
    Button stopButton;
    [SerializeField]
    Text timerText;
    [SerializeField]
    Text foundText;

    Stopwatch searchTime;


    public void SubscriveEvents()
    {
        Main.client.onSearchGameStarted += StartSearch;
        Main.client.onSearchGameStoped += StopSearch;
        Main.client.onUserCanLoad += StopSearch;
        Main.client.onFoundPlayersUpdated += UpdateFoundText;

        stopButton.onClick.AddListener(StopSearchClicked);
        foreach (SearchButton sb in searchButtons)
        {
            sb.button = sb.gameObject.GetComponent<Button>();
            sb.button.onClick.AddListener(() => StartSearchClicked(sb));
        }
    }

    void Start()
    {
        startPanel.SetActive(true);
        stopPanel.SetActive(false);
    }


    void UpdateFoundText(int current, int max)
    {
        foundText.text = current + "/" + max;
    }

    void Update()
    {
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if(searchTime != null)
        timerText.text = searchTime.Elapsed.ToString();
    }

    void StopSearch()
    {
        startPanel.SetActive(true);
        stopPanel.SetActive(false);
        searchTime.Stop();
        searchTime.Reset();
        searchTime = null;
    }
    void StartSearch()
    {
        startPanel.SetActive(false);
        stopPanel.SetActive(true);        
        searchTime = new Stopwatch();
        searchTime.Start();
    }
    void StartSearchClicked(SearchButton sb)
    {
        if (sb.teamSize != -1)
        {
            Main.client.SearchGame(sb.teamSize);
        }
        else
        {
            Main.client.EducationStart();
        }
    }


    void StopSearchClicked()
    {
        UnityEngine.Debug.Log("Stop click");
        Main.client.StopSearch();
    }

    public void UpdateBlocksSearchButtons(int count)
    {
        foreach (SearchButton sb in searchButtons)
        {
            if (count > sb.teamSize)
            {
                sb.MakeButtonDisable();
            }
            else
            {
                sb.MakeButtonEnable();
            }
            if(sb.teamSize == -1)
            {
                if(count == 1)
                {
                    sb.MakeButtonEnable();
                }
            }
        }
        
    }
}
