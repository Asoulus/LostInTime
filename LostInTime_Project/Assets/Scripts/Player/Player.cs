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

    [SerializeField]
    private int _healthPackAmount = 0;
    [SerializeField]
    private int _powerUpAmount = 0;
    [SerializeField]
    private int _ammoReserves = 0;

    [Header("References")]
    [SerializeField]
    private Slider _healthBar = null;
    [SerializeField]
    private Player_Master _playerMaster = null;

    [Header("UI")]
    [SerializeField]
    private Text _healthPackAmountUIText = null;

    [SerializeField]
    private Text _powerUpAmountUIText = null;

    public Camera cam = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInitialReferences();

        _playerMaster.EventPlayerTakeDamage += TakeDamage;
        _playerMaster.EventPlayerHeal += Heal;
    }

    public void PickUpHealthPack()
    {
        _healthPackAmount++;
        _healthPackAmountUIText.text = _healthPackAmount.ToString();
    }

    public void PickUpPowerUp()
    {
        _powerUpAmount++;
        _powerUpAmountUIText.text = _powerUpAmount.ToString();
    }

    public void PickUpAmmo()
    {
        _ammoReserves++;
        Debug.Log("AMMO");
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

    private void Heal(float healAmount)
    {
        if (_currentHealth == _maxHealth)
            return;

        _currentHealth += healAmount;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthBar.value = _currentHealth;

        _healthPackAmount--;
        _healthPackAmountUIText.text = _healthPackAmount.ToString();
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
        _playerMaster.EventPlayerHeal -= Heal;
    }


}
