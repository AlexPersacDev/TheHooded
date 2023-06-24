using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorEnemy : MonoBehaviour, IDamagable
{
    EnemyClass poorEnemy = new EnemyClass();
    int damage = 1;
    int hP;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        poorEnemy.speed = speed;
        poorEnemy.enemyDamage = damage;
        poorEnemy.enemyHP = hP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IDamagable.Damaged(int damage)
    {
        
    }
}
