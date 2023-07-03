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

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;
    
    [Header("Options Menu")]
    [SerializeField] GameObject optionsMenu;
    bool pauseMenuOpened;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    private void OnEnable()
    {
        SoulFragment.collected += SoulFragmentsUpdate;
        Player.looseLife += LifesUpdate;
        Player.gameOver += GameOver;
        Player.die += SoulsUpdate;
        Buttons.resume += PauseMenu;
        Buttons.options += OptionsMenu;
        Buttons.back += Back;
    }

    private void OnDisable()
    {
        SoulFragment.collected -= SoulFragmentsUpdate;
        Player.looseLife += LifesUpdate;
        Player.die -= SoulsUpdate;
        Player.gameOver -= GameOver;
        Buttons.resume -= PauseMenu;
        Buttons.options -= OptionsMenu;
        Buttons.back -= Back;
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

    void PauseMenu()
    {
            if (!pauseMenuOpened)
            {
                pauseMenu.SetActive(true);
                pauseMenuOpened = true;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                pauseMenuOpened = false;
                Time.timeScale = 1;
            }
    }

    void OptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    void Back()
    {
        optionsMenu.SetActive(false); 
    }
    void GameOver()
    {
        BlackPanel.SetActive(true);
    }
}
