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
    CapsuleCollider2D collPlayer;



    [Header("Properties")]
    [SerializeField] float speed, force, dashSpeed;
    int playerHP;
    int playerSouls;
    bool canMove = true;

    float radOverlap = 0.2f;
    [Header("OverlapGround")]
    [SerializeField] Transform feets;
    [SerializeField] LayerMask ground;

    [Header("OverlapAttack")]
    [SerializeField] Transform hand;
    [SerializeField] LayerMask enemy;

    bool dash = false;
    bool dashAttack = false;
    bool climb = false;
    bool wallJump = false;
    bool distanceAttack = false;
    bool meleAtack2 = false;
    bool shield = false;
    bool dobleJump = false;


    bool dashing = false;

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
        collPlayer = GetComponent<CapsuleCollider2D>();
        spawn = transform.position + Vector3.up;
        playerHP = GameManager.gM.PlayerLifes();
        playerSouls = GameManager.gM.PlayerSouls();

        dash = GameManager.gM.Dash();
        meleAtack2 = GameManager.gM.Range();
        distanceAttack = GameManager.gM.DistanceAttack();
        shield = GameManager.gM.Shield();
    }


    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        PlayerOnGround();//Detecto si estoy en el suelo
        PlayerAttack();
        StartCoroutine(Dash());
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
        else if (trigger.TryGetComponent<IInteractuable>(out IInteractuable interactuable))//si el objeto con el que se colisiona tiene dicha interfaz
        {
            interactuable.Interaction();//se llama al método
            Destroy(trigger);
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
        if (rbPlayer.velocity.y <= 0 && !dashing)//si mi velocidad en y es menor a 0
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
        playerHP = GameManager.gM.PlayerLifes();
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

    //void Dash()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        anim.SetTrigger("Dash");
    //        rbPlayer.velocity = Vector2.zero;
    //        rbPlayer.AddForce(new Vector3(transform.localScale.x, 0, 0) * dashSpeed, ForceMode2D.Impulse);
    //        rbPlayer.isKinematic = true;
    //        rbPlayer.isKinematic = false;

    //    }
    //}

    IEnumerator Dash()
    {
        if (dash && Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Falling", false);
            dashing = true;
            anim.SetTrigger("Dash");
            rbPlayer.AddForce(new Vector3(transform.localScale.x, 0, 0) * dashSpeed, ForceMode2D.Impulse);
            collPlayer.direction = CapsuleDirection2D.Horizontal;
            collPlayer.size = new Vector2(4.1f, 0.85f);
            while (dashing)
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
                yield return null;
            }
            dashing = false;
            StopCoroutine(Dash());
        }
    }
    void StopDashing()
    {
        collPlayer.direction = CapsuleDirection2D.Vertical;
        collPlayer.size = new Vector2(1, 1.3f);
        dashing = false;
    }

    void Dying()
    {
        anim.SetTrigger("Died");
        die?.Invoke();
        playerHP = GameManager.gM.PlayerLifes();
        playerSouls = GameManager.gM.PlayerSouls();
    }

    public void Respawn()
    {
        transform.position = spawn; //vuelvo al punto inicial
        rbPlayer.velocity = Vector3.zero;
        rbPlayer.isKinematic = false;
        dashing = false;
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
