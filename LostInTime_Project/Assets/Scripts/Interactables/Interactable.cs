using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private CanvasGroup _display = null;

    private void Start()
    {
        ToggleVisibility(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleVisibility(true);
        }

        PlayerInputHandler.instance.onInteractionButtonPressed += PerformAction;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleVisibility(false);
        }

        PlayerInputHandler.instance.onInteractionButtonPressed -= PerformAction;
    }

    private void ToggleVisibility(bool value)
    {
        if (_display == null)
            return;

        if (value)
        {
            _display.alpha = 1;
        }
        else
        {
            _display.alpha = 0;
        }

        _display.blocksRaycasts = value;
        _display.interactable = value;
    }

    private void PerformAction()
    {
        //TODO: Implement
    }
}
