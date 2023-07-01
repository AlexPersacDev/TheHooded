using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI soulFrag;
    int soulFragCount;
    [SerializeField] GameObject[] playerLifes;
    int playerLifesCount = 5;
    [SerializeField] TextMeshProUGUI playerSouls;
    int playerSoulsCount = 3;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Player.die += SoulsUpdate;
        Player.looseLife += LifesUpdate;
        SoulFragment.collected += SoulFragmentsUpdate;
    }

    private void OnDisable()
    {
        Player.die -= SoulsUpdate;
        Player.looseLife += LifesUpdate;
        SoulFragment.collected -= SoulFragmentsUpdate;
    }

    void SoulFragmentsUpdate()
    {
        soulFragCount++;
        soulFrag.text = "x" + soulFragCount.ToString();
    }

    void LifesUpdate()
    {
        Animator animLifes = playerLifes[playerLifesCount-1].GetComponent<Animator>();
        animLifes.SetTrigger("Loose");
        playerLifesCount--;
    }

    void SoulsUpdate()
    {
        playerSoulsCount--;
        playerSouls.text = "x" + playerSoulsCount.ToString();
        playerLifesCount = 5;
        for (int i = 0; i < playerLifesCount; i++)
        {
            Animator animLifes = playerLifes[i].GetComponent<Animator>();
            animLifes.SetTrigger("Activate");
        }
    }

}
