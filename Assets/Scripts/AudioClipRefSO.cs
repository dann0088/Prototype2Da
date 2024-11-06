using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip attack;
    public AudioClip footstepDefault;
    public AudioClip footstepGlass;
    public AudioClip footstepMetal;
    public AudioClip hitEnemy;
    public AudioClip takingDamage;
    public AudioClip enemyDied;
    //public AudioClip[] enemyEnrageState;
    public AudioClip enemyEnrageState;
    public AudioClip enemyIdleState;
    public AudioClip getInTheBag;
}
