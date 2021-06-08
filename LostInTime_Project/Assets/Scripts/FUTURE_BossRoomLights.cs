using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FUTURE_BossRoomLights : MonoBehaviour
{
    public Light[] swiatla;

    [SerializeField]
    private GameObject _drzwi = null;

    [SerializeField]
    private GameObject _boss = null;


    void Start()
    {
        swiatla = GetComponentsInChildren<Light>();

        foreach (Light swiatlo in swiatla)
            swiatlo.intensity = 0f;

        _boss.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (Light swiatlo in swiatla)
            swiatlo.intensity = 0.6f;

        _drzwi.SetActive(true);

        _boss.SetActive(true);

        //Instantiate(_boss,_location.transform.position, _location.transform.rotation);
    }

}
