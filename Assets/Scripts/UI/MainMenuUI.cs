using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button controlButton;
    [SerializeField] private Transform controlUIPage;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        controlButton.onClick.AddListener(() => {
            controlUIPage.gameObject.SetActive(true);
        });

        Time.timeScale = 1f;
    }

    private void Start() {
        controlUIPage.gameObject.SetActive(false);
    }
}
