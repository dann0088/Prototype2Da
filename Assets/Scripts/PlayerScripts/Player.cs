using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler OnAttackPressed;
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerHit;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float playerHealthMax = 100f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask layerMask;

    private Vector3 lastMovementDir;
    private Vector3 lastAttackPosition;
    private bool isWalking;
    private bool isIdle;
    private bool isAttacking;
    private bool deadStatus;
    private float horizontalMovement;
    private float verticalMovement;
    private float playerLooking = 1f;
    private float currentPlayerHealth;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        deadStatus = false;
        currentPlayerHealth = playerHealthMax;
    }
    

    private void Update() {
        HandleMovement();
        HandleAttacking();
    }

    private void HandleAttacking() {
        isAttacking = gameInput.GetAttackTrigger();
        if (isAttacking) {
            OnAttackPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleMovement() {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f);
        Vector2 playerSize = new Vector2(0.50f, 0.50f);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerAngle = 0.5f;
        bool canMove = !Physics2D.BoxCast(transform.position, playerSize, playerAngle, moveDir, moveDistance, layerMask);
        if (canMove && deadStatus == false) {
            transform.position += moveDir * moveDistance;
        }
        
        isWalking = moveDir != Vector3.zero;
        isIdle = moveDir == Vector3.zero;
        if (!isIdle) {
            horizontalMovement = inputVector.x;
            verticalMovement = inputVector.y;
        }

        if (inputVector.x == 1 || inputVector.x == -1) {
            playerLooking = inputVector.x;
        }


        if (moveDir != Vector3.zero) {
            lastMovementDir = moveDir;
            HandleAttackPoint(inputVector);
        }

        transform.localScale = new Vector2(playerLooking, 1f);
        //attackPoint.rotation = Quaternion.LookRotation(Vector3.forward, moveDir);
        
        //float rotateSpeed = 20f;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void HandleAttackPoint(Vector3 inputVector) {
        float attackPositionX = transform.position.x;
        float attackPositionY = transform.position.y;
        if (inputVector.x == 1) {
            attackPositionX = transform.position.x + 1.3f;
            attackPositionY = transform.position.y + 0.3f;
        } else if (inputVector.x == -1) {
            attackPositionX = transform.position.x - 1.3f;
            attackPositionY = transform.position.y + 0.3f;
        }

        if (inputVector.y == 1) {
            attackPositionY = transform.position.y + 1.7f;
        } else if (inputVector.y == -1) {
            attackPositionY = transform.position.y - 1f;
        }

        Vector3 attackPosition = new Vector3(attackPositionX, attackPositionY, 0f);
        attackPoint.SetPositionAndRotation(attackPosition, Quaternion.LookRotation(transform.forward, lastMovementDir));
    }

    public void TakeDamage(float enemyDamage) {
        currentPlayerHealth -= enemyDamage;
        if (currentPlayerHealth <= 0f && !deadStatus) {
            deadStatus = true;
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            //DestroySelf();
        }
        OnPlayerHit?.Invoke(this, EventArgs.Empty);
    }


    public float GetPlayerHealth() {
        return 1 - (currentPlayerHealth / playerHealthMax);
    }

    public bool IsWalking() {
        return isWalking;
    }

    public bool IsIdle() {
        return isIdle;
    }

    public bool IsAttacking() {
        return isAttacking;
    }

    public float IsHorizontalMovement() {
        return horizontalMovement;
    }

    public float IsVerticalMovement() {
        return verticalMovement;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
    

}
