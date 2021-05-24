// Created by Kimberly Burke 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARControls : MonoBehaviour
{
    public GameObject arObject;
    public Vector3 spawnPosition;

    public Slider scaleSlider;
    public float defaultScale;

    public void ResetAll()
    {

        if (arObject.activeInHierarchy)
        {
            Debug.LogWarning("Resetting");
            ResetTarget();
            ResetUI();
        }
    }

    private void ResetUI()
    {
        scaleSlider.value = defaultScale;
    }

    private void ResetTarget()
    {
        arObject.transform.localPosition = spawnPosition;
        arObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        arObject.transform.localRotation = Quaternion.identity;
        arObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
