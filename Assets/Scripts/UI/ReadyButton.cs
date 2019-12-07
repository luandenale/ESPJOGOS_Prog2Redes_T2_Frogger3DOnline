using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ReadyButton : NetworkBehaviour {
    private PlayerCharacter localPlayer;

    public void SetReady()
    {
        localPlayer = GameManager.instance.GetLocalPlayerReference();
        localPlayer.CmdSetReady(true);
    }    

    public void SetPlayerName(string newName)
    {
        localPlayer = GameManager.instance.GetLocalPlayerReference();
        localPlayer.CmdSetName(newName);
    }
}
