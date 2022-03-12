using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform sphere;
    void Update()
    {
        bool isGRounded = Physics.CheckSphere(sphere.position, 1f, 6);
        if (isGRounded)
        {
            Debug.Log("ground");
        }
    }
}
