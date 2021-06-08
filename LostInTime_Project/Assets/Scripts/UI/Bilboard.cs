using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform _cam = null;

    private void Start()
    {
        SetInitialReferences();
    }

    private void LateUpdate() 
    {
        transform.LookAt(transform.position + _cam.forward);
    }

    private void SetInitialReferences()//TODO lepiej przekazaæ kamere
    {
        _cam = Player.instance.cam.GetComponent<Transform>();
    }
}
