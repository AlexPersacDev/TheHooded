using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] GameObject playerUIGroup;
    [SerializeField] TextMeshProUGUI soulFrag;
    int soulFragCount;
    [SerializeField] GameObject[] playerLifes;
    int playerLifesCount;
    [SerializeField] TextMeshProUGUI playerSouls;
    int playerSoulsCount;

    [Header("Game Over")]
    [SerializeField] GameObject BlackPanel;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;
    
    [Header("Options Menu")]
    [SerializeField] GameObject optionsMenu;
    bool pauseMenuOpened;

    [Header("Tarot Cards")]
    [SerializeField] GameObject tarot;
    List<Sprite> tarotCards;
    [SerializeField] List<Button> buttons;
    [SerializeField] GameObject tarotPanel;
    [SerializeField] List<TextMeshProUGUI> titleCard;
    [SerializeField] List<TextMeshProUGUI> descriptionCard;
    [SerializeField] List<TextMeshProUGUI> pricersCard;
    [SerializeField] List<string> titles;
    [SerializeField] List<string> descriptions; 
    [SerializeField] List<int> prices; 
    bool dashAttackActive;
    [SerializeField] Sprite dashAttack;

    private void OnEnable()
    {
        SoulFragment.collected += SoulFragmentsUpdate;
        Player.looseLife += LifesUpdate;
        Player.gameOver += GameOver;
        Player.die += SoulsUpdate;
        Buttons.resume += PauseMenu;
        Buttons.options += OptionsMenu;
        Buttons.back += Back;
        Altar.tarodCards += TarotCards;
        GameManager.upgradeActivated += UpgradeActivated;
    }

    void Start()
    {
        playerLifesCount = GameManager.gM.PlayerLifes();
        playerSoulsCount = GameManager.gM.PlayerSouls();
        soulFragCount = GameManager.gM.SoulFragments();
        EnableLifes();
        playerSouls.text = "x" + playerSoulsCount.ToString();
        soulFrag.text = "x" + soulFragCount.ToString();
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }


    private void OnDisable()
    {
        SoulFragment.collected -= SoulFragmentsUpdate;
        Player.looseLife -= LifesUpdate;
        Player.die -= SoulsUpdate;
        Player.gameOver -= GameOver;
        Buttons.resume -= PauseMenu;
        Buttons.options -= OptionsMenu;
        Buttons.back -= Back;
        Altar.tarodCards -= TarotCards;
        GameManager.upgradeActivated -= UpgradeActivated;
    }

    void EnableLifes()
    {
        for (int i = 0; i < playerLifesCount; i++)
        {
            playerLifes[i].SetActive(true);
        }
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

    void CanGetDashAttack()
    {
        dashAttackActive = true;
        tarotCards.Add(dashAttack);
    }
    void TarotCards()
    {
        int counter = 0; //indice para sacar unicamente 3 cartas
        Time.timeScale = 0;
        playerUIGroup.SetActive(false);
        tarotCards = GameManager.gM.UpgardesList();
        tarot.SetActive(true); //Activo el grupo
        while (counter <= 2)// mientras el contador sea menor o igual a dos
        {
            for (int i = 0; i < buttons.ToArray().Length; i++) //recorre la lista de botones
            {
                if (buttons[counter] != buttons[i]) //si el botón actual es diferente al del for
                {
                    while (buttons[counter].image.sprite == buttons[i].image.sprite || buttons[counter].image.sprite == null) //mientras tengan el mismo sprite
                    {
                        int indexCard = Random.Range(0, tarotCards.ToArray().Length); //saca un indice
                        buttons[counter].image.sprite = tarotCards[indexCard]; //y aplicale dicho sprite
                        titleCard[counter].text = titles[indexCard];
                        descriptionCard[counter].text = descriptions[indexCard];
                        pricersCard[counter].text = prices[indexCard].ToString();
                    }
                }
            }
            counter++;
        }
             
    }

    void UpgradeActivated()
    {
        soulFragCount = GameManager.gM.SoulFragments();
        Time.timeScale = 1;
        tarot.SetActive(false);
        playerUIGroup.SetActive(true);
    }

}
