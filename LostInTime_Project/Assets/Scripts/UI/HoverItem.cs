using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverItem : MonoBehaviour, IPointerEnterHandler
{
    private GameObject _hover;

    private AudioSource _hoverSound;

    private void Start()
    {
        _hover = GameObject.Find("Hover_Sound");

        _hoverSound = _hover.GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverSound.Play();
    }
}
