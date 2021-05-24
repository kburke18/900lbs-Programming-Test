// Created by Kimberly Burke - 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControls : MonoBehaviour
{
    public GameObject debugObject;

    private GameObject target;
    private GameObject mainCamera;

    // scale variables
    public Animator colorSlideAnim;
    private bool slideActive = false;

    // rotate variables
    public Vector2 lastPosition;
    public Vector2 rotatespeed;
    public Vector2 rotateBuffers;
    private bool enableRotate;

    // instructions UI
    public Animator instructAnim;
    private bool instructVisible = false;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        FindTarget();
        enableRotate = true;
    }

    private void Update()
    {
        // Rotate target
        if (target != null && enableRotate) { 
            // target property of canRotate must be checked after establishing that target is not null
            if (target.GetComponent<DragControls>().canRotate) { 
                if (Input.GetMouseButtonDown(0))
                {
                    lastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    // Rotate when not touching the target
                    if (!Physics.Raycast(ray, out hit, 100.0f))
                    {
                        Vector2 diff = lastPosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
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
                        lastPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    }
                }
            }
        }
    }

    public void PlaySlideAnim()
    {
        if (slideActive)
        {
            colorSlideAnim.SetTrigger("Slideout");
            slideActive = false;
        } else
        {
            colorSlideAnim.SetTrigger("Slidein");
            slideActive = true;
        }
    }

    public void AnimateInstructions()
    {
        if (instructVisible)
        {
            instructAnim.SetTrigger("Slideout");
            instructVisible = false;
        } else
        {
            instructAnim.SetTrigger("Slidein");
            instructVisible = true;
        }
    }

    public void ChangeToButtonColor(Button selectedButton)
    {
        mainCamera.GetComponent<AudioSource>().Play(); // play click noise
        Vector4 buttonColor = selectedButton.GetComponent<Image>().color;
        if (target != null)
            target.GetComponent<Renderer>().material.color = buttonColor;
    }

    // Scale with UI element
    public void ScaleWithSlider(float scale)
    {
        if (target != null)
            target.transform.localScale = new Vector3(scale, scale, scale);
    }
    
    // Used for debugging purposes when target is no longer on screen
    public void Reset()
    {
        if (target != null)
            target.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
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
