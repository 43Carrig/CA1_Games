using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manipulate : MonoBehaviour
{
    private Renderer rend;
    // Test

    private Color defColour;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        defColour = rend.material.color;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dragMoveTo(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void Tap()
    {
       rend.material.color = Color.yellow;
       
    }

    public void scale(float scaleBy)
    {
        
        
        transform.localScale = Mathf.Clamp(scaleBy, 0,5)* Vector3.one;
    }

    public void unTap()
    {
        rend.material.color = defColour;
    }

    public void rotate(float rotation)
    {
        transform.Rotate(Camera.main.transform.forward, rotation);
    }
}
