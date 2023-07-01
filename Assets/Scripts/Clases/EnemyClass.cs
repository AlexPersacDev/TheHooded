using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyClass : ScriptableObject
{
    public float speed;
    public int enemyDamage;
    public int enemyHP;
    [SerializeField] GameObject[] soulFragments;
    //Clase para todos los enemigos
    public void Attack()//3 de 4 enemigos atacarán
    {
        
    }

    public void PlayerDetection()//3 de 4 enemigos deberán detectar al jugador
    {

    }

    public void Died(Animator enemyAnim)//Metodo que activa la animación de muerte
    {
        if (enemyHP < 0)
        {
            enemyAnim.SetTrigger("Dead");
        }
    }

    public void Drop(GameObject enemy, int ramd1, int ramd2) //al morir el enemigo dropeará fragmentos de alma
    {
        int soulFragmentsCant = Random.Range(ramd1, ramd2);//se ramdomiza la cantidad de fragmentos que soltará cada enbemigo dependiendo de su rango
        for (int i = 0; i < soulFragmentsCant; i++)
        {
            int frag = Random.Range(0, 4);//se ramdomiza el tipo de fragmento a dropar
            GameObject soulFragmentCopy = Instantiate(soulFragments[frag], enemy.transform.position, Quaternion.identity);//se instancian en la posición del enemigo
        }
    }
}
