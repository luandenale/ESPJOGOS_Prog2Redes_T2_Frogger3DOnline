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
    [SyncVar]
    public int playerID;

    private void Start() {
        GameManager.instance.RegisterPlayer(this);
        playerID = GameManager.instance._players.Count;      
    }

    public void SpawnCharacter() {
        if (character == Character.Chad) {
            Chad.SetActive(true);
            DestroyImmediate(Virgin);
        }
        else if (character == Character.Virgin) {
            Virgin.SetActive(true);
            DestroyImmediate(Chad); //you cant detroy the CHAD, but its ok
        }
        GameManager.instance.uiManager.OpponentReady();
    }
    
    [Command]
    public void CmdSetCharacter(Character characterType) {
        RpcSetCharacter(characterType);
    }
    [ClientRpc]
    public void RpcSetCharacter(Character characterType) {
        character = characterType; 
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
        //check to see if all the players are ready
        foreach (PlayerCharacter player in GameManager.instance._players) {
            if (!player.ready) {
                return;
            }
        }
        //all the players are ready
        foreach (PlayerCharacter player in GameManager.instance._players) {
            player.SpawnCharacter();
        }
        
    }

    [Command]
    public void CmdSetName(string newName) {
        RpcSetName(newName);
    }
    [ClientRpc]
    public void RpcSetName(string newName) {
        gameObject.name = newName;
    }
}
