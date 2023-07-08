using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerLight : MonoBehaviour
{
    [SerializeField] float timeTillDie = 1.0f;
    private float count;

    private void OnTriggerStay(Collider other)
    {
        count += Time.deltaTime;

        if(count > timeTillDie)
        {
            // Restart to closest respace location 
            print("Restart");
        }
    }
}