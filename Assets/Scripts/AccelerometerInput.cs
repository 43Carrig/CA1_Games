using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference - https://www.youtube.com/watch?v=XZWNXsjIvrE
public class AccelerometerInput : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
    }
}
