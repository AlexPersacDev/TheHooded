using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] TextMeshProUGUI soulFrag;
    int soulFragCount;
    [SerializeField] GameObject[] playerLifes;
    int playerLifesCount = 5;
    [SerializeField] TextMeshProUGUI playerSouls;
    int playerSoulsCount = 3;

    [Header("Game Over")]
    [SerializeField] GameObject BlackPanel;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SoulFragment.collected += SoulFragmentsUpdate;
        Player.looseLife += LifesUpdate;
        Player.gameOver += GameOver;
        Player.die += SoulsUpdate;
    }

    private void OnDisable()
    {
        SoulFragment.collected -= SoulFragmentsUpdate;
        Player.looseLife += LifesUpdate;
        Player.die -= SoulsUpdate;
        Player.gameOver -= GameOver;
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

    void GameOver()
    {
        BlackPanel.SetActive(true);
    }
}
