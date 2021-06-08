using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    [SerializeField]
    private bool _destroyedOnImpact = false;
    [SerializeField]
    private bool _isSword = false;

    private void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
        Physics.IgnoreLayerCollision(7, 8);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isSword)
            return;

        if (collision.transform.tag.Equals("Player") || collision.transform.tag.Equals("Weapon"))
            return;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (_destroyedOnImpact )
        {
            AudioSource tmp = GetComponent<AudioSource>();
            if (tmp!=null)
            {
                GetComponent<AudioSource>().Stop();
            }         
            Destroy(this.gameObject, 0.1f);
            return;
        }

        
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = collision.transform;
        Destroy(GetComponent<Collider>());
    }
}
