using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Stats
    [SerializeField] float speed = 5f;
    [SerializeField] float timeInAir = 2f;
    [SerializeField] float jumpDistance = 3f;

    [Header("Mask for jump")]
    [SerializeField] LayerMask jumpMask;
    [SerializeField] LayerMask groundedMask;
    [SerializeField] SpriteRenderer sprite;
    LayerMask currentMask;
    float colliderExtentSize;

    //Component
    Action currentState;
    PlayerActions inputs;
    Animator animator;

    //Inputs string 
    string moveKey = "Move";
    string jumpKey = "Jump";
    string guideKey = "ToggleGuide";
    string interactKey = "Interact";

    //AnimatorTriggers
    string moveKeyPressed = "MoveKeyPressed";
    string jumpTrigger = "JumpTrigger";

    //Jump management
    float elapsedTimeInJump = 0f;
    Vector2 jumpDirection;
    Vector2 startPos;
    Vector2 endPos;

    //Interactions
    Interactable interactable;
    Vector2 lastDirection;
    [SerializeField] float rayDistance = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputs = new PlayerActions();
        inputs.Enable();
        colliderExtentSize = GetComponent<Collider2D>().bounds.extents.x;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SetModeMove();
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Invoke();
        CheckForInteractable();
        FreeInteractable();
    }

    void SetModeMove() {
        sprite.color = Color.yellow;

        currentMask = groundedMask;
        currentState = DoActionMove;
    }

    void DoActionMove()
    {
        animator.SetBool(moveKeyPressed, inputs.asset[moveKey].IsPressed());
        float lHorizontal = inputs.asset[moveKey].ReadValue<Vector2>().x;
        float lVertical = inputs.asset[moveKey].ReadValue<Vector2>().y;

        if (lHorizontal != 0 || lVertical !=0)  {
            lastDirection = new Vector2(lHorizontal, lVertical);
        }

        //Movement with collisions checks
        if (!Physics2D.CircleCast(transform.position,colliderExtentSize,new Vector2(lHorizontal,0),speed * Time.deltaTime,currentMask))
        {
            transform.position += new Vector3(lHorizontal, 0) * speed * Time.deltaTime;
        }
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, new Vector2(0, lVertical), speed * Time.deltaTime,currentMask))
        {
            transform.position += new Vector3(0, lVertical) * speed * Time.deltaTime;
        }

        if (inputs.asset[jumpKey].WasPressedThisFrame())
        {
            SetModeJump();
        }
        else if (inputs.asset[interactKey].WasPerformedThisFrame() && interactable != null)
        {
            interactable.Interact();
        }
    }

    void SetModeJump() {
        sprite.color = Color.red;
        animator.SetTrigger(jumpTrigger);
        currentMask = jumpMask;
        elapsedTimeInJump = 0f;
        
        jumpDirection = inputs.asset[moveKey].ReadValue<Vector2>();
        startPos = transform.position;
        endPos = transform.position + (new Vector3(jumpDirection.x,jumpDirection.y)* jumpDistance);

        currentState = DoActionJump;
    }

    void DoActionJump()
    {
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, jumpDirection, speed * Time.deltaTime, currentMask)) {
            transform.position += new Vector3(jumpDirection.x, jumpDirection.y) * jumpDistance * Time.deltaTime;
        }

        elapsedTimeInJump += Time.deltaTime;
        if (elapsedTimeInJump > timeInAir)
        {
            SetModeMove();
        }
    }

    void CheckForInteractable()
    {
        RaycastHit2D lHit = Physics2D.CircleCast(transform.position, colliderExtentSize, lastDirection, rayDistance);
        if (lHit.collider != null && lHit.collider.TryGetComponent<Interactable>(out Interactable lInteracter))
        {
            interactable = lInteracter;
            interactable.ActivateHighlight();
        }
    }

    void FreeInteractable()
    {
        if (interactable == null) return;

        Vector3 distanceToInteractable = interactable.transform.position - transform.position;
        if (distanceToInteractable.magnitude > rayDistance)
        {
            interactable.DeactivateHighlight();
            interactable = null;
        }
    }
}
