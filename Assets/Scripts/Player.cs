using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed, force;
    float h, v;
    [Header("CheckSphereGround")]
    [SerializeField] Transform feets;
    [SerializeField] LayerMask ground;
    float radFeets = 0.5f;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        OnGround();
    }

    private void FixedUpdate()
    {
        rbPlayer.AddForce(new Vector3(h, 0, 0) * speed, ForceMode2D.Force);
    }

    void OnGround()
    {
        //bool onGround = Physics.CheckSphere(feets.position, radFeets, ground);
        if (Physics2D.OverlapCircle(feets.position, radFeets, ground) && Input.GetKeyDown(KeyCode.Space))
        {
            rbPlayer.AddForce(Vector3.up * force, ForceMode2D.Impulse);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(feets.position, radFeets);
    //}
}
