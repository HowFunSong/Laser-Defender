using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Rank Config", fileName = "New Rank Config")]
public class RankConfSO : ScriptableObject
{
    [Header("GameOver")]
    [SerializeField] List<Sprite> rankSprites;
    [SerializeField] List<string> titles;
    public Sprite GetRankSprite(int rank) 
    {
        return rankSprites[rank];
    }

    public string GetSubtile(int rank) 
    {
        return titles[rank]; 
    }

    [Header("Ranking")]
    [SerializeField] List<string> playerNames;
    [SerializeField] List<int> scores;

    public void SortScores()
    {
        // Combine player names and scores into a list of tuples
        List<(string name, int score)> playerData = new List<(string name, int score)>();
        for (int i = 0; i < playerNames.Count; i++)
        {
            playerData.Add((playerNames[i], scores[i]));
        }

        // Sort the combined list by score
        playerData = playerData.OrderByDescending(player => player.score).ToList();

        // Separate the sorted data back into the original lists
        for (int i = 0; i < playerData.Count; i++)
        {
            playerNames[i] = playerData[i].name;
            scores[i] = playerData[i].score;
        }
    }

    public List<string> GetPlayerNames()
    {
        return playerNames;
    }

    public List<int> GetScores()
    {
        return scores;
    }


}
