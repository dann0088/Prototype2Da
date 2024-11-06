using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMode : MonoBehaviour
{
    //public event EventHandler<OnEnemyDamageOvertimeEventArgs> OnSippingPlayerHealth;
    //public class OnEnemyDamageOvertimeEventArgs : EventArgs {
    //    public float enemyDamage;
    //}

    [SerializeField] private Sprite enrageVisual;
    [SerializeField] private Sprite normalVisual;
    [SerializeField] private Enemy enemy;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private bool showGizmos = true;
    //[SerializeField] private Player player;
    private const string IS_ENRAGE = "IsEnrage";
    private const string IS_IDLE = "IsIdle";
    private const string NORMAL = "Normal";
    private const string ENRAGE = "Enrage";
    private const string DEATH = "Death";
    private float enrageRadius = 0.9f;
    private float normalRadius = 0.37f;
    private float currentEnemyRadius = 0f;
    private float enemyDamageOvertime;
    private float enemyDamageOvertimeMax = 2f;
    private float currentEnemyDamage = 2f;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();

    }

    private void Start() {
        currentEnemyRadius = normalRadius;
        //enemy.OnNormalState += Enemy_OnNormalState;
        //enemy.OnDeadState += Enemy_OnDeadState;
        //enemy.OnHit += Enemy_OnHit;
    }
    
    //private void Enemy_OnHit(object sender, EventArgs e) {
    //    StartCoroutine(FlashHit());
    //}

    public void EnemyOnHit() {
        StartCoroutine(FlashHit());
    }

    private IEnumerator FlashHit() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void EnemyDeadState() {
        animator.SetTrigger(DEATH);
    }

    //private void Enemy_OnDeadState(object sender, EventArgs e) {
    //    animator.SetTrigger(DEATH);
    //}

    private void Update() {
        var playerDamageCollide = Physics2D.OverlapCircle(transform.position, currentEnemyRadius, targetLayer);
        if (playerDamageCollide != null) {
            enemyDamageOvertime -= Time.deltaTime;
            if (enemyDamageOvertime <= 0f) {
                enemyDamageOvertime = enemyDamageOvertimeMax;
                Player.Instance.TakeDamage(currentEnemyDamage);
            }
        }
    }

    public void EnemyNormalState() {
        animator.SetTrigger(NORMAL);
        float normalStateDamage = 2f;
        float normaStateDamageOvertime = 2f;
        currentEnemyDamage = normalStateDamage;
        enemyDamageOvertimeMax = normaStateDamageOvertime;
    }

    //private void Enemy_OnNormalState(object sender, System.EventArgs e) {
    //    animator.SetTrigger(NORMAL);
    //    float normalStateDamage = 2f;
    //    float normaStateDamageOvertime = 2f;
    //    currentEnemyDamage = normalStateDamage;
    //    enemyDamageOvertimeMax = normaStateDamageOvertime;
    //}

    public void EnemyEnrageState() {
        animator.SetTrigger(ENRAGE);
        float enragedStateDamage = 4f;
        float enrageStateDamageOvertime = 1f;
        currentEnemyDamage = enragedStateDamage;
        enemyDamageOvertimeMax = enrageStateDamageOvertime;
    }
    //private void Enemy_OnEnragedState(object sender, System.EventArgs e) {
    //    animator.SetTrigger(ENRAGE);
    //    float enragedStateDamage = 4f;
    //    float enrageStateDamageOvertime = 1f;
    //    currentEnemyDamage = enragedStateDamage;
    //    enemyDamageOvertimeMax = enrageStateDamageOvertime;
    //}

    private void EnrageModeVisual() {
        currentEnemyRadius = enrageRadius;
        gameObject.GetComponent<SpriteRenderer>().sprite = enrageVisual;
        gameObject.GetComponent<CircleCollider2D>().radius = enrageRadius;
    }

    private void NormalModeVisual() {
        currentEnemyRadius = normalRadius;
        gameObject.GetComponent<SpriteRenderer>().sprite = normalVisual;
        gameObject.GetComponent<CircleCollider2D>().radius = normalRadius;
        animator.SetBool(IS_ENRAGE, false);
        animator.SetBool(IS_IDLE, true);
    }

    private void RemoveEnemy() {
        enemy.DestroySelf();
    }

    private void OnDrawGizmos() {
        if (showGizmos) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, currentEnemyRadius);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawSphere(transform.position, patrolRange);
        }
    }
}
