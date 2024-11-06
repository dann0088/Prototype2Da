using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Transform background;
    [SerializeField] private string deadGameOver;
    [SerializeField] private string eggRetrieveFailed;

    private void Awake() {
        restartButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.GameScene);
        });
    }

    private void Start() {
        ExitRoute.Instance.OnFailureExit += ExitRoute_OnFailureExit;
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        Hide();
    }

    private void ExitRoute_OnFailureExit(object sender, System.EventArgs e) {
        Time.timeScale = 0f;
        gameOverText.text = eggRetrieveFailed;
        Show();
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e) {
        //Time.timeScale = 0f;
        background.gameObject.SetActive(false);
        gameOverText.text = deadGameOver;
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }


    private void Hide() {
        gameObject.SetActive(false);
    }
}
