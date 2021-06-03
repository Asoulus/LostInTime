using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onInteractionButtonPressed;
    public event Action onLeftMouseButtonPressed;
    public event Action onEscapeButtonPressed;
    //public event Action onSpaceButtonPressed;
    //public event Action onEnterButtonPressed;
    public event Action onReloadButtonPressed;
    public event Action onQuestButtonPressed;
    public event Action<int> onOneButtonPressed;
    public event Action<int> onTwoButtonPressed;
    public event Action<float> onScrollWheelUsed;


    public void InteractionButtonPressed()
    {
        if (onInteractionButtonPressed != null)
        {
            onInteractionButtonPressed();
        }
    }

    public void LeftMouseButtonPressed()
    {
        if (onLeftMouseButtonPressed != null)
        {
            onLeftMouseButtonPressed();
        }
    }

    public void EscapeButtonPressed()
    {
        if (onEscapeButtonPressed != null)
        {
            onEscapeButtonPressed();
        }
    }
    /*
    public void SpaceButtonPressed()
    {
        if (onSpaceButtonPressed != null)
        {
            onSpaceButtonPressed();
        }
    }

    public void EnterButtonPressed()
    {
        if (onEnterButtonPressed != null)
        {
            onEnterButtonPressed();
        }
    }
    */
    public void ReloadButtonPressed()
    {
        if (onReloadButtonPressed != null)
        {
            onReloadButtonPressed();
        }
    }

    public void QuestButtonPressed()
    {
        if (onQuestButtonPressed != null)
        {
            onQuestButtonPressed();
        }
    }

    public void OneButtonPressed(int selected)
    {
        if (onOneButtonPressed != null)
        {
            onOneButtonPressed(selected);
        }
    }

    public void ScrollWheelUsed(float value)
    {
        if (onScrollWheelUsed != null)
        {
            onScrollWheelUsed(value);
        }
    }

    public void TwoButtonPressed(int selected)
    {
        if (onTwoButtonPressed != null)
        {
            onTwoButtonPressed(selected);
        }
    }
}
