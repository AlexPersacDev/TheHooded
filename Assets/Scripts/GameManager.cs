using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;



    int playerLifes = 5;
    int playerSouls = 3;
    int soulFragments = 100;
    [Header("lista de mejoras")]
    [SerializeField] List<Sprite> upgradeList;
    List<bool> upgrades;
    //Desbloqueables PowerUps
    
    bool dash = false;
    bool dashAttack = false;
    bool climb = false;
    bool wallJump = false;
    bool distanceAttack = false;
    bool meleAtack2 = false;
    bool shield = false;
    bool DobleJump = false;

    string actualButtonTag;
    bool upgraded = false;
    List<string> disabledButtons;

    public delegate void UpgradeActivated();
    public static event UpgradeActivated upgradeActivated;
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
        for (int i = 0; i < upgradeList.ToArray().Length; i++)
        {

        }
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
    public int SoulFragments()
    {
        return soulFragments;
    }

    public List<Sprite> UpgardesList()
    {
        return upgradeList;
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
    //desbloquear mejoras
    public void UnlockDistanceAttack(int price)
    {
        if (soulFragments >= price)
        {
            soulFragments -= price;
            distanceAttack = true;
            upgradeActivated?.Invoke();
            upgraded = true;

        }
    }
    public void UnlockRange(int price)
    {
        if (soulFragments >= price)
        {
            soulFragments -= price;
            meleAtack2 = true;
            upgradeActivated?.Invoke();
            upgraded = true;

        }
    }
    public void UnlockDash(int price)
    {
        if (soulFragments >= price)
        {
            soulFragments -= price;
            dash = true;
            upgradeActivated?.Invoke();
            upgraded = true;
        }
    }
    public void UnlockShield(int price)
    {
        if (soulFragments >= price)
        {
            soulFragments -= price;
            shield = true;
            upgradeActivated?.Invoke();
            upgraded = true;

        }
    }
    //pasar datos de mejoras
    public bool DistanceAttack()
    {
       return distanceAttack;
    }
    public bool Range()
    {
        return meleAtack2;
    }
    public bool Dash()
    {
        return dash;
    }
    public bool Shield()
    {
        return shield;
    }

    public void AltarButton(Button altarButton)
    {
        actualButtonTag = altarButton.tag;
    }

    public void DisableAltarButton()
    {
        //if (disabledButtons.Count != 0)
        //{
        //    for (int i = 0; i < disabledButtons.Count; i++)
        //    {
        //        GameObject button = GameObject.FindWithTag(disabledButtons[i]);
        //        button.SetActive(false);
        //    }
        //}
        if (upgraded)
        {
            //int index = actualButtonTag.Count;
            GameObject button = GameObject.FindWithTag(actualButtonTag);
            button.SetActive(false);
            upgraded = false;
            //disabledButtons.Add(button.tag);
        }
    }
}
