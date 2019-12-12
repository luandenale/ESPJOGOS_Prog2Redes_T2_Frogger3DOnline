using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ReadyButton : NetworkBehaviour
{
    private PlayerCharacter _localPlayer;
    [SerializeField] private RawImage _modelImage;
    [SerializeField] private InputField _playerName;
    [SerializeField] private Text _selectCharacterText;
    [SerializeField] private Text _enterNameText;


    public void SetReady()
    {
        if(_modelImage.texture.name == "EmptySelectionRenderer")
        {
            StartCoroutine(DisplaySelectCharacter());
        }
        else if (String.IsNullOrWhiteSpace(_playerName.text))
        {
            StartCoroutine(DisplayEnterName());
        }
        else
        {
            GetComponent<Button>().interactable = false;
            
            GameManager.instance.uiManager.WaitingOponent();
            _localPlayer = GameManager.instance.GetLocalPlayerReference();
            _localPlayer.CmdSetReady(true);
        }
    }    

    public void SetPlayerName(string newName)
    {
        _localPlayer = GameManager.instance.GetLocalPlayerReference();
        _localPlayer.CmdSetName(newName);
    }

    private IEnumerator DisplaySelectCharacter()
    {
        if(_selectCharacterText.color.a == 0)
        {
            _selectCharacterText.color = new Color(_selectCharacterText.color.r, _selectCharacterText.color.g, _selectCharacterText.color.b, 255);
            yield return new WaitForSeconds(2f);
            _selectCharacterText.color = new Color(_selectCharacterText.color.r, _selectCharacterText.color.g, _selectCharacterText.color.b, 0);
        }

        yield return null;
    }

    private IEnumerator DisplayEnterName()
    {
        if(_enterNameText.color.a == 0)
        {
            _enterNameText.color = new Color(_enterNameText.color.r, _enterNameText.color.g, _enterNameText.color.b, 255);
            yield return new WaitForSeconds(2f);
            _enterNameText.color = new Color(_enterNameText.color.r, _enterNameText.color.g, _enterNameText.color.b, 0);
        }

        yield return null;
    }
}
