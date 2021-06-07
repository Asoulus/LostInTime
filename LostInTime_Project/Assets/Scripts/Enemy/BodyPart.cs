using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BodyPart : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _dmgMultiplier = 1f;
    [SerializeField]
    private bool _shouldRemove = false; 

    [Header("References")]
    [SerializeField]
    private Enemy _enemy = null;
    [SerializeField]
    private Enemy_Master _enemyMaster = null;
    [SerializeField]
    private Rigidbody _rb = null;
    [SerializeField]
    private Collider _collider = null;
    

    private void Start()
    {
        SetInitialReferences();
        _enemyMaster.EventEnemyDie += RemoveThis;
    }

    private void OnDisable()
    {
        _enemyMaster.EventEnemyDie -= RemoveThis;
    }

    private void RemoveThis()
    {
        if (_shouldRemove)
        {                  
            _collider.enabled = false;
            _rb.isKinematic = true;
            _rb.useGravity = false;

            Destroy(this);
        }
    }

    private void SetInitialReferences()
    {
        _enemy = transform.root.GetComponent<Enemy>();
        _enemyMaster = transform.root.GetComponent<Enemy_Master>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_enemy.IsDead)
        {
            Projectile projectile = collision.transform.GetComponent<Projectile>();

            if (projectile != null)
            {
                TakeDamage(projectile.damage);
                Destroy(projectile.gameObject);
                //TODO sound + effect
            }
        }
        
    }

    public void TakeDamage(float damage)
    {
        damage *= _dmgMultiplier;
        _enemy.TakeDamage(damage);
    }
}
