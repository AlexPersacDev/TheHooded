using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour, IInteractuable
{
    public delegate void Tarot();
    public static event Tarot tarodCards;

    void IInteractuable.Interaction()
    {
        tarodCards?.Invoke();
    }
}
