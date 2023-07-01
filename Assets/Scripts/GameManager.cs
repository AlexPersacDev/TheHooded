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
        SoulFragment.collected += Collectionable;
        Player.looseLife += PlayerLifes;
    }

    private void OnDisable()
    {
        SoulFragment.collected -= Collectionable;
        Player.looseLife += PlayerLifes;
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
