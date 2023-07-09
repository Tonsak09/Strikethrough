using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialaogueManager : MonoBehaviour
{
    [SerializeField] PlayerLogic player;
    [SerializeField] GameObject textBox;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] KeyCode continueKey;

    [SerializeField] GameObject explodeFX;

    [SerializeField] AK.Wwise.Event myEvent = null;

    private List<string> dialogueList;
    private bool inUse;

    private int index;
    private Transform fxSpawner;

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

                Instantiate(explodeFX, fxSpawner.position, explodeFX.transform.rotation);
                Destroy(fxSpawner.gameObject);

                myEvent.Post(this.gameObject);
            }
        }
    }

    /// <summary>
    /// Attempt to run a new dialogue 
    /// </summary>
    /// <param name="dialogue"></param>
    public bool TryRunDialogue(List<string> dialogue, Transform _fxSpawner)
    {
        if (inUse)
            return false;

        dialogueList = dialogue;
        inUse = true;
        index = 0;

        textBox.SetActive(true);

        player.CanMove = false;
        fxSpawner = _fxSpawner;

        return true;
    }

    private void TurnOff()
    {
        print("turning off");
        inUse = false;
        index = 0;

        textBox.SetActive(false);
        textMesh.text = "";

        player.CanMove = true;
    }
}
