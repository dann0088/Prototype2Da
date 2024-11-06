using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }
    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnDialogueExitAction;
    //public event EventHandler OnGameInputDestroy;
    private PlayerInputAction playerInputActions;
    
    private void Awake() {
        Instance = this;

        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Pause.performed += Pause_performed;
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.DialogueExit.performed += DialogueExit_performed;
    }

    private void OnDestroy() {
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.DialogueExit.performed -= DialogueExit_performed;
        playerInputActions.Dispose();
    }

    private void DialogueExit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnDialogueExitAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    //private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
    //    OnAttackAction?.Invoke(this, EventArgs.Empty);
    //}

    public bool GetAttackTrigger() {
        bool isAttacking = playerInputActions.Player.Attack.triggered;
        return isAttacking;
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}
