using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    Rigidbody2D rbPlayer;
    Animator anim;
    float h, v;
    Vector3 spawn;

    [Header("Properties")]
    [SerializeField] float speed, force;
    int playerDamage = 23;
    int playerHP;

    float radOverlap = 0.2f;
    [Header("OverlapGround")]
    [SerializeField] Transform feets;
    [SerializeField] LayerMask ground;

    [Header("OverlapAttack")]
    [SerializeField] Transform hand;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawn = transform.position;
    }


    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        PlayerOnGround();//Detecto si estoy en el suelo
        PlayerAttack();
    }

    private void FixedUpdate()
    {
        PlayerMovement();//llamo al metodo que detecta el movimiento
    }



    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Void"))//si entro en contacto con "Void"
        {
            transform.position = spawn; //vuelvo al punto inicial
        }
    }
    void PlayerOnGround()
    {
        //bool onGround = Physics.CheckSphere(feets.position, radFeets, ground);
        if (Physics2D.OverlapCircle(feets.position, radOverlap, ground)) // si estoy tocando el suelo
        {
            PlayerJumping();
            anim.SetBool("Falling", false);//dejar� de caer
        }
        else// si estoy en el aire
            PlayerFalling(); //llamo al metodo que detecta si estoy cayendo
    }

    void PlayerOverlapAttack()
    {
        Collider2D enemyColl = Physics2D.OverlapCircle(hand.position, radOverlap);
        if (enemyColl && enemyColl.gameObject.TryGetComponent<IDamageable>(out IDamageable enemyDamageable))
        {
            enemyDamageable.Damaged(playerDamage);
        }
    }

    void PlayerMovement() //m�todo de movimiento
    {
        rbPlayer.AddForce(new Vector3(h, 0, 0) * speed, ForceMode2D.Force); //aplico una fuerza en direcci�n h(-1 o 1)
        if (h != 0)//si h no es igual a cero
        {
            anim.SetBool("Running", true);//estar� en movimiento por lo que correr�
            Flip();//girar� segun el valor de h
        }
        else//si no estoy en movimiento 
            anim.SetBool("Running", false);//dejar� de correr

    }
    void Flip()//m�todo de giro
    {
        transform.localScale = new Vector3(h, transform.localScale.y, transform.localScale.z);//mi escala en x ser� igual al valor de h
    }

    void PlayerJumping()//m�todo de salto
    {
        if (Input.GetKeyDown(KeyCode.Space))//si pulso espacio
        {
            rbPlayer.AddForce(Vector3.up * force, ForceMode2D.Impulse);//aplico una ferza hacia arriba
            anim.SetBool("Running", false); //el salto tiene prioridas sobre correr
            anim.SetTrigger("Jump");//Activar� animaci�n de saltar
        }
    }

    void PlayerFalling()//m�todo de caida
    {
        if (rbPlayer.velocity.y <= 0)//si mi velocidad en y es menor a 0
        {
            anim.SetBool("Falling", true); //estar� cayendo
        }
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Attack");
        }
    }

    void IDamageable.Damaged(int damage)
    {
        playerHP -= damage;
        Debug.Log("auch");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hand.position, radOverlap);
    }
}
