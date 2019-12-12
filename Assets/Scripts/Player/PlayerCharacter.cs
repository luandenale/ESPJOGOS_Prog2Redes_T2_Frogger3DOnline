using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCharacter : NetworkBehaviour
{
    [SerializeField]
    private GameObject _chadGameObject;
    [SerializeField]
    private GameObject _virginGameObject;
    
    private Character _character; 
    public Character Character 
    {
        get
        {
            return _character;
        }
    }

    private bool _isReady;
    public bool IsReady
    {
        get
        {
            return _isReady;
        }
    }

    
    [SyncVar]
    public int playerID;

    public bool playerAlive;
    public bool playerEnabled;

    private void Awake()
    {
        GameManager.instance.RegisterPlayer(this);
        playerID = GameManager.instance._players.Count;      
    }

    public void SpawnCharacter()
    {
        if (_character == Character.Chad)
        {
            _chadGameObject.SetActive(true);
            DestroyImmediate(_virginGameObject);
        }
        else if (_character == Character.Virgin)
        {
            _virginGameObject.SetActive(true);
            DestroyImmediate(_chadGameObject); //you cant detroy the CHAD, but its ok
        }
        
        GameManager.instance.uiManager.OpponentReady();
    }

#region SetCharacter
    [Command]
    public void CmdSetCharacter(Character p_characterType)
    {
        RpcSetCharacter(p_characterType);
    }
    [ClientRpc]
    public void RpcSetCharacter(Character p_characterType)
    {
        _character = p_characterType; 
    }
#endregion

#region SetReady
    //call this in the button event ready
    [Command]
    public void CmdSetReady(bool p_isReady)
    {
        RpcSetReady(p_isReady);
    }
    [ClientRpc]
    public void RpcSetReady(bool p_isReady)
    {
        _isReady = p_isReady;
        OnReadyButtonClick();
    }

    public void OnReadyButtonClick()
    {
        //check to see if all the players are ready
        foreach (PlayerCharacter p_player in GameManager.instance._players)
        {
            if (!p_player.IsReady)
                return;
        }
        //all the players are ready
        foreach (PlayerCharacter p_player in GameManager.instance._players)
            p_player.SpawnCharacter();
    }
#endregion

#region SetName
    [Command]
    public void CmdSetName(string p_newName)
    {
        RpcSetName(p_newName);
    }
    [ClientRpc]
    public void RpcSetName(string p_newName)
    {
        int __size = 50;

        if(p_newName.Length < __size)
            __size = p_newName.Length;

        gameObject.name = p_newName.Substring(0,__size);
    }
#endregion

#region GoToMenu
    [Command]
    public void CmdToMenu()
    {
        GameManager.instance.ToMenu();
    }

    [ClientRpc]
    private void RpcToMenu()
    {
        if (!isLocalPlayer) return;

        GameManager.instance.ReloadAllGame();
    }

    public void ToMenu()
    {
        RpcToMenu();
    }
#endregion
}
