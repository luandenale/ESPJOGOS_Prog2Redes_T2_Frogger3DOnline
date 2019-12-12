using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceSpawn : MonoBehaviour
{
    public float minDistanceToSpawn = 20f;
    public bool canSpawn = false;

    public void CheckPlayersPos() {

        float nearestPos = 10000000;
        float spawnToPlayer;
        foreach (PlayerCharacter player in GameManager.instance._players)
        {
            spawnToPlayer = Mathf.Abs((transform.parent.position - player.transform.position).z); //Distance from player to Spawn
            if (spawnToPlayer <= nearestPos) {
                nearestPos = spawnToPlayer; //Clossest Player from the spawn
            }
        }

        if (nearestPos <= minDistanceToSpawn) {
            canSpawn = true;
        }
    }

}
