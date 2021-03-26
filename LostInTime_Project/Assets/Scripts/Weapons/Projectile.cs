using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            
            collision.gameObject.GetComponent<BodyPart>().TakeDamage(damage);
            Debug.Log("DMG");
            Destroy(this.gameObject);
        }      
    }
    */
}
