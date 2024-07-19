using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //[SerializeField] Camera camera;
    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = 0.5f;

    Vector3 initiaPosition;

    void Start()
    {
        initiaPosition = transform.position;    
    }

    public void Play() 
    {
        StartCoroutine(Shake());
        transform.position = initiaPosition;
    }

    IEnumerator Shake() 
    {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration) 
        {
            transform.position = initiaPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initiaPosition;
    }
}
