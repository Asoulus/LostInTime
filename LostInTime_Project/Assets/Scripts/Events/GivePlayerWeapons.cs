using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerWeapons : MonoBehaviour
{
    private void Awake()
    {
        Player.instance.AssignWeapons(PlayerChoices.weapons[0], PlayerChoices.weapons[1]);
    }
}
