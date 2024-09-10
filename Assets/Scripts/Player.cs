using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Stats
    [SerializeField] float speed = 5f;

    Action currentState;

    // Start is called before the first frame update
    void Start()
    {
        
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

    }
}
