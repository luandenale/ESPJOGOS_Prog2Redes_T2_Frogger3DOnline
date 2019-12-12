using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class CrossFinishLine : NetworkBehaviour
{
    [SerializeField]
    private LayerMask _finishLayer;
    private BoxCollider _collider;
    private PlayerCharacter _playerCharacter;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _playerCharacter = GetComponent<PlayerCharacter>();
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if (isLocalPlayer)
        {
            if ((_finishLayer & (1 << p_other.gameObject.layer)) != 0)
                CmdUpdateCrossLine();
        }
    }

    [Command]
    public void CmdUpdateCrossLine()
    {
        RpcUpdateCrossLine();
    }

    [ClientRpc]
    public void RpcUpdateCrossLine() {

        GameManager.instance.gameEnded = true;

        if (GameManager.instance.gameEnded)
        {
            if (isLocalPlayer)
                GameManager.instance.MatchWon();
            else
                GameManager.instance.MatchLost();

            _playerCharacter.CmdToMenu();
        }
    }

}
