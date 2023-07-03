using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.TryGetComponent<Player>(out Player player))//si el objeto con el que se colisiona tiene dicha interfaz
        {
            Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = true;
            if (trigger.TryGetComponent<IDamageable>(out IDamageable damagable))
            {
                damagable.Damaged();//se llama al método
                player.Respawn();
            }
        }
    }
}
