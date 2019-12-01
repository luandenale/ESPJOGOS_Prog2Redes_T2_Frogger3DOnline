using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ReadyButton : NetworkBehaviour
{
    public PlayerCharacter localPlayer;

    private void OnMouseDown()
    {
        localPlayer.CmdSetReady(true);
    }

}
