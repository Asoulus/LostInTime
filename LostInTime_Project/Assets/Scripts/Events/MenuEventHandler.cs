using System;
using UnityEngine;

public class MenuEventHandler : MonoBehaviour
{
    public static MenuEventHandler instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action<int> onMainMenuButtonPressed;
    public event Action<string, float> onFeedback;

    public void MainMenuButtonPressed(int value)
    {
        if (onMainMenuButtonPressed != null)
        {
            onMainMenuButtonPressed(value);
        }
    }
    public void Feedback(string value, float time)
    {
        if (onFeedback != null)
        {
            onFeedback(value, time);
        }
    }
}
