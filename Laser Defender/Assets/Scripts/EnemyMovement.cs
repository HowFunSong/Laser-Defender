using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Spin")]
    [SerializeField] bool canSpin = true;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] bool clockwise =false;
    
    [Header("Swin")]
    [SerializeField] bool canSwin = false;
    [SerializeField] float swinAngle = 45f;
    [SerializeField] float swinSpeed = 2f;

    Transform enemyTransform;
    private float swinTimer = 0f;
    private float previousAngle = 0f;

    private void Start()
    {
        enemyTransform = GetComponent<Transform>();
        if (enemyTransform != null)
        {
            //Debug.Log("Get enemyTransform");
        }
    }

    private void Update()
    {
        if (canSpin)
        {
            Spin();
        }
        else if (canSwin)
        {
            Swin();
        }

    }

    private void Swin()
    {
        swinTimer += Time.deltaTime * swinSpeed;
        float angle = Mathf.Sin(swinTimer) * swinAngle;
        float angleDelta = angle - previousAngle;
        enemyTransform.Rotate(0, 0, angleDelta);
        previousAngle = angle;
    }

    private void Spin()
    {
        if (clockwise)
        {
            enemyTransform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        else
        {
            enemyTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
