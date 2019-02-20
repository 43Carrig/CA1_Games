﻿using System;
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
            // Input.touches - Returns list of objects representing status of all touches during last frame.
            // Each entry represents a status of a finger touching the screen.
            Touch theTouch = Input.touches[0];

            // Ray - A ray is an infinite line starting at origin and going in some direction
            Ray laser = Camera.main.ScreenPointToRay(theTouch.position);


            if (Input.touchCount == 1)
            {
                // https://docs.unity3d.com/ScriptReference/TouchPhase.html
                // Handle finger movements based on TouchPhase
                switch (theTouch.phase)
                {
                    //When a touch has first been detected, record the starting position
                    case TouchPhase.Began:

                        // Used to get information back from a raycast
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
                            //Drag Camera Position (Not changing field of view)
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
                // Mathf.Atan - Returns the arc-tangent of f - the angle in radians whose tangent is f. 
                // https://gamedev.stackexchange.com/questions/14602/what-are-atan-and-atan2-used-for-in-games
                // atan(angle) = opposite/adjacent
                var currentTheta = Mathf.Atan2(
                    Input.touches[0].position.y - Input.touches[1].position.y, 
                        Input.touches[0].position.x - Input.touches[1].position.x);

                if (currentlySelectedObject != null)
                {
                    // Object rotation 
                    // Mathf.Rad2Deg - Radians-to-degrees conversion
                    currentlySelectedObject.rotate((currentTheta - pastTheta) * Mathf.Rad2Deg);
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

            if (Input.touchCount == 4)
            {
                // https://answers.unity.com/questions/899037/applicationquit-not-working-1.html
                #if UNITY_EDITOR
                                // Application.Quit() does not work in the editor so
                                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                                UnityEditor.EditorApplication.isPlaying = false;
                #else
                         Application.Quit();
                #endif
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


