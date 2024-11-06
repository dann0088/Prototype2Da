using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoute : MonoBehaviour
{
    public static ExitRoute Instance { get; private set; }
    public event EventHandler OnSuccessExit;
    public event EventHandler OnFailureExit;

    [SerializeField] private Transform interactUI;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float radius = 1.2f;

    private bool isOverlappingExit;
    private bool isGotTheEgg = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        isGotTheEgg = false;
        isOverlappingExit = false;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        EggScript.Instance.OnGettingEgg += EggScript_OnGettingEgg;
        HideInteractUI();
    }

    private void EggScript_OnGettingEgg(object sender, System.EventArgs e) {
        isGotTheEgg = true;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (isOverlappingExit) {
            Exit();
        }
    }

    private void Update() {
        var playerDetect = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        
        if (playerDetect != null) {
            isOverlappingExit = true;
            ShowInteractUI();
        } else {
            isOverlappingExit = false;
            HideInteractUI();
        }
    }

    private void ShowInteractUI() {
        interactUI.gameObject.SetActive(true);
    }

    private void HideInteractUI() {
        interactUI.gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Exit() {
        if (isGotTheEgg) {
            OnSuccessExit?.Invoke(this, EventArgs.Empty);
        } else {
            OnFailureExit?.Invoke(this, EventArgs.Empty);
        }
        //Loader.Load(Loader.Scene.MainMenuScene);
    }
}
