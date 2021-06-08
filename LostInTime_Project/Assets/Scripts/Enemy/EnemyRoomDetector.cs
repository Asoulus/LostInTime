using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomDetector : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private int _roomID = 0;
    [SerializeField]
    private bool _isStartingRoom = false;
    [SerializeField]
    private bool _isFinishingRoom = false;

    [Header("References")]
    [SerializeField]
    private List<GameObject> _roomEnemies = new List<GameObject>();
    [SerializeField]
    private GameObject _nextRoomDoor = null;
    [SerializeField]
    private GameObject _exitObject = null;
    [SerializeField]
    private LevelController _levelController = null;

    public bool IsStartingRoom
    {
        get => _isStartingRoom;
    }

    public bool IsFinishingRoom
    {
        get => _isFinishingRoom;
        set => _isFinishingRoom = value;
    }

    public int RoomID
    {
        get => _roomID;
    }

    private void Start()
    {
        StartCoroutine(CheckDeath());
        _exitObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isStartingRoom = true;
            _levelController.AssignStartingRoom(_roomID);
        }
    }

    private IEnumerator CheckDeath()
    {
        yield return new WaitForSeconds(1f);

        bool test = true;

        foreach (GameObject enemy in _roomEnemies)
        {
            if (enemy != null)
            {
                test = false;
                break;
            }
        }

        if (test)
        {
            if (IsFinishingRoom)
            {
                _exitObject.SetActive(true);
            }
            else
            {
                NextRoom();
            }
            
            yield return null;
        }

        StartCoroutine(CheckDeath());
    }

    private void NextRoom()
    {
        _nextRoomDoor.SetActive(false);
    }

}
