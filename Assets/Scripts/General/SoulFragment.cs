using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFragment : MonoBehaviour, ICollectable
{
    public delegate void Colecction();
    public static event Colecction collected;

    Rigidbody2D rbFragSoul;
    Collider2D sfColl;
    int speedSoulFrag = 5;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] float desfase;
    [SerializeField] Vector3 direction;
    Vector3 startPos;
    void Start()
    {
        rbFragSoul = GetComponent<Rigidbody2D>();
        sfColl = GetComponent<Collider2D>();
        rbFragSoul.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f)) * speedSoulFrag, ForceMode2D.Impulse);//se les aplica un impulso al spawnear
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) //cuando colisione con el suelo
        {
            StartCoroutine(Moving()); //se activará esta corrutina
        }
        else if (collision.gameObject.layer == 6)
        {
            sfColl.isTrigger = true;
        }
    }

    void ICollectable.Collected()
    {
        collected?.Invoke();//se activará el evento de recollección
        Destroy(gameObject);
    }

    IEnumerator Moving()
    {
        startPos = transform.position + new Vector3(0, 0.3f, 0); //lo primero es declarar el inicio +0.3 en y
        rbFragSoul.isKinematic = true; //lo volvemos cinemático al dejar de utilizar fuerzas
        sfColl.isTrigger = true; //lo volvemos trigger oara poder ser recolectado
        while (true)//creamos un "falso" update
        {
            float seno = range * Mathf.Sin(speed * Time.time + desfase);
            transform.position = startPos + direction.normalized * seno;
            yield return null;
        }
    }
}
