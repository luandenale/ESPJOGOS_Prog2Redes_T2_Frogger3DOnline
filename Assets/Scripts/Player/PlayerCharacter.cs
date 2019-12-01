using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCharacter : NetworkBehaviour
{
    public Character character;
    public GameObject Chad, Virgin;
    public bool ready;


    private void Start() {

    }

    public void SpawnCharacter() {
        if (isLocalPlayer) {
            if (character == Character.Chad) {
                Chad.SetActive(true);
            }
            else if (character == Character.Virgin) {
                Virgin.SetActive(true);
            }
        }
    }

    public void SetCharacter(Character characterType) {
        if (isLocalPlayer) {
            character = characterType;
        }
    }
    //call this in the button event ready
    [Command]
    public void CmdSetReady(bool isReady) {
        RpcSetReady(isReady);
    }
    [ClientRpc]
    public void RpcSetReady(bool isReady) {
        ready = isReady;
        OnReadyButtonClick();
    }

    public void OnReadyButtonClick() {
        foreach (PlayerCharacter player in GameManager._players) {
            if (!player.ready) {
                return;
            }
        }
        foreach (PlayerCharacter player in GameManager._players) {
            player.SpawnCharacter();
        }
        GameManager.startGame = true;
    }
}
