using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Master : MonoBehaviour
{

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventPlayerDie;

    public delegate void HealthEventHandler(float amount);
    public event HealthEventHandler EventPlayerTakeDamage;

    public void CallEventPlayerDie()
    {
        if (EventPlayerDie != null)
        {
            EventPlayerDie();
        }
    }  
    public void CallEventPlayerTakeDamage(float amount)
    {
        if (EventPlayerTakeDamage != null)
        {
            EventPlayerTakeDamage(amount);
        }
    }
}
