using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Display Interaction Settings")]
    [SerializeField] Transform display;
    [SerializeField] float displayUpRate;
    [SerializeField] float displayDownRate;
    [SerializeField] AnimationCurve displayCurveX;
    [SerializeField] AnimationCurve displayCurveY;

    [Header("Dialogue")]
    [SerializeField] KeyCode interactKey;
    [SerializeField] DialaogueManager dialaogueManager;
    [SerializeField] List<string> dialogue;

    [SerializeField] AK.Wwise.Event InteractEvent = null;

    private float currentDisplay;
    private bool inZone;
    public bool inDialogue;

    private bool over;

    private enum DialogueState
    {
        canStart,
        cannotStart
    }

    private void Start()
    {
        inZone = false;
        inDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!over)
        {
            if (!inDialogue)
            {
                UpdateDisplay();

                if (Input.GetKeyDown(interactKey) && inZone)
                {
                    inDialogue = true;
                    InteractEvent.Post(this.gameObject);
                }
            }
            else
            {
                display.gameObject.SetActive(false);
                over = dialaogueManager.TryRunDialogue(dialogue);
            }
        }
        
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
