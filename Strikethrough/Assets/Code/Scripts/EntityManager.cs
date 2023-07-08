using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform ground;
    private EntityVisual[] visuals;

    // Start is called before the first frame update
    void Start()
    {
        visuals = GameObject.FindObjectsOfType<EntityVisual>();
    }

    private void Update()
    {
        ground.position = player.position;

        // Update origin pos of every visual 
        for (int i = 0; i < visuals.Length; i++)
        {
            visuals[i].SetPos(player.position);
        }
    }

}
