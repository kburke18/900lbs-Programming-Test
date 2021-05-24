// Solution for transform from - https://answers.unity.com/questions/12322/drag-gameobject-with-mouse.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragControls : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool canRotate;
    private bool isScaling;
    private AudioSource clipAudio;

    private void Start()
    {
        clipAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        canRotate = true;
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - 
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        canRotate = false;
        clipAudio.Play();
    }

    void OnMouseDrag()
    {
        if (!isScaling)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        canRotate = true;
    }

    public void EnableDrag()
    {
        isScaling = false;
    }

    public void DisableDrag()
    {
        isScaling = true;
    }
}
