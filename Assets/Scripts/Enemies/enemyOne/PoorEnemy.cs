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
    Animator anim;

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
        anim = GetComponent<Animator>();

        StartCoroutine(PoorEnemyMovement());
    }


    void Update()
    {
        PoorEnemyFlip();
    }

    void IDamageable.Damaged()//metodo con el que recibe da�o
    {
        poorEnemy.enemyHP--;
        anim.SetTrigger("Damaged");
        poorEnemy.Died(anim);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        //si se entra en contacto con un objeto da�able y con la layermask de Player
        if (trigger.gameObject.TryGetComponent<IDamageable>(out IDamageable player) && trigger.gameObject.layer == 6)
        {
            player.Damaged();//se llamara al metodo Damaged de dicho objeto
        }
    }

    IEnumerator PoorEnemyMovement()
    {
        while (true)
        {
            newPos = transform.position;
            int newX =  Random.Range(-3, 3);
            destiny = new Vector3(newX, 0, 0) + startPos;
            
            while (transform.position != destiny)//mientras su posici�n sea diferente al destino se mueve
            {
                anim.SetBool("Walking", true);
                transform.position = Vector3.MoveTowards(transform.position, destiny, poorEnemy.speed * Time.deltaTime);
                yield return null; //vuelve al siguiente frame
            }
            anim.SetBool("Walking", false);
            yield return new WaitForSeconds(2f);
        }
    }

    void PoorEnemyFlip()
    {
        if (transform.position.x > newPos.x)
        {
            transform.localScale = Vector3.one;
        }
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Droping()
    {
        poorEnemy.Drop(gameObject, 1, 4);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
