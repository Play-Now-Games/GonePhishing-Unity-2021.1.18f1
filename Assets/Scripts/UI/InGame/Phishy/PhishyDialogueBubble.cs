using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhishyDialogueBubble : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.GetComponentInParent<CanvasGroup>().interactable)
            GetComponentInParent<Phishy>().AdvanceDialogueBubble();
    }
}
