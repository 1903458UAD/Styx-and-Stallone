using JetBrains.Annotations;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerActions : MonoBehaviour
{
    [Header("Player Attributes")]
    public float playerSpeed = 8;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float rotationSmoothTime = 0.12f;

    [Tooltip("Speed of character rotation")]
    [Range(5.0f, 10.0f)]
    public float rotationSpeed = 10.0f;

    [Tooltip("Acceleration")]
    public float accelerationRate = 10.0f;

    [Tooltip("Deceleration")]
    public float decelerationRate = 12.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float jumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float jumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float fallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool grounded = true;

    [Tooltip("Useful for rough ground")]
    public float groundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float groundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask groundLayers;

    // player
    private float speed;
    private float animationBlend;
    private float targetRotation = 0.0f;
    private float rotationVelocity;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;

    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    private Animator _animator;

    private bool _hasAnimator;

    [SerializeField] private int playerIndex = 0;

    private CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;
    private bool inputJump = false;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        _hasAnimator = TryGetComponent(out _animator);

        AssignAnimationIDs();

        jumpTimeoutDelta = jumpTimeout;
        fallTimeoutDelta = fallTimeout;
    }

    void OnDrawGizmosSelected()
    {
        // Set the Gizmo color based on grounded state
        Gizmos.color = grounded ? Color.green : Color.red;

        // Calculate sphere position (same as your grounded check)
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);

        // Draw a wireframe sphere at the grounded check position with groundedRadius
        Gizmos.DrawWireSphere(spherePosition, groundedRadius);
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);

        GroundedCheck();
        JumpAndGravity();
        Move();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void GroundedCheck()
    { 
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

        Debug.Log($"Grounded: {grounded} ");

        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, grounded);
        }
    }

    private void JumpAndGravity()
    {
        if (grounded)
        {
            fallTimeoutDelta = fallTimeout;

            if (_hasAnimator)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }

            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }

            if (inputJump && jumpTimeoutDelta <= 0.0f)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, true);
                }

                inputJump = false;
            }

            if (jumpTimeoutDelta >= 0.0f)
            {
                jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            jumpTimeoutDelta = jumpTimeout;

            if (fallTimeoutDelta >= 0.0f)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }
            }

            inputJump = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        if (!grounded)
        {
            if (verticalVelocity < -terminalVelocity)
            {
                verticalVelocity = -terminalVelocity;
            }
        }
    }


    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    public void SetInputJumpBool(bool jump)
    {
        inputJump = jump;
    }

    private void Move()
    {
        if (inputVector != Vector2.zero)
        {
            moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

            speed = Mathf.MoveTowards(speed, playerSpeed, accelerationRate * Time.deltaTime);
            animationBlend = Mathf.Lerp(animationBlend, speed, Time.deltaTime * accelerationRate);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        else
        {
            speed = Mathf.MoveTowards(speed, 0, decelerationRate * Time.deltaTime);
            animationBlend = Mathf.Lerp(animationBlend, speed, Time.deltaTime * decelerationRate);
        }

        if (animationBlend < 0.01f) animationBlend = 0f;

        Vector3 velocity = moveDirection * speed;
        Vector3 totalMove = velocity + new Vector3(0, verticalVelocity, 0);

        characterController.Move(totalMove * Time.deltaTime);


        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, moveDirection.magnitude);
        }

    }
}

