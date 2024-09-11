using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Stats
    [SerializeField] private float speed = 5f;
    [SerializeField] private float timeInAir = 2f;
    [SerializeField] private float jumpDistance = 3f;

    [Header("Mask for jump")]
    [SerializeField]
    private LayerMask jumpMask;
    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private SpriteRenderer sprite;
    private LayerMask currentMask;
    private float colliderExtentSize;

    //Component
    private Action currentState;
    private PlayerActions inputs;
    private Animator animator;

    //Inputs string 
    private string moveKey = "Move";
    private string jumpKey = "Jump";
    private string guideKey = "ToggleGuide";
    private string interactKey = "Interact";

    //AnimatorTriggers
    private string moveKeyPressed = "MoveKeyPressed";
    private string jumpTrigger = "JumpTrigger";

    //Jump management
    private float elapsedTimeInJump = 0f;
    private Vector2 jumpDirection;
    private Vector2 startPos;
    private Vector2 endPos;

    //Interactions
    private Interactable interactable;
    private Vector2 lastDirection;
    [SerializeField] private float rayDistance = 1f;

    private Guide guide;

    private void Start()
    {
        guide = FindObjectOfType<Guide>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputs = new();
        inputs.Enable();
        colliderExtentSize = GetComponent<Collider2D>().bounds.extents.x;
        sprite = GetComponent<SpriteRenderer>();
        
        SetModeMove();

        if (SceneManager.GetActiveScene().name == "LevelTutorial")
        {
            
        }
    }

    // Update is called once per frame
    private void Update()
    {
        currentState?.Invoke();
        CheckForInteractable();
        FreeInteractable();
    }

    private void SetModeMove() {
        sprite.color = Color.yellow;

        currentMask = groundedMask;
        currentState = DoActionMove;
    }

    private void DoActionMove()
    {
        animator.SetBool(moveKeyPressed, inputs.asset[moveKey].IsPressed());
        float lHorizontal = inputs.asset[moveKey].ReadValue<Vector2>().x;
        float lVertical = inputs.asset[moveKey].ReadValue<Vector2>().y;

        if (lHorizontal != 0 || lVertical != 0)
            lastDirection = new(lHorizontal, lVertical);

        //Movement with collisions checks
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, new(lHorizontal, 0), speed * Time.deltaTime, currentMask))
            transform.position += new Vector3(lHorizontal, 0) * (speed * Time.deltaTime);
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, new(0, lVertical), speed * Time.deltaTime, currentMask))
            transform.position += new Vector3(0, lVertical) * (speed * Time.deltaTime);

        if (inputs.asset[jumpKey].WasPressedThisFrame())
            SetModeJump();
        if (inputs.asset[interactKey].WasPerformedThisFrame())
            interactable?.Interact();
        if (inputs.asset[guideKey].WasPerformedThisFrame())
            DoActionToggleGuide();
    }

    private void SetModeJump() {
        sprite.color = Color.red;
        animator.SetTrigger(jumpTrigger);
        currentMask = jumpMask;
        elapsedTimeInJump = 0f;
        
        jumpDirection = inputs.asset[moveKey].ReadValue<Vector2>();
        startPos = transform.position;
        endPos = transform.position + new Vector3(jumpDirection.x,jumpDirection.y)* jumpDistance;

        currentState = DoActionJump;
    }

    private void DoActionJump()
    {
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, jumpDirection, speed * Time.deltaTime, currentMask)) {
            transform.position += new Vector3(jumpDirection.x, jumpDirection.y) * (jumpDistance * Time.deltaTime);
        }

        elapsedTimeInJump += Time.deltaTime;
        if (elapsedTimeInJump > timeInAir)
        {
            SetModeMove();
        }
    }

    private void CheckForInteractable()
    {
        RaycastHit2D[] lHit = Physics2D.CircleCastAll(transform.position, colliderExtentSize, lastDirection, rayDistance);
        foreach (RaycastHit2D hit in lHit)
        {
            Interactable lInteracter = hit.collider?.GetComponent<Interactable>();
            if (lInteracter is null)
                continue;
            
            interactable?.DeactivateHighlight();
            interactable = lInteracter;
            break;
        }

        interactable?.ActivateHighlight();
    }

    private void FreeInteractable()
    {
        if (interactable is null)
            return;

        Vector3 distanceToInteractable = interactable.transform.position - transform.position;
        if (distanceToInteractable.sqrMagnitude <= rayDistance * rayDistance)
            return;
        
        interactable?.DeactivateHighlight();
        interactable = null;
    }

    private void DoActionToggleGuide()
    {
        // TODO animation from bottom
        guide?.ToggleGuideDisplay();
    }
}
