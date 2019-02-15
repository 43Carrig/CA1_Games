using System;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    // New SelectedObject
    private Manipulate newSelectedObject;

    // Currently selected object
    private Manipulate currentlySelectedObject;

    private float dragDistance;

    // Defines if the touch might only be a tap
    private bool possibleTap;
    private float initialPinchDistance = -1;
    private float pastTheta;

    // Update is called once per frame
    void Update()
    {
        // Track a single touch as a direction control
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.touches[0];

            Ray laser = Camera.main.ScreenPointToRay(theTouch.position);


            if (Input.touchCount == 1)
            {
                // https://docs.unity3d.com/ScriptReference/TouchPhase.html
                // Handle finger movements based on TouchPhase
                switch (theTouch.phase)
                {
                    //When a touch has first been detected, record the starting position
                    case TouchPhase.Began:

                        RaycastHit info;
                        possibleTap = true;

                        //If Lazer hits object in World
                        if (Physics.Raycast(laser, out info))
                        {
                            // Get Manipulate Component off newly selected Object
                            newSelectedObject = info.collider.GetComponent<Manipulate>();
                            dragDistance = info.distance;
                        }
                        // If nothing hits
                        else
                            newSelectedObject = null;

                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:
                        possibleTap = false;
                        if (newSelectedObject)
                        {
                            //  Drag selected object
                            newSelectedObject.dragMoveTo(laser.GetPoint(dragDistance));
                        }
                        else
                        {
                            //Drag Camera Postion (Not chaning field of view)
                            Vector2 touchDeltaPosition = Input.touches[0].deltaPosition;
                            Camera.main.transform.Translate(-touchDeltaPosition.x * 0.005f,
                                -touchDeltaPosition.y * 0.001f, 0);
                        }

                        break;

                    // Touch ended
                    case TouchPhase.Ended:

                        //Is a tap
                        if (possibleTap)
                            if (newSelectedObject)
                            {
                                // Change newly selected Object color
                                newSelectedObject.Tap();
                                if (currentlySelectedObject)
                                    //Change old selected object color back to normal
                                    currentlySelectedObject.unTap();

                                // Set newly selected object to old selected object in prep for next frame
                                currentlySelectedObject = newSelectedObject;
                            }
                            //  Not a tap
                            else
                                emptyTap();

                        break;
                }
            }

            //Scaling and rotating
            if (Input.touchCount == 2)
            {

                float currentPinchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                //Pinching in
                if (initialPinchDistance < 0f)
                    initialPinchDistance = currentPinchDistance;
                
                if (currentlySelectedObject)
                    currentlySelectedObject.scale(currentPinchDistance / initialPinchDistance);

                // Only runs if no object is currently selected
                if (currentlySelectedObject == null)
                    // Zoom works
                    Camera.main.fieldOfView += (currentPinchDistance - initialPinchDistance) * 0.005f;
                
                // Taken from class whiteboard
                var currentTheta = Mathf.Atan(
                    Input.touches[0].position.y - Input.touches[1].position.y /
                        Input.touches[0].position.x - Input.touches[1].position.x);

                if (currentlySelectedObject != null)
                {
                    // Object rotation 
                    currentlySelectedObject.rotate((currentTheta - pastTheta) * Mathf.Rad2Deg * -100);
                }
                else
                {
                    // Camera rotation
                    Camera.main.transform.Rotate(Camera.main.transform.forward, currentTheta - pastTheta);
                }

                pastTheta = currentTheta;

                if (Input.touches[1].phase == TouchPhase.Ended)
                {
                    initialPinchDistance = -1;
                }
            }
        }
    }
    
    /// <summary>                                   
    /// Removes reference from old selected object  
    /// </summary>                                  
    private void emptyTap()
    {
        currentlySelectedObject = null;
    }
}


