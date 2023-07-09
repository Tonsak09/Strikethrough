using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characters = new List<GameObject>();
    private bool isOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        bool over = true;
        for (int i = 0; i < characters.Count; i++)
        {
            if(characters[i] != null)
            {
                over = false;
                break;
            }
        }

        if(over)
        {
            if(!isOver)
            {
                StartCoroutine(End());
                isOver = true;
            }
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene(2);
    }
}
