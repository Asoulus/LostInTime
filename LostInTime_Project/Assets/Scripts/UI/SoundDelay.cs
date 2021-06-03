using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDelay : MonoBehaviour
{

    void Start()
    {
        AudioListener.volume = 0;
        StartCoroutine(TurnSoundBackOn());
    }

    private IEnumerator TurnSoundBackOn()
    {
        yield return new WaitForSeconds(0.25f);
        AudioListener.volume = 1;
    }

}
