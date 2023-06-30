using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int playerLifes;
    int playerSouls;
    int soulFragments;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(soulFragments);
    }

    private void OnEnable()
    {
        SoulFragment.collected += Collectionable;
    }

    private void OnDisable()
    {
        SoulFragment.collected -= Collectionable;
    }

    void Collectionable()
    {
        soulFragments++;
    }
}
