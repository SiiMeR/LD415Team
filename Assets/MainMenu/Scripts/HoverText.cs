using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HoverText : MonoBehaviour {
    public Color baseColor;
    public Color highlightColor;

    Text buttonText;

	// Use this for initialization
	void Start () {
        buttonText = GetComponentInChildren<Text>();
        buttonText.color = baseColor;
	}
	
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightColor; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = baseColor; //Or however you do your color
    }
}
