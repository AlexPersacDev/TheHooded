using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorEnemy : MonoBehaviour, IDamageable
{
    //EnemyClass poorEnemy = new EnemyClass();
    [SerializeField] EnemyClass poorEnemy;
    int damage = 1;
    int hP;
    float speed = 5;

    Vector3 startPos;
    Vector3 newPos;
    Vector3 destiny;

    void Start()
    {
        //se declaran los valores 
        poorEnemy.speed = speed;
        poorEnemy.enemyDamage = damage;
        poorEnemy.enemyHP = hP;
        startPos = transform.position;
        poorEnemy.anim = GetComponent<Animator>();

        StartCoroutine(PoorEnemyMovement());
    }


    void Update()
    {
        Flip();
    }

    void IDamageable.Damaged(int damage)//metodo con el que recibe daño
    {
        Debug.Log(damage);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //si se entra en contacto con un objeto dañable y con la layermask de Player
        if (trigger.gameObject.TryGetComponent<IDamageable>(out IDamageable player) && trigger.gameObject.layer == 6)
        {
            player.Damaged(damage);//se llamara al metodo Damaged de dicho objeto
        }
    }

    IEnumerator PoorEnemyMovement()
    {
        while (true)
        {
            newPos = transform.position;
            int newX =  Random.Range(-3, 3);
            destiny = new Vector3(newX, 0, 0) + startPos;
            
            while (transform.position != destiny)//mientras su posición sea diferente al destino se mueve
            {
                poorEnemy.anim.SetBool("Walking", true);
                transform.position = Vector3.MoveTowards(transform.position, destiny, poorEnemy.speed * Time.deltaTime);
                yield return null; //vuelve al siguiente frame
            }
            poorEnemy.anim.SetBool("Walking", false);
            yield return new WaitForSeconds(2f);
        }
    }

    void Flip()
    {
        if (transform.position.x > newPos.x)
        {
            transform.localScale = Vector3.one;
        }
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
