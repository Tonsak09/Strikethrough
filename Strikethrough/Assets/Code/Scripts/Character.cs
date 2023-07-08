using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] Transform display;
    [SerializeField] float displayUpRate;
    [SerializeField] float displayDownRate;
    [SerializeField] AnimationCurve displayCurveX;
    [SerializeField] AnimationCurve displayCurveY;

    private float currentDisplay;
    private bool inZone;

    private void Start()
    {
        inZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    /// <summary>
    /// Changes the display in how it's displayed
    /// </summary>
    private void UpdateDisplay()
    {
        // Change the display amount 
        if(inZone)
        {
            currentDisplay = Mathf.Clamp01(currentDisplay + displayUpRate * Time.deltaTime);
        }
        else
        {
            currentDisplay = Mathf.Clamp01(currentDisplay - displayDownRate * Time.deltaTime);
        }

        // Change scale 
        display.localScale = new Vector3(displayCurveX.Evaluate(currentDisplay), displayCurveY.Evaluate(currentDisplay), currentDisplay);   
    }

    private void OnTriggerEnter(Collider other)
    {
        inZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inZone = false;
    }
}
