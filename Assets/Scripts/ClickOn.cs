using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference - https://www.youtube.com/watch?v=OCGoTiV4kbM&list=PLL8DeMf3fIgHX21VkYwCJTdqBaTSvjjEk
public class ClickOn : MonoBehaviour
{
    [SerializeField] 
    private Material red;
    [SerializeField] 
    private Material green;

    private MeshRenderer myRend;

    [HideInInspector] 
    public bool currentlySelected = false;

    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        ClickMe();
    }

    public void ClickMe()
    {
        if (currentlySelected == false)
        {
            myRend.material = red;
        }
        else
        {
            myRend.material = green;
        }
    }

}
