using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioPlayer.PlayOverlap("button-hover");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioPlayer.PlayOverlap("button-hover");
    }
}
