using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private string _objectName = null;
    [SerializeField]
    private bool _isExitDoor = false;
    [SerializeField]
    private bool _isLevelExitDoor = false;
    [SerializeField]
    private bool _isDoorOpener = false;

    [Header("References")]
    [SerializeField]
    private CanvasGroup _display = null;
    [SerializeField]
    private Text _primaryText = null;
    [SerializeField]
    private Text _secondaryText = null;
    [SerializeField]
    private LevelLoader _levelLoader = null;
    [SerializeField]
    private LockWeapons _weaponLocker = null;
    [SerializeField]
    private GameObject _Door = null;

    private void Start()
    {
        ToggleVisibility(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_weaponLocker!=null && !_isDoorOpener)
        {
            if (!_weaponLocker.ReturnLocked(_objectName))
                return;
        }
        

        if (other.CompareTag("Player"))
        {
            ToggleVisibility(true);
            
        }

        PlayerInputHandler.instance.onOneButtonPressed += PerformAction;
        PlayerInputHandler.instance.onTwoButtonPressed += PerformAction;
        PlayerInputHandler.instance.onInteractionButtonPressed += ExitDoor;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_weaponLocker.ReturnLocked(_objectName) && _objectName != null && _weaponLocker != null) 
            return;

        if (other.CompareTag("Player"))
        {
            ToggleVisibility(false);
            
        }

        PlayerInputHandler.instance.onOneButtonPressed -= PerformAction;
        PlayerInputHandler.instance.onTwoButtonPressed -= PerformAction;
        PlayerInputHandler.instance.onInteractionButtonPressed -= ExitDoor;
    }

    private void ExitDoor()
    {
        if (_isDoorOpener)
        {
            Debug.Log("sd");
            _Door.SetActive(false);
            return;
        }

        if (_isLevelExitDoor)
        {
            _levelLoader.LoadLevel("PlayerHub");
            return;
        }

        if (!_isExitDoor)
            return;   

        if (PlayerChoices.weapons[0] == null || PlayerChoices.weapons[1] == null)
        {
            Player.instance.ToggleTooltip(true, "Must select two weapons before going into a level!!!");
            return;
        }

        List<string> tmp = new List<string>();

        tmp.Add("Past");
        tmp.Add("Present");
        tmp.Add("Future");


        _levelLoader.LoadLevel(tmp[Random.Range(0, 3)]);
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

    private void PerformAction(int value)
    {
        if (_isDoorOpener)
            return;


        if (_isLevelExitDoor)
        {
            if (value == 1) 
            {
                _levelLoader.LoadLevel("Menu");
            }
            else
            {
                Application.Quit();
            }
            return;
        }

        if (_isExitDoor)
            return;

        if (value == 1) 
        {
            if (PlayerChoices.weapons[1] == _objectName)
            {
                return; 
            }

            PlayerChoices.weapons[0] = _objectName;
            _primaryText.text = "Primary Weapon: " + _objectName;
        }
        else
        {
            if (PlayerChoices.weapons[0] == _objectName)
            {
                return;
            }

            PlayerChoices.weapons[1] = _objectName;
            _secondaryText.text = "Secondary Weapon: " + _objectName;
        }
        
    }
}
