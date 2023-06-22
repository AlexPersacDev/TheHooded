using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    Animator anim;
    [SerializeField] float speed, force;
    float h, v;
    Vector3 spawn;

    [Header("CheckSphereGround")]
    [SerializeField] Transform feets;
    [SerializeField] LayerMask ground;
    float radFeets = 0.2f;
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
        OnGround();//Detecto si estoy en el suelo
    }

    private void FixedUpdate()
    {
        Movement();//llamo al metodo que detecta el movimiento
        
    }

    void OnGround()
    {
        //bool onGround = Physics.CheckSphere(feets.position, radFeets, ground);
        if (Physics2D.OverlapCircle(feets.position, radFeets, ground)) // si estoy tocando el suelo
        {
            Jumping();
            anim.SetBool("Falling", false);//dejaré de caer
        }
        else// si estoy en el aire
            Falling(); //llamo al metodo que detecta si estoy cayendo
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feets.position, radFeets);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Void"))//si entro en contacto con "Void"
        {
            transform.position = spawn; //vuelvo al punto inicial
        }
    }

    void Movement() //método de movimiento
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
    void Flip()//método de giro
    {
        transform.localScale = new Vector3(h, transform.localScale.y, transform.localScale.z);//mi escala en x será igual al valor de h
    }

    void Jumping()//método de salto
    {
        if (Input.GetKeyDown(KeyCode.Space))//si pulso espacio
        {
            rbPlayer.AddForce(Vector3.up * force, ForceMode2D.Impulse);//aplico una ferza hacia arriba
            anim.SetTrigger("Jump");//Activaré animación de saltar
        }
    }

    void Falling()//método de caida
    {
        if (rbPlayer.velocity.y <= 0)//si mi velocidad en y es menor a 0
        {
            anim.SetBool("Falling", true); //estaré cayendo
        }
    }
}
