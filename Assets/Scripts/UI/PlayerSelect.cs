using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    public PlayerCharacter localPlayer;
    public Character characterType;

    public void SetPlayerModel()
    {
        localPlayer = GameManager.instance.GetLocalPlayerReference();
        localPlayer.CmdSetCharacter(characterType);
    }
}
