using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _spawnRate = 2f;
    [SerializeField]
    private GameObject _enemy = null;
    [SerializeField]
    private int _amount = 3;
    [SerializeField]
    private float _spawnRadius = 5f;
    [SerializeField]
    private bool _canSpawn = true;
    [SerializeField]
    private int _enemyCount = 0;

    private int _count = 0;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_spawnRate);

        if (_canSpawn)
        {
            for (int i = 0; i < _amount; i++)
            {
                Vector3 spawnPos = transform.position + Random.insideUnitSphere * _spawnRadius;
                Instantiate(_enemy, spawnPos, Quaternion.identity);
                _count++;
            }
        }
        if (_count <= _enemyCount) 
        {
            StartCoroutine(Spawn());
        }       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }

}
