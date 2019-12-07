using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class CrossFinishLine : NetworkBehaviour
{
    [SerializeField]
    private LayerMask finishLayer;
    private BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {

        if ((finishLayer & (1 << other.gameObject.layer)) != 0) {
            CmdUpdateCrossLine();
        }

    }

    [Command]
    public void CmdUpdateCrossLine() {
        RpcUpdateCrossLine();
    }

    [ClientRpc]
    public void RpcUpdateCrossLine() {

        GameManager.instance.gameEnded = true;

        if (GameManager.instance.gameEnded) {
            if (isLocalPlayer) {
                GameManager.instance.MatchWon();
            }

            else {
                GameManager.instance.MatchLost();
            }

            GetComponent<PlayerCharacter>().CmdToMenu();
        }
    }

}
