using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [SerializeField]
    private bool _canPause = true;
    [SerializeField]
    private bool _isPaused = false;

    [SerializeField]
    private CanvasGroup _mainUI = null;
    [SerializeField]
    private CanvasGroup _pauseUI = null;

    //[SerializeField]
    //private FirstPersonMovement _player = null;

    public bool IsPaused
    {
        get => _isPaused;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerInputHandler.instance.onEscapeButtonPressed += EscapeButtonPressed;
        TogglePauseUI(false);


        //_player = PlayerReference.instance.GetComponent<FirstPersonMovement>();
    }

    private void EscapeButtonPressed()
    {
        if (_canPause)// && !_player.IsInteracting)
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //PlayerReference.instance.GetComponent<PlayerJump>().enabled = false;
        //_player.CanMove = false;

        ToggleMainUI(false);
        TogglePauseUI(true);

        _isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //PlayerReference.instance.GetComponent<PlayerJump>().enabled = true;
        //_player.CanMove = true;      

        ToggleMainUI(true);
        TogglePauseUI(false);

        _isPaused = false;
    }

    private void ToggleMainUI(bool value)
    {
        if (value)
        {
            _mainUI.alpha = 1;
            _mainUI.interactable = true;
            _mainUI.blocksRaycasts = true;
        }
        else
        {
            _mainUI.alpha = 0;
            _mainUI.interactable = false;
            _mainUI.blocksRaycasts = false;
        }
    }
    
    private void TogglePauseUI(bool value)
    {
        if (value)
        {
            _pauseUI.alpha = 1;
            _pauseUI.interactable = true;
            _pauseUI.blocksRaycasts = true;
        }
        else
        {
            _pauseUI.alpha = 0;
            _pauseUI.interactable = false;
            _pauseUI.blocksRaycasts = false;
        }
    }

    private void OnDisable()
    {
        PlayerInputHandler.instance.onEscapeButtonPressed -= EscapeButtonPressed;
    }
}
