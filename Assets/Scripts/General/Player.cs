using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    Rigidbody2D rbPlayer;
    Animator anim;
    float h, v;
    Vector3 spawn;



    [Header("Properties")]
    [SerializeField] float speed, force;
    int playerHP = 5;
    int playerSouls = 3;
    bool canMove = true;

    float radOverlap = 0.2f;
    [Header("OverlapGround")]
    [SerializeField] Transform feets;
    [SerializeField] LayerMask ground;

    [Header("OverlapAttack")]
    [SerializeField] Transform hand;
    [SerializeField] LayerMask enemy;

    public delegate void LooseLife();
    public static event LooseLife looseLife;
    public delegate void Died();
    public static event Died die;
    public delegate void GameOver();
    public static event GameOver gameOver;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawn = transform.position + Vector3.up;
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



    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.TryGetComponent<ICollectable>(out ICollectable collectable))//si el objeto con el que se colisiona tiene dicha interfaz
        {
            collectable.Collected();//se llama al método
        }
    }
    void PlayerOnGround()
    {
        //bool onGround = Physics.CheckSphere(feets.position, radFeets, ground);
        if (Physics2D.OverlapCircle(feets.position, radOverlap, ground)) // si estoy tocando el suelo
        {
            PlayerJumping();
            anim.SetBool("Falling", false);//dejaré de caer
        }
        else// si estoy en el aire
            PlayerFalling(); //llamo al metodo que detecta si estoy cayendo
    }

    void PlayerOverlapAttack()
    {
        Collider2D enemyColl = Physics2D.OverlapCircle(hand.position, radOverlap, enemy);
        if (enemyColl && enemyColl.gameObject.TryGetComponent<IDamageable>(out IDamageable enemyDamageable))
        {
            enemyDamageable.Damaged();
        }
    }

    void PlayerMovement() //método de movimiento
    {
        if (canMove)
        {
            rbPlayer.AddForce(new Vector3(h, 0, 0) * speed, ForceMode2D.Force); //aplico una fuerza en dirección h(-1 o 1)
            if (h != 0)//si h no es igual a cero
            {
                anim.SetBool("Running", true);//estaré en movimiento por lo que correré
                Flip();//giraré segun el valor de h
            }
            else//si no estoy en movimiento 
                anim.SetBool("Running", false);//dejaré de correr
        }
    }
    void Flip()//método de giro
    {
        transform.localScale = new Vector3(h, transform.localScale.y, transform.localScale.z);//mi escala en x será igual al valor de h
    }

    void PlayerJumping()//método de salto
    {
        if (Input.GetKeyDown(KeyCode.Space))//si pulso espacio
        {
            rbPlayer.AddForce(Vector3.up * force, ForceMode2D.Impulse);//aplico una ferza hacia arriba
            anim.SetBool("Running", false); //el salto tiene prioridas sobre correr
            anim.SetTrigger("Jump");//Activaré animación de saltar
        }
    }

    void PlayerFalling()//método de caida
    {
        if (rbPlayer.velocity.y <= 0)//si mi velocidad en y es menor a 0
        {
            anim.SetBool("Falling", true); //estaré cayendo
        }
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Attack");
        }
    }

    void LooseLifes()
    {
        looseLife?.Invoke();
        playerHP--;
    }

    void IDamageable.Damaged()
    {
        rbPlayer.velocity = Vector2.zero;
        LooseLifes();
        if (playerHP > 0)
        {
            rbPlayer.AddForce(new Vector3(-transform.localScale.x, 0, 0) * 15, ForceMode2D.Impulse);
            anim.SetTrigger("Damaged");
        }
        else
        {
            Dying();
            rbPlayer.AddForce(new Vector3(-transform.localScale.x, 1, 0).normalized * 50, ForceMode2D.Impulse);
        }

    }

    void Dying()
    {
        anim.SetTrigger("Died");
        die?.Invoke();
        playerSouls--;
        playerHP = 5;
    }

    public void Respawn()
    {
        transform.position = spawn; //vuelvo al punto inicial
        rbPlayer.velocity = Vector3.zero;
        rbPlayer.isKinematic = false;
        if (playerSouls == 0)
        {
            gameOver?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hand.position, radOverlap);
    }

}
