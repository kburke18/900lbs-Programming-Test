// Created by Kimberly Burke - 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private GameObject target;

    // Rotate variables
    public Vector2 lastPosition;
    public Vector2 rotatespeed;
    public Vector2 rotateBuffers;
    private bool enableRotate;

    // Scale variables
    public Vector2 lastDistance;
    public float maxScale;
    public float minScale;
    public float zoomBuffer;

    private void Start()
    {
        enableRotate = true;
        FindTarget();
    }

    void Update()
    {
        /** if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            // faster rotation for x-direction
        } else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || 
            Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
            // faster rotation for y-direction
        } **/

        // Rotate object
        if (target != null && enableRotate && target.GetComponent<DragControls>().canRotate)
        {
            // target property of canRotate must be checked after establishing that target is not null
            if (target.GetComponent<DragControls>().canRotate)
            {
                // Track a single touch as a direction control.
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (!Physics.Raycast(ray, out hit, 100.0f))
                    {
                        // Handle finger movements based on TouchPhase
                        switch (touch.phase)
                        {
                            //When a touch has first been detected, change the message and record the starting position
                            case TouchPhase.Began:
                                // Record initial touch position.
                                lastPosition = touch.position;
                                break;

                            //Determine if the touch is a moving touch
                            case TouchPhase.Moved:
                                Vector2 diff = lastPosition - touch.position;
                                if (diff.x > rotateBuffers.x)
                                {
                                    target.transform.Rotate(Vector3.up, rotatespeed.x * Time.deltaTime, Space.World);
                                }
                                else if (diff.x < -rotateBuffers.x)
                                {
                                    target.transform.Rotate(Vector3.up, -rotatespeed.x * Time.deltaTime, Space.World);
                                }
                                if (diff.y > rotateBuffers.y)
                                {
                                    target.transform.Rotate(Vector3.right, -rotatespeed.y * Time.deltaTime, Space.World);
                                }
                                else if (diff.y < -rotateBuffers.y)
                                {
                                    target.transform.Rotate(Vector3.right, rotatespeed.y * Time.deltaTime, Space.World);
                                }
                                lastPosition = touch.position;
                                break;
                            case TouchPhase.Ended:
                                // Report that the touch has ended when it ends
                                break;
                        }
                    }
                }
            }
        }
        // Track double touch as zoom/pinch
        /** if (Input.touchCount == 2)
        {
            // Solution from: https://stackoverflow.com/questions/36129929/how-to-scale-in-and-out-objects-individual-with-pinch-zoom
            
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroLast = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLast = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float touchDeltaMagLast = (touchZeroLast - touchOneLast).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float difference = touchDeltaMag - touchDeltaMagLast;

            if (difference > maxScale)
            {
                difference = maxScale;
            } else if (difference < minScale)
            {
                difference = minScale;
            }
            target.transform.localScale = new Vector3(difference, difference, difference);
        } **/
    }

    public void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("AR Object");
    }

    public void TargetDestroyed()
    {
        target = null;
    }

    // Called for Slider Events
    public void DisableRotate()
    {
        enableRotate = false;
    }

    public void EnableRotate()
    {
        enableRotate = true;
    }
}
