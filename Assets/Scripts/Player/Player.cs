using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Stats
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float timeInAir = 2f;
    [SerializeField]
    private float jumpDistance = 3f;
    private float jumpHeigth = 3f;
    Transform playerRenderer;

    [Tooltip("Mask for jump")]
    [SerializeField]
    private LayerMask jumpMask;
    [SerializeField]
    private LayerMask groundedMask;
    [SerializeField]
    LayerMask onlyJumpLayer;
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
    private const string MoveKeyPressed = "MoveKeyPressed";
    private const string JumpTrigger = "JumpTrigger";
    
    private static readonly int MoveKeyPressedId = Animator.StringToHash(MoveKeyPressed);
    private static readonly int JumpTriggerId = Animator.StringToHash(JumpTrigger);

    //Jump management
    private float elapsedTimeInJump;
    private Vector2 jumpDirection;
    private Vector2 startPos;
    private Vector2 endPos;

    //Interactions
    private Interactable interactable;
    private Vector2 lastDirection;
    [SerializeField]
    private float rayDistance = 1f;

    [SerializeField] AnimationCurve jumpCurve;

    private Guide guide;

    private void Start()
    {
        
        guide = GetComponent<Guide>();
        SetModeMove();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputs = new();
        inputs.Enable();
        colliderExtentSize = GetComponent<Collider2D>().bounds.extents.x;
        sprite = GetComponent<SpriteRenderer>();
        playerRenderer = transform.GetChild(0);


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

    public void SetModeMove() {
        sprite.color = Color.yellow;
        playerRenderer.transform.localPosition = new Vector3(0, .5f);
        currentMask = groundedMask;
        currentState = DoActionMove;
    }

    private void DoActionMove()
    {
        animator?.SetBool(MoveKeyPressedId, inputs.asset[MoveKey].IsPressed());
        float lHorizontal = inputs.asset[MoveKey].ReadValue<Vector2>().x;
        float lVertical = inputs.asset[MoveKey].ReadValue<Vector2>().y;

        if (lHorizontal != 0 || lVertical != 0)
            lastDirection = new(lHorizontal, lVertical);

        //Movement with collisions checks
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, new(lHorizontal, 0), speed * Time.deltaTime, currentMask))
            transform.position += new Vector3(lHorizontal, 0) * (speed * Time.deltaTime);
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, new(0, lVertical), speed * Time.deltaTime, currentMask))
            transform.position += new Vector3(0, lVertical) * (speed * Time.deltaTime);

        if (inputs.asset[JumpKey].WasPressedThisFrame())
            SetModeJump();
        if (inputs.asset[InteractKey].WasPerformedThisFrame())
            interactable?.Interact();
        if (inputs.asset[GuideKey].WasPerformedThisFrame())
            SetModeGuide();
    }

    private void SetModeJump() {
        sprite.color = Color.red;
        animator?.SetTrigger(JumpTriggerId);

        currentMask = jumpMask;
        elapsedTimeInJump = 0f;
        
        jumpDirection = inputs.asset[MoveKey].ReadValue<Vector2>();
        startPos = transform.position;
        endPos = transform.position + new Vector3(jumpDirection.x,jumpDirection.y)* jumpDistance;

        currentState = DoActionJump;
    }

    private void DoActionJump()
    {
        if (!Physics2D.CircleCast(transform.position, colliderExtentSize, jumpDirection, speed * Time.deltaTime, groundedMask)) {
            transform.position += new Vector3(jumpDirection.x, jumpDirection.y) * jumpDistance * Time.deltaTime;
        }

        float lRatio = elapsedTimeInJump / timeInAir;
        playerRenderer.transform.localPosition = new Vector3(0, 0.5f +(jumpHeigth * jumpCurve.Evaluate(lRatio)),0);

        elapsedTimeInJump += Time.deltaTime;
        if (elapsedTimeInJump > timeInAir)
        {
            if (Physics2D.OverlapCircle(transform.position,colliderExtentSize,onlyJumpLayer))
            {
                transform.position = startPos;
            }
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
        // TODO animation from bottom
        guide.ToggleGuideDisplay();
        
        currentState = DoActionGuide;
    }

    public void SetModeDummy()
    {
        currentState = () => { };
    }

    private void DoActionGuide()
    {
        if (!inputs.asset[GuideKey].WasPerformedThisFrame())
            return;
        
        guide.ToggleGuideDisplay();
        SetModeMove();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene($"Level{char.GetNumericValue(SceneManager.GetActiveScene().name[^1]) + 1}");
    }
}
