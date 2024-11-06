using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private EnemyMode enemyMode;

    public static event EventHandler<OnGetEnemyPosition> OnAnyHit;
    public static event EventHandler<OnGetEnemyPosition> OnDead;
    public static event EventHandler<OnGetEnemyPosition> OnEnragedState;
    public static void ResetStaticData() {
        OnAnyHit = null;
        OnDead = null;
        OnEnragedState = null;
    }
    public class OnGetEnemyPosition : EventArgs {
        public Transform transform;
    }

    
    //public event EventHandler OnNormalState;
    //public event EventHandler OnDeadState;
    //public event EventHandler OnHit;
    private Animator animator;
    private EnemyAI enemyAI;

    private float health;
    private float returnToNormalStateTimer = 10f;

    private bool isEnrageState;
    private bool isEnemyDead;

    public const string IS_ENRAGE = "IsEnrage";
    public const string IS_IDLE = "IsIdle";

    private void Awake() {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }
    private void Start() {
        isEnrageState = false;
        isEnemyDead = false;
        health = maxHealth;
    }

    private void Update() {
        if (isEnrageState) {
            returnToNormalStateTimer -= Time.deltaTime;
            if (returnToNormalStateTimer <= 0f) {
                isEnrageState = false;
                enemyMode.EnemyNormalState();
                //OnNormalState?.Invoke(this, EventArgs.Empty);
            }
        }
        
    }

    public void TakeDamage(float damage) {
        if (!isEnemyDead) {
            if (!isEnrageState) {
                isEnrageState = true;
                enemyMode.EnemyEnrageState();
                OnEnragedState?.Invoke(this, new OnGetEnemyPosition {
                    transform = transform,
                });
            }
            returnToNormalStateTimer = 10f;
            health -= damage;
            if (health <= 0) {
                isEnemyDead = true;
                enemyMode.EnemyDeadState();
                // TODO combine this two event handler
                // For sound
                OnDead?.Invoke(this, new OnGetEnemyPosition {
                    transform = transform,
                });
                //OnDeadState?.Invoke(this, EventArgs.Empty);
            }
            //OnHit?.Invoke(this, EventArgs.Empty);
            enemyMode.EnemyOnHit();
            OnAnyHit?.Invoke(this, new OnGetEnemyPosition {
                transform = transform,
            });
        }
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public bool GetEnemyDeadStatus() {
        return isEnemyDead;
    }


}
