using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henchman : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Vector3> hideLocations;

    [SerializeField] Vector3 idleCenter;
    [SerializeField] float idleRadius;
    [SerializeField] float detectRadius;

    [Header("Idle")]
    [SerializeField] float timeToMove;
    [SerializeField] float speed;
    [SerializeField] float distanceToBeSafe;

    private Vector3 target;
    private float timer;

    private bool fleeing;

    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = this.GetComponent<Character>();
        target = idleCenter; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.GetComponent<Character>().inDialogue)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if(fleeing)
            {
                if(Vector3.Distance(this.transform.position, target) <= distanceToBeSafe)
                {
                    character.enabled = false;
                }
            }
            else
            {
                character.enabled = true;
            }
        }

        
        

        if (Vector3.Distance(player.position, idleCenter) > detectRadius)
        {
            // Idle
            if(timer >= timeToMove)
            {
                target = idleCenter + new Vector3(Random.Range(-idleRadius, idleRadius) / 2.0f, 0.0f, Random.Range(-idleRadius, idleRadius) / 2.0f);
                fleeing = false;
                timer = 0.0f;
            }
            else
            {
                // Move character
                timer += Time.deltaTime;
            }
        }
        else
        {
            // Run to light
            if (!fleeing)
            {
                Vector3 closest = hideLocations[0];
                for (int i = 1; i < hideLocations.Count; i++)
                {
                    if (Vector3.Distance(this.transform.position, hideLocations[i]) < Vector3.Distance(this.transform.position, closest))
                    {
                        closest = hideLocations[i];
                    }
                }
                target = closest;
                fleeing = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < hideLocations.Count; i++)
        {
            Gizmos.DrawSphere(hideLocations[i], distanceToBeSafe);
        }

        Gizmos.DrawWireSphere(idleCenter, idleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(idleCenter, detectRadius);

        Gizmos.DrawSphere(target, 0.2f);
    }
}
