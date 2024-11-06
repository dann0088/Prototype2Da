using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnGameOver;
    private enum State {
        GamePlaying,
        GameOver,
    }

    private State state;
    private bool isGamePaused = false;

    private void Awake() {
        Instance = this;
        state = State.GamePlaying;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        ExitRoute.Instance.OnFailureExit += ExitRoute_OnFailureExit;
        ExitRoute.Instance.OnSuccessExit += ExitRoute_OnSuccessExit;
    }

    private void ExitRoute_OnSuccessExit(object sender, EventArgs e) {
        SwitchToMainMenu();
    }

    private void ExitRoute_OnFailureExit(object sender, EventArgs e) {
        ShowGameOver();
    }

    private void Player_OnPlayerDeath(object sender, EventArgs e) {
        ShowGameOver();
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch(state) {
            case State.GamePlaying:
                break;
            case State.GameOver:
                break;
        }
    }

    // Future update for game is currently playing
    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }
    
    // Future update for game is gameover
    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ShowGameOver() {
        state = State.GameOver;
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    private void SwitchToMainMenu() {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
