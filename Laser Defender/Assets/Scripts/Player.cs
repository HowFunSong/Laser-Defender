using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft = 0.5f;
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingTop = 5f;
    [SerializeField] float paddingBottom = 2f;
    [SerializeField] GameObject shieldPrefab;
    Vector2 rawInput;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;
    
    bool hasShield = false;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
        
    }
    
    void Update()
    {
        Move();
    }

    void InitBounds() 
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

    }


    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        // Clamp (check, min, max)
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        this.transform.position = newPos;
    }

    void OnMove(InputValue value) 
    {
        rawInput = value.Get<Vector2>();
        //Debug.Log(rawInput);
    }

    void OnFire(InputValue value)
    {
        if (shooter != null) 
        {
            shooter.isFiring = value.isPressed;
            //Debug.Log(value.isPressed);
        }
    }

    void OnParry(InputValue value) 
    {
        //Debug.Log(value.isPressed);
    }

}
