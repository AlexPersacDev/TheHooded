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

    }

    private void OnEnable()
    {
        Player.looseLife += PlayerLifes;
        SoulFragment.collected += Collectionable;
    }

    private void OnDisable()
    {
        Player.looseLife += PlayerLifes;
        SoulFragment.collected -= Collectionable;
    }

    void PlayerLifes()
    {
        playerLifes--;
    }

    void Collectionable()
    {
        soulFragments++;
    }

    void PlayerSouls()
    {
        playerLifes = 5;
        playerSouls--;
    }
}
