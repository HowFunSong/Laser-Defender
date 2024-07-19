using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ScoreKeeper scoreKeeper;
    
    [Header("Health")]
    [SerializeField] Slider slider;
    [SerializeField] Health health;

    private int initialHealth;
    


    private void Awake()
    {
        // score
        text = this.GetComponentInChildren<TextMeshProUGUI>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        // health
        slider = this.GetComponentInChildren<Slider>();
        health = FindObjectOfType<Player>().gameObject.GetComponent<Health>();

        initialHealth = health.GetHealth();

        if (text == null)
        {
            Debug.Log("text not found...");
        }


        if (slider == null)
        {
            Debug.Log("slider not found...");
        }


        if (scoreKeeper == null)
        {
            Debug.Log("scoreKeeper not found...");
        }


        if (health == null)
        {
            Debug.Log("health not found...");
        }
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        // score 
        text.text = scoreKeeper.GetScore().ToString("00000000");

        //health
        float healthPercentage = (float)health.GetHealth() / initialHealth;
        slider.value = healthPercentage;
        //Debug.Log(slider.value);

    }
}
