using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;



// ���U���Ω�ǦC�� JSON �ƾ�
[System.Serializable]
public class ScoreData
{
    public string username;
    public int score;
}


public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] RankConfSO rankConfSO;
    [SerializeField] string playerName;


    [SerializeField] int score;
    static ScoreKeeper instance;
    //static AudioPlayer instance = this ; �o�ˬO����, �]���o�Ӯɶ��٨S���Ыع��;

    public ScoreKeeper GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() 
    {
        return score;
    }
    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName() 
    {
        TMP_InputField inputField = FindObjectOfType<TMP_InputField>();
        if ( inputField != null)
        {
            Debug.Log(inputField.text);
            playerName = inputField.text;
        }
    }

    public void ModifyScore(int value) 
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }

    public void ResetScore()
    {
        score = 0;
    }

    //�o�e���Z
    public void SendScore() 
    {
        StartCoroutine(SendScoreCoroutine());

    }

    private IEnumerator SendScoreCoroutine()
    {
        string url = "https://c719-140-116-247-195.ngrok-free.app/submit_score";
        string name = GetPlayerName();
        int currentScore = GetScore();

        // �c�� JSON �ƾ�
        string json = JsonUtility.ToJson(new ScoreData { username = name, score = currentScore });

        // �ЫؽШD
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending score: " + www.error);
        }
        else
        {
            Debug.Log("Score submitted successfully");
        }
    }


    // ����Ʀ�]
    public void GetLeaderboard()
    {
        StartCoroutine(GetLeaderboardCoroutine());
    }

    public IEnumerator GetLeaderboardCoroutine()
    {
        string url = "https://c719-140-116-247-195.ngrok-free.app/leaderboard";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching leaderboard: " + www.error);
        }
        else
        {
            Debug.Log("Leaderboard fetched successfully");
            string jsonResponse = www.downloadHandler.text;
            Debug.Log("Raw leaderboard data: " + jsonResponse);

            // �ѪRJSON�T���ç�srankConfSO
            List<KeyValuePair<string, int>> leaderboard = ParseLeaderboard(jsonResponse);
            Debug.Log("Parsed leaderboard count: " + leaderboard.Count);
            for (int i = 0; i < leaderboard.Count; i++)
            {
                Debug.Log("Rank " + (i + 1) + ": " + leaderboard[i].Key + " - " + leaderboard[i].Value);
            }
            UpdateRankConfSO(leaderboard);
        }
    }

    private List<KeyValuePair<string, int>> ParseLeaderboard(string jsonResponse)
    {
        List<KeyValuePair<string, int>> leaderboard = new List<KeyValuePair<string, int>>();

        // �ϥ� Newtonsoft.Json �ѪR JSON �T��
        var jsonArray = JsonConvert.DeserializeObject<List<List<object>>>(jsonResponse);

        foreach (var item in jsonArray)
        {
            string username = item[0].ToString();
            int score = int.Parse(item[1].ToString());
            leaderboard.Add(new KeyValuePair<string, int>(username, score));
        }

        return leaderboard;
    }

    private void UpdateRankConfSO(List<KeyValuePair<string, int>> leaderboard)
    {
        Debug.Log("Updating RankConfSO...");
        rankConfSO.UpdateTop10(leaderboard);
        Debug.Log("RankConfSO updated.");
    }
}
