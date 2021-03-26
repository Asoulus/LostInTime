using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Variables")]
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private float _currentHealth = 100f;

    [Header("References")]
    [SerializeField]
    private Slider _healthBar = null;
    [SerializeField]
    private Player_Master _playerMaster = null;

    public Camera cam = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInitialReferences();

        _playerMaster.EventPlayerTakeDamage += TakeDamage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(50);
        }
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthBar.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            //Death
            _playerMaster.CallEventPlayerDie();
            Debug.Log("YOU HAVE DIED!"); 
        }
    }

    private void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthBar.value = _currentHealth;
    }

    private void SetInitialReferences()
    {
        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;

        cam = GetComponentInChildren<Camera>();
        _playerMaster = GetComponent<Player_Master>();
    }

    private void OnDisable()
    {
        _playerMaster.EventPlayerTakeDamage -= TakeDamage;
    }
}
