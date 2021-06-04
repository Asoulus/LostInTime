using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : AutomaticPickUpObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUp();
        }
    }

    public  void PickUp()
    {
        Player.instance.PickUpHealthPack();

        if (_pickUpSound!=null)
        {
            _pickUpSound.Play();
        }

        Destroy(this.gameObject, 0.1f);
    }
}
