using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    //Stats
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float timeInAir = 2f;
    [SerializeField]
    private float jumpHeight = 1f;

    [Tooltip("Mask for jump")]
    [SerializeField]
    private LayerMask jumpMask;
    [SerializeField]
    private LayerMask groundedMask;
    [SerializeField]
    private SpriteRenderer sprite;
    private LayerMask currentMask;
    private float colliderExtentSize;

    //Component
    private Action currentState;
    private PlayerActions inputs;
    private Animator animator;

    //Inputs string 
    private const string MoveKey = "Move";
    private const string JumpKey = "Jump";
    private const string GuideKey = "ToggleGuide";
    private const string InteractKey = "Interact";

    //AnimatorTriggers
    private const string RunAnimState = "Running";
    private const string JumpAnimState = "Jump";
    
    private static readonly int RunAnimStateId = Animator.StringToHash(RunAnimState);
    private static readonly int JumpAnimStateId = Animator.StringToHash(JumpAnimState);

    //Jump management
    private float elapsedTimeInJump;
    private Vector2 jumpStart;
    private Transform animatorTransform;
    private Vector2 jumpDestination;

    [SerializeField]
    private float jumpDetectionDistance = 3f;

    //Interactions
    private Interactable interactable;
    private Vector2 lastDirection;
    [SerializeField]
    private float rayDistance = 1f;

    private Guide guide;

    private CapsuleCollider2D collider;

    private void Awake()
    {
        guide = GetComponent<Guide>();
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        inputs = new();
        inputs.Enable();
        colliderExtentSize = GetComponent<Collider2D>().bounds.extents.x;
        sprite = GetComponent<SpriteRenderer>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);
            if (!t.TryGetComponent<Animator>(out _))
                continue;
            
            animatorTransform = t;
            break;
        }
        
        SetModeMove();

        if (SceneManager.GetActiveScene().name == "Level0")
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

    public void SetModeMove()
    {
        currentMask = groundedMask;
        currentState = DoActionMove;
    }

    private void DoActionMove()
    {
        animator.SetBool(RunAnimStateId, inputs.asset[MoveKey].IsPressed());
        float lHorizontal = inputs.asset[MoveKey].ReadValue<Vector2>().x;
        float lVertical = inputs.asset[MoveKey].ReadValue<Vector2>().y;

        float scaleXAbs = Mathf.Abs(transform.localScale.x);
        if (lHorizontal != 0f)
            transform.localScale = new(lHorizontal > 0f ? -scaleXAbs : scaleXAbs, transform.localScale.y, transform.localScale.z);

        if (lHorizontal != 0 || lVertical != 0)
            lastDirection = new(lHorizontal, lVertical);

        //Movement with collisions checks
        RaycastHit2D collision =
            Physics2D.CircleCast(transform.position, colliderExtentSize, new(lHorizontal, 0), speed * Time.deltaTime, currentMask);
        if (!collision || collision.collider.isTrigger)
            transform.position += new Vector3(lHorizontal, 0) * (speed * Time.deltaTime);

        collision = Physics2D.CircleCast(transform.position, colliderExtentSize, new(0, lVertical), speed * Time.deltaTime, currentMask);
        if (!collision || collision.collider.isTrigger)
            transform.position += new Vector3(0, lVertical) * (speed * Time.deltaTime);

        if (inputs.asset[JumpKey].WasPressedThisFrame())
            SetModeJump();
        if (inputs.asset[InteractKey].WasPerformedThisFrame())
            interactable?.Interact();
        if (inputs.asset[GuideKey].WasPerformedThisFrame())
            SetModeGuide();
    }

    private void SetModeJump()
    {
        animator.SetTrigger(JumpAnimStateId);
        currentMask = jumpMask;
        elapsedTimeInJump = 0f;
        jumpStart = animatorTransform.position;

        jumpDestination = Vector2.zero;
        
        Collider2D[] result = Physics2D.OverlapCircleAll(transform.position, jumpDetectionDistance);
        foreach (Collider2D hit in result)
        {
            if (!hit.transform.TryGetComponent(out JumpArea jumpArea))
                continue;

            jumpDestination = jumpArea.Other.transform.position;
            break;
        }

        currentState = DoActionJump;
    }

    private void DoActionJump()
    {
        float progress = elapsedTimeInJump / timeInAir;
        float newY = progress switch
        {
            < 0.4f => jumpStart.y,
            < 0.7f => Mathf.Lerp(jumpStart.y, jumpStart.y + jumpHeight, (progress - 0.4f) / 0.3f),
            < 0.75f => jumpStart.y + jumpHeight,
            _ => Mathf.Lerp(jumpStart.y + jumpHeight, jumpStart.y, (progress - 0.75f) / 0.25f)
        };
        Vector2 newPosition = progress switch
        {
            < 0.4f => transform.position,
            < 0.9f => Vector2.Lerp(jumpStart, jumpDestination, (progress - 0.4f) / 0.5f),
            _ => jumpDestination
        };

        if (jumpDestination != Vector2.zero)
            transform.position = newPosition;
        
        animatorTransform.localPosition = new(animatorTransform.localPosition.x, newY);

        elapsedTimeInJump += Time.deltaTime;
        if (elapsedTimeInJump > timeInAir)
        {
            animatorTransform.localPosition = new(animatorTransform.localPosition.x, jumpStart.y);
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

    public void SetModeGuide()
    {
        guide.ToggleGuideDisplay();
        
        currentState = DoActionGuide;
    }

    public void SetModeDummy()
    {
        currentState = null;
    }

    private void DoActionGuide()
    {
        if (inputs.asset[GuideKey].WasPerformedThisFrame())
            guide.ToggleGuideDisplay();

        if (!guide.Visible)
            SetModeMove();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene($"Level{char.GetNumericValue(SceneManager.GetActiveScene().name[^1]) + 1}");
    }
}
