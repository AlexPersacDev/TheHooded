using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyClass : ScriptableObject
{
    public float speed;
    public int enemyDamage;
    public int enemyHP;
    //Clase para todos los enemigos
    public void Attack()//3 de 4 enemigos atacarán
    {
        
    }

    public void PlayerDetection()//3 de 4 enemigos deberán detectar al jugador
    {

    }
}
