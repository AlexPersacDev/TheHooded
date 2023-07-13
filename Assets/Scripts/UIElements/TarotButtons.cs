using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class TarotButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button thisButton;
    [SerializeField] TextMeshProUGUI titleCard;
    [SerializeField] TextMeshProUGUI descriptionCard;
    [SerializeField] TextMeshProUGUI priceCard;
    int price;

    List<Sprite> tarotUpgrades;


    void Start()
    {
        int.TryParse(priceCard.text, out price); //pasa el precio que está en string a int
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        titleCard.enabled = true;
        descriptionCard.enabled = true;
        priceCard.enabled = true;
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        titleCard.enabled = false;
        descriptionCard.enabled = false;
        priceCard.enabled = false;
    }

    public void ActivateUpgrade()
    {
        FindWichUpgrade();
    }

    void FindWichUpgrade()
    {
        if (thisButton.image.sprite.name == "DistanceAttack")
        {
            GameManager.gM.UnlockDistanceAttack(price);
        }
        else if (thisButton.image.sprite.name == "MeleAttack2")
        {
            GameManager.gM.UnlockRange(price);
        }
        else if (thisButton.image.sprite.name == "Dash")
        {
            GameManager.gM.UnlockDash(price);
        }
        else if (thisButton.image.sprite.name == "Shield")
        {
            GameManager.gM.UnlockShield(price);
        }
        else if (thisButton.image.sprite.name == "DashAttack")
        {
            GameManager.gM.UnlockShield(price);
        }
        else if (thisButton.image.sprite.name == "WallJump")
        {
            GameManager.gM.UnlockShield(price);
        }
    }

    
}
