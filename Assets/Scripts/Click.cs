using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference - https://www.youtube.com/watch?v=OCGoTiV4kbM&list=PLL8DeMf3fIgHX21VkYwCJTdqBaTSvjjEk 
public class Click : MonoBehaviour
{
    [SerializeField] 
    private LayerMask clickablesLayer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickablesLayer))
            {

                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();
                clickOnScript.currentlySelected =! clickOnScript.currentlySelected; 
                clickOnScript.ClickMe();
            }
        }
    }
}
