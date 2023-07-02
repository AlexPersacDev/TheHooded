using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    void ActiveGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    void Disabled()
    {
        gameObject.SetActive(false);
    }
}
