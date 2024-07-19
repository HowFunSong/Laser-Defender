using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    [Header("AI Setup")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] bool isDouble  = false;

    public bool isFiring = false;
    //[HideInInspector] public bool isFiring = false;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        //if (audioPlayer != null)
        //{
        //    Debug.Log("Get the audioPlayer");
        //}
        //else 
        //{
        //    Debug.Log("AudioPlayer not Found");
        //}
    }

    void Start()
    {
        if (useAI) 
        {
            isFiring = true;
        }

    }

    void Update()
    {
        Fire();
    }

    void Fire() 
    {
        if (isFiring && firingCoroutine == null)
        {
            if (isDouble)
            {
                firingCoroutine = StartCoroutine(FireContinuouslyDouble());
            }
            else
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
        }
        else if (!isFiring && firingCoroutine!=null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }


    IEnumerator FireContinuously() 
    {
        while (true) 
        {
            GameObject instance= Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rigidbody = instance.GetComponent<Rigidbody2D>();
            if (rigidbody!=null) 
            {
                //Debug.Log(transform.up);
                rigidbody.velocity = transform.up * projectileSpeed;
            }


            Destroy(instance, projectileLifetime);
            float time = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            time = Mathf.Clamp(time, minimumFiringRate, float.MaxValue);


            //audioPlayer.PlayShootingClip();
            audioPlayer.GetInstance().PlayShootingClip();

            yield return new WaitForSeconds(time);
        }
        
    }


    IEnumerator FireContinuouslyDouble()
    {
        while (true)
        {

            Vector3 xoffset = new Vector3(0.3f, 0f, 0f);
            Vector3 yoffset = new Vector3(0f, 0.1f, 0f);

            Vector3 pos1 = transform.position + xoffset + yoffset;
            Vector3 pos2 = transform.position - xoffset + yoffset;

            GameObject instance_1 = Instantiate(projectilePrefab, pos1, Quaternion.identity);
            GameObject instance_2 = Instantiate(projectilePrefab, pos2, Quaternion.identity);


            Rigidbody2D rb1 = instance_1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = instance_2.GetComponent<Rigidbody2D>();

            if (rb1 != null && rb2 != null)
            {
                //Debug.Log(transform.up);
                rb1.velocity = transform.up * projectileSpeed;
                rb2.velocity = transform.up * projectileSpeed;

            }


            Destroy(instance_1, projectileLifetime);
            Destroy(instance_2, projectileLifetime);

            float time = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            time = Mathf.Clamp(time, minimumFiringRate, float.MaxValue);


            //audioPlayer.PlayShootingClip();
            audioPlayer.GetInstance().PlayShootingClip();

            yield return new WaitForSeconds(time);
        }

    }
}
