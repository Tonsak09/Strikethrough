using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Vector3> respawnPoints;
   
    public void Respawn()
    {
        Vector3 spawnPoint = respawnPoints[0];
        for (int i = 1; i < respawnPoints.Count; i++)
        {
            // Bad to do but I am so tired 
            if (Vector3.Distance(respawnPoints[i], player.position) < Vector3.Distance(spawnPoint, player.position))
            {
                spawnPoint = respawnPoints[i];
            }
        }
        print(spawnPoint);
        player.GetComponent<CharacterController>().enabled = false;
        player.position = spawnPoint;
        player.GetComponent<CharacterController>().enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < respawnPoints.Count; i++)
        {
            Gizmos.DrawSphere(respawnPoints[i], 0.1f);
        }
    }
}
