using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _maxHealth = 250f;
    [SerializeField]
    private float _currentHealth = 250f; 
    [SerializeField]
    private float _despawnTime = 2f;  
    [SerializeField]
    private bool _isDead = false;
    [SerializeField]
    private string _myName = string.Empty;

    [Header("References")]
    [SerializeField]
    private NavMeshAgent _agent = null;
    [SerializeField]
    private Enemy_Master _enemyMaster = null;
    [SerializeField]
    private Text _nameField = null; 
    [SerializeField]
    private Slider _healthBar = null;
    [SerializeField]
    private GameObject _enemyUIRoot = null;

    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    private void Start()
    {
        SetInitialReferences();
    }

    public void TakeDamage(float value)
    {
        _currentHealth -= value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        _healthBar.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        _healthBar.value = _currentHealth;
    }

    private void Death()
    {
        _isDead = true;
        _agent.baseOffset = 0; //zeby mesh nie byl pod podlaga a byla wartość zmieniona zeby nie lewitowal
        _agent.enabled = false;

        _enemyMaster.CallEventEnemyDie();

        _enemyUIRoot.SetActive(false); //wylaczanie nazwy i zdrowia przeciwnika

        Destroy(gameObject, _despawnTime);
    }

    private void SetInitialReferences()
    {
        _agent = GetComponentInChildren<NavMeshAgent>();
        _enemyMaster = GetComponent<Enemy_Master>();

        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _currentHealth;
        _nameField.text = _myName;
    }
}
