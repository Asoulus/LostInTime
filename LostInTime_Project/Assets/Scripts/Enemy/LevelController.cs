using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private List<EnemyRoomDetector> _rooms = new List<EnemyRoomDetector>();
    [SerializeField]
    private int _startingRoomId;
    [SerializeField]
    private bool _lvlIsFuture = false;

    public void AssignStartingRoom(int id)
    {
        if (_lvlIsFuture)
            return;

        _startingRoomId = id;

        foreach (var room in _rooms)
        {
            room.GetComponent<Collider>().enabled = false;
        }

        switch (_startingRoomId)
        {
            case 0:
                {
                    foreach (var room in _rooms)
                    {
                        if (room.RoomID == 3)
                        {
                            room.IsFinishingRoom = true;
                            return;
                        }
                    }
                }
                break;
            case 1:
                {
                    foreach (var room in _rooms)
                    {
                        if (room.RoomID == 0)
                        {
                            room.IsFinishingRoom = true;
                            return;
                        }
                    }
                }
                break;
            case 2:
                {
                    foreach (var room in _rooms)
                    {
                        if (room.RoomID == 1)
                        {
                            room.IsFinishingRoom = true;
                            return;
                        }
                    }
                }
                break;
            case 3:
                {
                    foreach (var room in _rooms)
                    {
                        if (room.RoomID == 2)
                        {
                            room.IsFinishingRoom = true;
                            return;
                        }
                    }
                }
                break;

        }
    }
}
