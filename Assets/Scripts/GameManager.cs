using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;



    int playerLifes = 5;
    int playerSouls = 3;
    int soulFragments = 0;
    [SerializeField] List<Sprite> upgardeList;
    private void Awake()
    {

        if (gM == null) // si no existe gm
        {
            gM = this; //gm soy yo
            DontDestroyOnLoad(gameObject); // no me desturllo
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        SoulFragment.collected += Collectionable;
        Player.looseLife += LoosingPlayerLifes;
        Player.die += LoosingPlayerSouls;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        SoulFragment.collected -= Collectionable;
        Player.looseLife -= LoosingPlayerLifes;
        Player.die -= LoosingPlayerSouls;
    }

    public int PlayerLifes()
    {
        return playerLifes;
    }
    public int PlayerSouls()
    {
        return playerSouls;
    }
    public int SoukFragments()
    {
        return soulFragments;
    }

    public List<Sprite> UpgardesList()
    {
        return upgardeList;
    }

    void LoosingPlayerLifes()
    {
        playerLifes--;
    }

    void Collectionable()
    {
        soulFragments++;
    }

    void LoosingPlayerSouls()
    {
        playerLifes = 5;
        playerSouls--;
    }
}
