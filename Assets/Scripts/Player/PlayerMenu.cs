using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMenu : NetworkBehaviour
{

    public static List<PlayerMenu> players = new List<PlayerMenu>();
    public Character character;
    public CharacterSelect chadButton, virginButton;
    public ReadyButton readyButton;
    public InputField characterName;
    public MenuUIManager uiManager;
    public GameObject Chad, Virgin;
    public bool ready;


    private void Start() {

        CmdAddPlayer();

        if (isLocalPlayer) {
            chadButton.localPlayer = this;
            virginButton.localPlayer = this;
            readyButton.localPlayer = this;
        }
    }

    public void SpawnCharacter() {
        if (isLocalPlayer) {
            gameObject.name = characterName.text;
            if (character == Character.Chad) {
                Instantiate(Chad, transform.position, Quaternion.identity, transform);
            }
            else if (character == Character.Virgin) {
                Instantiate(Virgin, transform.position, Quaternion.identity, transform);
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

    [Command]
    public void CmdAddPlayer() {
        RpcAddPlayer();
    }
    [ClientRpc]
    public void RpcAddPlayer() {
        players.Add(this);
    }

    public void OnReadyButtonClick() {
        foreach (PlayerMenu player in PlayerMenu.players) {
            if (!player.ready) {
                return;
            }
        }
        foreach (PlayerMenu player in PlayerMenu.players) {
            player.SpawnCharacter();
        }
        GameManager.startGame = true;
        uiManager.gameObject.SetActive(true);
        uiManager.OpponentConnected();
    }
}
