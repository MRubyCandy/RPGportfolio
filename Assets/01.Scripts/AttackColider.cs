using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColider : MonoBehaviour
{
    Collider AtkColider;
    float Damage;

    void Start()
    {
        AtkColider = GetComponent<Collider>();
    }

    private void Update()
    {
        Damage = GameManager.gameManager.GameData.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        var Enemy = other.GetComponent<EnemyHealth>();
        if(Enemy != null)
        {
            Enemy.OnDamage(Damage);
        }
        var bossEnemy = other.GetComponent<EnemyHealth_objpooling>();
        if(bossEnemy != null)
        {
            bossEnemy.OnDamage(Damage);
        }
    }
}
