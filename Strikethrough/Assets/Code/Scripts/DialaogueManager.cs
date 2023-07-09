using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialaogueManager : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] KeyCode continueKey;

    [SerializeField] AK.Wwise.Event myEvent = null;

    private List<string> dialogueList;
    private bool inUse;

    private int index;

    private void Start()
    {
        TurnOff();
    }

    private void Update()
    {
        if(inUse)
        {
            textMesh.text = dialogueList[index];

            if(Input.GetKeyDown(continueKey))
            {
                index++;
            }

            if(index >= dialogueList.Count)
            {
                TurnOff();
                myEvent.Post(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Attempt to run a new dialogue 
    /// </summary>
    /// <param name="dialogue"></param>
    public bool TryRunDialogue(List<string> dialogue)
    {
        if (inUse)
            return false;

        dialogueList = dialogue;
        inUse = true;
        index = 0;

        textBox.SetActive(true);
        return true;
    }

    private void TurnOff()
    {
        print("turning off");
        inUse = false;
        index = 0;

        textBox.SetActive(false);
        textMesh.text = "";
    }
}
