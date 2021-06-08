using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FUTURE_BossRoomLights : MonoBehaviour
{
    public Light[] swiatla;

    void Start()
    {
        swiatla = GetComponentsInChildren<Light>();

        foreach (Light swiatlo in swiatla)
            swiatlo.intensity = 0f;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (Light swiatlo in swiatla)
            swiatlo.intensity = 0.6f;
        
    }

}
