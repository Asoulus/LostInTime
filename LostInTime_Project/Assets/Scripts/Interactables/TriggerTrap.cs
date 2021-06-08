using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerTrap : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _delayTime = 10f;
    [SerializeField]
    private bool _canTrigger = true;

    private void OnTriggerStay(Collider other)
    {
        if (_canTrigger)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                //TODO: Implement
                _canTrigger = false;
                StartCoroutine(Delay());
            }
        }   
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delayTime);
        _canTrigger = true;
    }
}
