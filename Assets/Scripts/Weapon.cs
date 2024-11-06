using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float damage = 25f;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
        }
    }
}
