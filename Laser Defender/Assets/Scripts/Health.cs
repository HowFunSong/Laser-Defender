using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    LevelManager levelManager;


    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            //Take damage
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            audioPlayer.PlayOnDamage();
            damageDealer.Hit();
        }
        else 
        {
            //Debug.Log("collision happened... but damageDealer not found.");
        }
        
    }

    public int GetHealth()
    {
        return health;
    }

    private void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
        else
        {
            //Debug.Log("cameraShake not found");
        }
        
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            Destroy(gameObject);
            levelManager.LoadGameOver();
            return;
        }

        Destroy(gameObject);

    }

    void PlayHitEffect() 
    {
        if (hitEffect!=null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
            //instance.Play();
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }        
    }

   
}
