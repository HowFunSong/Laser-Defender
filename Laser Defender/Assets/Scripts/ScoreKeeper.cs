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
        string name = GetPlayerName();
        int currentScore = GetScore();
        
        /// not yet

        ///

    }
    //��srankConfSO�� top 10
    public void UpDateScores()
    {
        /// not yet

        ///

    }
}
