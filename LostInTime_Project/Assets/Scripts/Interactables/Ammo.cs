using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : AutomaticPickUpObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        Player.instance.PickUpAmmo();

        if (_pickUpSound != null)
        {
            _pickUpSound.Play();
        }

        Destroy(this.gameObject, 0.1f);
    }
}
