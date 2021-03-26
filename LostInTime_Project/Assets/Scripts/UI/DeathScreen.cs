using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField]
    private Player_Master _playerMaster = null;

    [SerializeField]
    private GameObject _info = null;

    // Start is called before the first frame update
    void Start()
    {
        _info.SetActive(false);
        _playerMaster.EventPlayerDie += Death;
    }

    private void Death()
    {
        _info.SetActive(true);
    }

    private void OnDisable()
    {
        _playerMaster.EventPlayerDie -= Death;
    }
}
