using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _questUI = null;

    private bool _isShown = false;

    private void Start()
    {
        PlayerInputHandler.instance.onQuestButtonPressed += QuestButtonPressed;
        ToggleQuestUI(false);
    }

    private void QuestButtonPressed()
    {
        ToggleQuestUI(!_isShown);
    }

    private void ToggleQuestUI(bool value)
    {
        if (value)
        {
            _questUI.alpha = 1;
            _questUI.interactable = true;
            _questUI.blocksRaycasts = true;            
        }
        else
        {
            _questUI.alpha = 0;
            _questUI.interactable = false;
            _questUI.blocksRaycasts = false;
        }

        _isShown = value;
    }

    private void OnDisable()
    {
        PlayerInputHandler.instance.onEscapeButtonPressed -= QuestButtonPressed;
    }
}
