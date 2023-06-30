using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Player.collected += Collectionable;
    }

    private void OnDisable()
    {
        Player.collected -= Collectionable;
    }

    void Collectionable()
    {
        Debug.Log("EE");
    }
}
