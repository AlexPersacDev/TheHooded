using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public delegate void Resume();
    public static event Resume resume;
    public delegate void Options();
    public static event Options options;
    public delegate void Back();
    public static event Back back;
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
}