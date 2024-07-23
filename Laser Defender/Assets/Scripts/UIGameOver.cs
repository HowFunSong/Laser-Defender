using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [Header("subtile")]
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] RankConfSO rankConfSO;
    [SerializeField] TextMeshProUGUI subTitle;


    [Header("score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    
    

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        scoreText.text = "Your Score:\r\n" + scoreKeeper.GetScore().ToString();
        RenderRankImageAndSubtitle(scoreKeeper.GetScore());
        scoreKeeper.SendScore();
    }
    
        

    private void RenderRankImageAndSubtitle(int score) 
    {
        switch (score)
        {
            case int n when (n > 10000):
                subTitle.text = rankConfSO.GetSubtitle(4);
                image.sprite = rankConfSO.GetRankSprite(4);
                Debug.Log(rankConfSO.GetSubtitle(4));
                break;
            case int n when (n > 6000):
             
                subTitle.text = rankConfSO.GetSubtitle(3);
                image.sprite = rankConfSO.GetRankSprite(3);
                Debug.Log(rankConfSO.GetSubtitle(3));
                break;

            case int n when (n > 3000):
                
                subTitle.text = rankConfSO.GetSubtitle(2);
                image.sprite = rankConfSO.GetRankSprite(2);
                Debug.Log(rankConfSO.GetSubtitle(2));
                break;

            case int n when (n > 1000):
              
                subTitle.text = rankConfSO.GetSubtitle(1);
                image.sprite = rankConfSO.GetRankSprite(1);
                Debug.Log(rankConfSO.GetSubtitle(1));
                break;

            default:
               
                subTitle.text = rankConfSO.GetSubtitle(0);
                image.sprite = rankConfSO.GetRankSprite(0);
                Debug.Log(rankConfSO.GetSubtitle(0));
                break;
        }
    }
}
