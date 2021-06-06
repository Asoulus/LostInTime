using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_script : MonoBehaviour
{
    public GameObject go1;
    public GameObject go2;
    public GameObject go3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(go1);
            Instantiate(go2);
            Instantiate(go3);
        }
    }
}
