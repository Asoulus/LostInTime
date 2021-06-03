using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MenuButton
{
    [SerializeField]
    private GameObject _submenu = null;

    [SerializeField]
    private int id = 0;

    [SerializeField]
    private int _offset = 250;

    private RectTransform _subRect = null;

    private void Start()
    {
        _subRect = _submenu.GetComponent<RectTransform>();
        MenuEventHandler.instance.onMainMenuButtonPressed += MainMenuButtonPressed;

        LeanTween.move(_subRect, new Vector3(2000, 0, 0), 0.15f);
    }

    private void MainMenuButtonPressed(int value)
    {
        if (id == value)
        {
            LeanTween.move(_subRect, new Vector3(_offset, 0, 0), 0.15f);
        }
        else
        {
            LeanTween.move(_subRect, new Vector3(2000, 0, 0), 0.15f);
        }
    }

    public void ToggleSubmenu()
    {
        MenuEventHandler.instance.MainMenuButtonPressed(id);
    }

    private void OnDisable()
    {
        MenuEventHandler.instance.onMainMenuButtonPressed -= MainMenuButtonPressed;
    }
}
