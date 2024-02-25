using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorObject : MonoBehaviour
{
    public float speed = 100;

    private void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        
    }
}
