using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegPoint : MonoBehaviour
{
    [SerializeField]
    private float _range = 0.2f;
    [SerializeField]
    private float _moveValue = 0.2f;
    [SerializeField]
    private float _lowDistanceValue = 0.1f;
    [SerializeField]
    private float _highDistanceValue = 0.25f;


    //TODO poprawic zmiane wysokosci punktu
    private void FixedUpdate()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, _range);       
         
        if (hit.distance <= _lowDistanceValue)
        {          
            transform.position = new Vector3(transform.position.x ,transform.position.y + _moveValue, transform.position.z );
        }else 
        if (hit.distance >= _highDistanceValue)
        {            
            transform.position = new Vector3(transform.position.x, transform.position.y - _moveValue, transform.position.z);
        }
        
    }


    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, Vector3.down * _range);
    }
    */

    private void OnDestroy()
    {
        this.enabled = false;
    }

}
