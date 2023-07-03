using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] int scene;

    private void OnTriggerEnter2D(Collider2D triger)
    {
        if (triger.gameObject.layer == 6)
        {
            SceneManager.LoadScene(scene);
        }
    }

}
