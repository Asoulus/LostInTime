using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private int _questID = 0;
    [SerializeField]
    private int _currentState = 0;
    [SerializeField]
    private int _goal = 50;
    [SerializeField]
    private string _inputPrefsName = "";

    private static string _prefsName = "";


    [Header("References")]
    [SerializeField]
    private GameObject _checkMark = null;
    [SerializeField]
    private Text _progressText = null;

    private void Start()
    {
        _prefsName = _inputPrefsName;

        QuestHandler.instance.onEnemyDeath += UpdateQuest;
        QuestHandler.instance.onResetQuests += ResetQuests;

        _currentState = PlayerPrefs.GetInt(_prefsName);
        _currentState = PlayerPrefs.GetInt(_inputPrefsName);
    }

    private void UpdateQuest()
    {
        if (_currentState == _goal)
        {
            QuestHandler.instance.onEnemyDeath -= UpdateQuest;
            return;
        }

        _currentState ++;
        //PlayerPrefs.SetInt(_prefsName, _currentState);
        CheckCompletion();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _progressText.text = _currentState + "/" + _goal;

        if (_currentState == _goal)
        {
            _checkMark.SetActive(true);
        }
    }

    private void CheckCompletion()
    {
        if (_currentState == _goal)
        {
            switch (_questID)
            {
                case 0:
                    {
                        LockWeapons.instance.UnlockWeapon("Revolver");
                    }break;
                case 1:
                    {
                        LockWeapons.instance.UnlockWeapon("AK47");
                    }
                    break;
                case 2:
                    {
                        LockWeapons.instance.UnlockWeapon("OrbGun");
                    }
                    break;
                case 3:
                    {
                        LockWeapons.instance.UnlockWeapon("Shotgun");
                    }
                    break;
            }
        }
    }

    private void ResetQuests()
    {
        PlayerPrefs.SetInt(_prefsName, 0);
    }

    private void OnDisable()
    {
        QuestHandler.instance.onEnemyDeath -= UpdateQuest;
        QuestHandler.instance.onResetQuests -= ResetQuests;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(_prefsName, _currentState);
    }
}
