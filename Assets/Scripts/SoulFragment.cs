using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFragment : MonoBehaviour, ICollectable
{
    public delegate void Colecction();
    public static event Colecction collected;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ICollectable.Collected()
    {
        collected?.Invoke();//se activar� el evento de recollecci�n
        Destroy(gameObject);
    }
}
