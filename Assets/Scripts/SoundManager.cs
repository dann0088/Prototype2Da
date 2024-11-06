using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Player.Instance.OnAttackPressed += Player_OnAttackPressed;
        Player.Instance.OnPlayerHit += Player_OnPlayerHit;
        Enemy.OnAnyHit += Enemy_OnAnyHit;
        Enemy.OnDead += Enemy_OnDead;
        Enemy.OnEnragedState += Enemy_OnEnragedState;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Enemy_OnEnragedState(object sender, Enemy.OnGetEnemyPosition e) {
        PlaySound(audioClipRefSO.enemyEnrageState, e.transform.position);
    }

    private void Enemy_OnDead(object sender, Enemy.OnGetEnemyPosition e) {
        PlaySound(audioClipRefSO.enemyDied, e.transform.position);
    }

    private void Player_OnPlayerHit(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.takingDamage, Player.Instance.transform.position);
    }

    private void Enemy_OnAnyHit(object sender, Enemy.OnGetEnemyPosition e) {
        PlaySound(audioClipRefSO.hitEnemy, e.transform.position);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.getInTheBag, Player.Instance.transform.position);
    }

    private void Player_OnAttackPressed(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.attack, Player.Instance.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume) {
        if (FloorType.Instance.MetalFloor()) {
            PlaySound(audioClipRefSO.footstepMetal, position, 1f);
        }

        if (FloorType.Instance.GlassFloor()) {
            PlaySound(audioClipRefSO.footstepGlass, position, volume);
        }

        if (FloorType.Instance.DefaultFloor()) {
            PlaySound(audioClipRefSO.footstepDefault, position, volume);
        }
    }
}
