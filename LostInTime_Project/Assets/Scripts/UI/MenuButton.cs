using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    [SerializeField]
    protected GameObject _hover;

    [SerializeField]
    protected GameObject _selector;

    protected bool isSelected = false;

    public void Hovering()
    {
        if (_hover != null)
        {
            _hover.SetActive(true);
        }
    }

    public void Leaving()
    {
        if (_hover != null)
        {
            _hover.SetActive(false);
        }
    }

    public void Return()
    {
        MenuEventHandler.instance.MainMenuButtonPressed(-1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToggleSelection()
    {
        StartCoroutine(Pressed());
    }

    protected IEnumerator Pressed()
    {
        isSelected = !isSelected;
        _selector.SetActive(!_selector.activeSelf);

        yield return new WaitForSeconds(0.25f);

        isSelected = !isSelected;
        _selector.SetActive(!_selector.activeSelf);
    }
}
