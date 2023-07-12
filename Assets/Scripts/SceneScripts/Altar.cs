using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour, IInteractuable
{
    public delegate void Tarot();
    public static event Tarot tarodCards;
    Animator anim;

    public void OnEnable()
    {
        GameManager.upgradeActivated += Destroy;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void IInteractuable.Interaction()
    {
        tarodCards?.Invoke();
    }

    void Destroy()
    {
        anim.SetTrigger("Broken");
    }
}
