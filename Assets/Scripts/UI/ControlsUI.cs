using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] private Button backToMainMenu;

    private void Awake() {
        backToMainMenu.onClick.AddListener(() => {
            HideControlUI();
        });
    }

    private void HideControlUI() {
        gameObject.SetActive(false);
    }
}
