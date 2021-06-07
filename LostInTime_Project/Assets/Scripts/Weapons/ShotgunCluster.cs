using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunCluster : MonoBehaviour
{
    private Rigidbody[] _clusters;

    private float coneSize = 0.5f;

    void Start()
    {
        Physics.IgnoreLayerCollision(7,7);

        _clusters = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < _clusters.Length; i++)
        {
            float width = Random.Range(-10f, 10f) * coneSize;
            float height = Random.Range(-5f, 15f) * coneSize;

            //_clusters[i].AddForce(transform.forward * 100 + transform.right * width + transform.up * height);
            _clusters[i].AddForce(transform.root.forward * 50, ForceMode.Impulse);
            _clusters[i].AddForce(transform.root.right * width, ForceMode.Impulse);
            _clusters[i].AddForce(transform.root.up * height, ForceMode.Impulse);
        }
    }


}
