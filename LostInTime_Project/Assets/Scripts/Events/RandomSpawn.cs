using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _spawns = new List<GameObject>();

    GameObject tasd;

    void Start()
    {
        int index = Random.Range(0, _spawns.Count);

        Player.instance.gameObject.transform.position   = _spawns[index].transform.position;
    }


}
