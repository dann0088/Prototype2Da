using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Transform meleeAttackPoint;
    //public event EventHandler OnAttackEnemy;
    //public class OnAttackEnemyEventArgs: EventArgs {
    //    public int damaged;
    //    public Collider2D collision;
    //}

    private bool isAttacking = false;
    private float attackDuration = 0.3f;
    private float attackTimer = 0f;

    private void Start() {
        meleeAttackPoint.gameObject.SetActive(false);
        //Player player = GetComponent<Player>();
        Player.Instance.OnAttackPressed += Player_OnAttackPressed;
    }

    private void Player_OnAttackPressed(object sender, System.EventArgs e) {
        if (!isAttacking) {
            isAttacking = true;
            meleeAttackPoint.gameObject.SetActive(true);
        }
    }

    private void Update() {
        CheckMeleeTimer();
        
    }

    private void CheckMeleeTimer() {
        if (isAttacking) {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration) {
                meleeAttackPoint.gameObject.SetActive(false);
                attackTimer = 0f;
                isAttacking = false;
            }
        }
    }

    //private void OnDrawGizmosSelected() {
    //    Gizmos.DrawSphere(meleeAttackPoint.position, 1.5f);
    //}
}
