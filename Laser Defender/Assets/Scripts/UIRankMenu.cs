using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRankMenu : MonoBehaviour
{
    [SerializeField] RankConfSO rankConfSO;
    [SerializeField] List<TextMeshProUGUI> playerNameTexts;
    [SerializeField] List<TextMeshProUGUI> scoreTexts;
    
    private void Start()
    {
        GetTextField();
        Rank();    
    }

    public void Rank()
    {
        rankConfSO.SortScores();

        var sortedPlayerNames = rankConfSO.GetPlayerNames();
        var sortedScores = rankConfSO.GetScores();

        int n = Math.Min(sortedPlayerNames.Count, playerNameTexts.Count);


        for (int i = 0; i < n && i < sortedPlayerNames.Count; i++)
        {
            playerNameTexts[i].text = sortedPlayerNames[i];
        }

        for (int i = 0; i < n && i < sortedScores.Count; i++)
        {
            scoreTexts[i].text = sortedScores[i].ToString();
        }
    }

    private void GetTextField()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.gameObject.name.Contains("Name"))
            {
                playerNameTexts.Add(text);
            }
            else if (text.gameObject.name.Contains("Score"))
            {
                scoreTexts.Add(text);
            }
        }
    }



}
