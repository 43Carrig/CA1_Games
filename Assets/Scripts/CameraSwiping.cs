using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=0ee_Mjzb3Jk&list=PLLH3mUGkfFCWCsGUfwLMnDWdkpQuqW3xa&index=11

public class CameraSwiping : MonoBehaviour
{
    private Vector3 offset;
    private Vector2 touchPosition;
    private float swipeResistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            if (Mathf.Abs(swipeForce) > swipeResistance)
            {
                if (swipeForce < 0)
                {
                    SlideCamera(true);
                }
                else
                {
                    SlideCamera(false);
                }
            }
        }
    }

    public void SlideCamera(bool left)
    {
        if (left)
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else
            offset = Quaternion.Euler(0, -90, 0) * offset;
    }
}
