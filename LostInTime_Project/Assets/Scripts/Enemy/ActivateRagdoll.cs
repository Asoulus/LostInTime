using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRagdoll : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Enemy enemy = null;
    [SerializeField]
    private Enemy_Master _enemyMaster = null;
    [SerializeField]
    private Rigidbody _rb = null;
    [SerializeField]
    private Collider _collider = null;


    private void Start()
    {
        SetInitialReferences();
        _enemyMaster.EventEnemyDie += Activate;
    }

    private void OnDisable()
    {
        _enemyMaster.EventEnemyDie -= Activate;
    }

    private void Activate()
    {
        _rb.isKinematic = false;
        _rb.useGravity = true;

        _collider.isTrigger = false;
        _collider.enabled = true;

        if (gameObject.name == "Head")
        {
            _collider.isTrigger = true;
        }
    }

    private void SetInitialReferences()
    {
        enemy = transform.root.GetComponent<Enemy>();
        _enemyMaster = transform.root.GetComponent<Enemy_Master>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
}
