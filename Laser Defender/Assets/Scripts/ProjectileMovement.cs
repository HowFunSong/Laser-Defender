using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] bool clockwise = false;
    [SerializeField] bool canSpin = true;
    Transform ProjectileTransform;


    private void Start()
    {
        ProjectileTransform = GetComponent<Transform>();
        if (ProjectileTransform != null)
        {
            //Debug.Log("Get enemyTransform");
        }

 

    }

    private void Update()
    {
        if (canSpin)
        {
            if (clockwise)
            {
                ProjectileTransform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            }
            else
            {
                ProjectileTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }

        }
    }
}
