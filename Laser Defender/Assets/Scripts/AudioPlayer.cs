using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;
    
    
    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance ;
    //static AudioPlayer instance = this ; 這樣是錯的, 因為這個時間還沒有創建實例;

    public AudioPlayer GetInstance() 
    {
        return instance;
    }



    public void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if (instanceCount > 1)
        if (instance !=null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            //這裡代表切換場景的時候不要銷毀該物件, 而是保留到下一個場景
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip() 
    {
        if (shootingClip != null) 
        {
            AudioSource.PlayClipAtPoint(shootingClip,
                                        Camera.main.transform.position,
                                        shootingVolume);
        }
    }
    public void PlayOnDamage() 
    {
        if (damageClip != null)
        {
            AudioSource.PlayClipAtPoint(damageClip,
                                        Camera.main.transform.position,
                                        damageVolume);
        }
    }

    



}
