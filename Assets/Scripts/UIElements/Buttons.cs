using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public delegate void Resume();
    public static event Resume resume;
    public delegate void Options();
    public static event Options options;
    public delegate void Back();
    public static event Back back;
    Button thisButton;
    int la = 10;


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameManager.gM.DisableAltarButton();
        }
    }
    public void RestartGame()
    {
        string lvlName = SceneManager.GetActiveScene().name;//recoge el nombre de la escena actual
        SceneManager.LoadScene(lvlName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        resume?.Invoke();
    }

    public void OptionsMenu()
    {
        options?.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        back?.Invoke();
    }

    public void ButtonLvl1()
    {
        SceneManager.LoadScene(1);
    }
    public void ButtonLvl2()
    {
        SceneManager.LoadScene(4);
    }
    public void Altar()
    {
        thisButton = gameObject.GetComponent<Button>();
        GameManager.gM.AltarButton(thisButton); //le paso el boton pulsado al gm
        SceneManager.LoadScene(3);
    }

}
