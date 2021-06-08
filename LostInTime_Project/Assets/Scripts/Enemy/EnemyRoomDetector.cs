using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomDetector : MonoBehaviour
{
    private int _enemyCount = 0;

    private GameObject _completionDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCount++;
            Debug.Log(_enemyCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCount--;
            Debug.Log(_enemyCount);
        }
    }


}
