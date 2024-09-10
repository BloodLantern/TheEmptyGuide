using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Stats
    [SerializeField] float speed = 5f;
    float colliderExtentSize;

    Action currentState;
    PlayerActions inputs;

    //Inputs string 
    string move = "Move";

    private void Awake()
    {
        inputs = new PlayerActions();
        inputs.Enable();
        colliderExtentSize = GetComponent<Collider2D>().bounds.extents.x;
    }

    void Start()
    {
        SetModeMove();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Invoke();
    }

    void SetModeMove() {
        currentState = DoActionMove;
    }

    void DoActionMove()
    {
        float lHorizontal = inputs.asset[move].ReadValue<Vector2>().x;
        float lVertical = inputs.asset[move].ReadValue<Vector2>().y;
        if (!Physics2D.CircleCast(transform.position,colliderExtentSize,new Vector2(lHorizontal,0),speed * Time.deltaTime))
        {
            transform.position += new Vector3(lHorizontal, 0) * speed * Time.deltaTime;
        }
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, new Vector2(0, lVertical), speed * Time.deltaTime))
        {
            transform.position += new Vector3(0, lVertical) * speed * Time.deltaTime;
        }

    }
}
