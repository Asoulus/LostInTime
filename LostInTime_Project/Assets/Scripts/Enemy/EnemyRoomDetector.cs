using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomDetector : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _roomEnemies = new List<Enemy>();
    [SerializeField]
    private EnemyRoomDetector _nextRoom;
    [SerializeField]
    private GameObject _roomDoor = null;

    private void Start()
    {
        StartCoroutine(CheckDeath());
    }

    private IEnumerator CheckDeath()
    {
        yield return new WaitForSeconds(5f);

        bool test = true;

        foreach (var enemy in _roomEnemies)
        {
            if (!enemy.IsDead)
            {
                test = false;
                break;
            }
        }

        if (test)
        {
            NextRoom();
        }
    }

    private void NextRoom()
    {
        _roomDoor.SetActive(false);
    }
}
