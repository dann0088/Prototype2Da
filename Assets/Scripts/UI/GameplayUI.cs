using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Player player;
    [SerializeField] private Transform dialogueBox;
    [SerializeField] private Image egg;

    private void Start() {
        ShowGameplayUI();
        ShowDialogueBox();
        HideEgg();
        GameInput.Instance.OnDialogueExitAction += GameInput_OnDialogueExitAction;
        EggScript.Instance.OnGettingEgg += EggScript_OnGettingEgg;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }


    private void GameManager_OnGameOver(object sender, System.EventArgs e) {
        HideGameplayUI();
    }

    private void EggScript_OnGettingEgg(object sender, System.EventArgs e) {
        ShowEgg();
    }

    private void GameInput_OnDialogueExitAction(object sender, System.EventArgs e) {
        HideDialogueBox();
    }

    private void Update() {
        healthBarImage.fillAmount = player.GetPlayerHealth();
    }

    private void ShowDialogueBox() {
        dialogueBox.gameObject.SetActive(true);
    }

    private void HideDialogueBox() {
        dialogueBox.gameObject.SetActive(false);
    }

    private void ShowEgg() {
        egg.gameObject.SetActive(true);
    }

    private void HideEgg() {
        egg.gameObject.SetActive(false);
    }

    private void ShowGameplayUI() {
        gameObject.SetActive(true);
    }

    private void HideGameplayUI() {
        gameObject.SetActive(false);
    }

}
