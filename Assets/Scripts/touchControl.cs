using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchControl : MonoBehaviour
{
    private Manipulate newSelectedObject, currentlySelectedObject;
    private float dragDistance;
    private bool possibleTap;
    private float initialPinchDistance =-1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.touches[0];

            Ray laser = Camera.main.ScreenPointToRay(theTouch.position);


            if (Input.touchCount == 1)
            {
                
            

            switch (theTouch.phase)
            {
                case TouchPhase.Began:

 
                    RaycastHit info;
                    possibleTap = true;
                    
                    if (Physics.Raycast(laser, out info))
                    {
                        newSelectedObject = info.collider.GetComponent<Manipulate>();
                        dragDistance = info.distance;


                    }
                    else
                        newSelectedObject = null;
                    
                    break;
                
                case TouchPhase.Moved:

                    possibleTap = false;
                    if (newSelectedObject)
                    {
                        newSelectedObject.dragMoveTo(laser.GetPoint(dragDistance));

                    }

                    break;
                
                case  TouchPhase.Ended:
                    
                    if (possibleTap)
                        if (newSelectedObject)
                        {
                            newSelectedObject.Tap();
                            if (currentlySelectedObject)
                                currentlySelectedObject.unTap();

                            currentlySelectedObject = newSelectedObject;
                        }
                        else
                            emptyTap();

                    break;
                    


            }
            }

            if (Input.touchCount == 2)
            {

                float currentPinchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                if (initialPinchDistance < 0f)
                    initialPinchDistance = currentPinchDistance;
                    
                if (currentlySelectedObject)
                    currentlySelectedObject.scale(currentPinchDistance/initialPinchDistance);
                
                // one liner for zoom - code elsewhere 
            }
        
        }
        
    }

    private void emptyTap()
    {
        currentlySelectedObject = null;
    }
}
