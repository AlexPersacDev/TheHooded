using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float anchoImg;
    [SerializeField] Vector3 direction;

    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float resto = speed * Time.time % anchoImg;
        transform.position = startPos + direction * resto;
    }
}
