using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] RankConfSO rankConfSO;
    [SerializeField] string playerName;


    [SerializeField] int score;
    static ScoreKeeper instance;
    //static AudioPlayer instance = this ; 這樣是錯的, 因為這個時間還沒有創建實例;

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

    //發送成績
    public void SendScore() 
    {
        string name = GetPlayerName();
        int currentScore = GetScore();
        
        /// not yet

        ///

    }
    //更新rankConfSO中 top 10
    public void UpDateScores()
    {
        /// not yet

        ///

    }
}
