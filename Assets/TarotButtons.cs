using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class TarotButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI titleCard;
    [SerializeField] TextMeshProUGUI descriptionCard;
    [SerializeField] TextMeshProUGUI priceCard;
    void Start()
    {
        
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
}
