using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region Variables
    
    public StateMachine movementSM;
    public State crouching;
    public State standing;
    public State jumping;    
    
    public float NormalColliderHeight = 1.8f;
    public float CrouchColliderHeight = 1f;

    public float walkSpeed = 3;
    public float walkLimit = 0.3f;
    public float runSpeed = 6;
    public float runLimit = 0.6f;
    public float crouchSpeed;

    
    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

    public float jumpForce = 100f;
    
    public float CollisionOverlapRadius;
    public LayerMask whatIsGround;

    public Transform groundDetector;

    private readonly int moveParam = Animator.StringToHash("Speed");
    private readonly int crouchParam = Animator.StringToHash("Crouch");

    #endregion

    #region Properties

    public int CrouchParam => crouchParam;


    private float originalHeight;
    public float ColliderSize
    {
        get => GetComponent<CapsuleCollider>().height;
        set
        {
            var collider = GetComponent<CapsuleCollider>();
            var center = collider.center;
            center.y = (value - originalHeight) / 2;
            collider.center = center;
            collider.height = value;

        }
    }

    #endregion

    #region Reference

    public Animator anim;
    private Rigidbody rb;
    

    #endregion

    private float playerFacingDegree;
    


    #region Methods

    public void ResetMoveDirection()
    {
        playerFacingDegree = transform.eulerAngles.y;
    }

    public void Move(Vector2 moveVector)
    {
        anim.SetFloat("Speed", moveVector.magnitude);
        Vector2 inputDir = moveVector.normalized;
        

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg+playerFacingDegree;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        else
        {
            ResetMoveDirection();
        }

        if (moveVector.magnitude >= runLimit)
        {
            float targetSpeed = runSpeed * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            rb.velocity = new Vector3((transform.forward * runSpeed).x, rb.velocity.y, (transform.forward * runSpeed).z);

        }
        else if (moveVector.magnitude >= walkLimit)
        {
            float targetSpeed = walkSpeed * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
            rb.velocity = new Vector3((transform.forward * walkSpeed).x, rb.velocity.y, (transform.forward * walkSpeed).z);

        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    public void SetAnimationBool(int param, bool value)
    {
        anim.SetBool(param, value);
    }

    public void TriggerAnimation(int param)
    {
        anim.SetTrigger(param);
    }

    public void ResetMoveParams()
    {
        anim.SetFloat("Speed", 0f);
    }

    public void ApplyImpulse(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public bool CheckCollisionOverlap(Vector3 point)
    {
        return Physics.OverlapSphere(point, CollisionOverlapRadius, whatIsGround).Length > 0;
    }
    
    #endregion


    #region MonoBehaviour Callbacks

    private void Start()
    {
        originalHeight = GetComponent<CapsuleCollider>().height;
        ResetMoveDirection();
        rb = GetComponent<Rigidbody>();
        movementSM = new StateMachine();

        standing = new CharacterStandingState(movementSM, this);
        crouching = new CharacterCrouchingState(movementSM, this);
        jumping = new CharacterJumpingState(movementSM, this);

        movementSM.Initialize(standing);
    }

    private void Update()
    {
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    #endregion

    

    
}