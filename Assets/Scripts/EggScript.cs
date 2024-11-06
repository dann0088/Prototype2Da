using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{

    public static EggScript Instance { get; private set; }
    public event EventHandler OnGettingEgg;

    [SerializeField] private Transform interactUI;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float radius = 1f;

    private bool isOverlappingEgg;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        isOverlappingEgg = false;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        HideInteractUI();
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (isOverlappingEgg) {
            GetEgg();
        }  
    }

    private void Update() {
        var playerDetect = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        if (playerDetect != null) {
            isOverlappingEgg = true;
            ShowInteractUI();
        } else {
            isOverlappingEgg = false;
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

    private void GetEgg() {
        OnGettingEgg?.Invoke(this, EventArgs.Empty);
        isOverlappingEgg = false;
        Destroy(gameObject);
    }
}
