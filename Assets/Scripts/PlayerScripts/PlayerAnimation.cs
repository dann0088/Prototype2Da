using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_IDLE = "IsIdle";
    private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
    private const string VERTICAL_MOVEMENT = "VerticalMovement";
    private const string ATTACK = "Attack";
    private const string DEATH = "Death";

    [SerializeField] private Player player;
    private Animator animator;

    private bool isPlayerDead;

    private void Awake() {
        isPlayerDead = false;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        player.OnAttackPressed += Player_OnAttackPressed;
        player.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, EventArgs e) {
        animator.SetTrigger(DEATH);
        isPlayerDead = true;
    }

    private void Player_OnAttackPressed(object sender, EventArgs e) {
        if (!isPlayerDead) {
            animator.SetTrigger(ATTACK);
        }
        
    }

    private void Update() {
        if (!isPlayerDead) {
            animator.SetBool(IS_WALKING, player.IsWalking());
            animator.SetBool(IS_IDLE, player.IsIdle());
            animator.SetFloat(HORIZONTAL_MOVEMENT, player.IsHorizontalMovement());
            animator.SetFloat(VERTICAL_MOVEMENT, player.IsVerticalMovement());
        } 
    }


    private void DestroyPlayerGameObject() {
        player.DestroySelf();
    }
}
