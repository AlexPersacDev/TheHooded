using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;



    int playerLifes;
    int playerSouls;
    int soulFragments;

    private void Awake()
    {

        if (gM == null) // si no existe gm
        {
            gM = this; //gm soy yo
            DontDestroyOnLoad(gameObject); // no me desturllo
        }
        else
            Destroy(gameObject);
    }

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
