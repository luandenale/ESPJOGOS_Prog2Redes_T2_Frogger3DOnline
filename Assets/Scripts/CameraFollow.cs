using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    private GameObject _player;
    [SerializeField]
    private GameObject _cameraText;
    private PlayerCharacter _playerCharacter;
    private bool _playerDead = false;

    private void Awake()
    {
        GameManager.instance.onGameStarts += SetCameraPlayer;
    }

    public void SetCameraPlayer()
    {
        _playerCharacter = GameManager.instance.GetLocalPlayerReference();
        _player = _playerCharacter.gameObject;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
    }

    private void LateUpdate () 
    {
        if (GameManager.instance.startGame)
        {
            if (_playerCharacter.playerAlive || (!_playerCharacter.playerAlive && _playerDead))
                transform.position = new Vector3(transform.position.y, transform.position.y, _player.transform.position.z - 30);
            else if (!_playerCharacter.playerAlive && !_cameraText.activeSelf && !GameManager.instance.GameEnded())
                _cameraText.SetActive(true);
            else if(!_playerCharacter.playerAlive && !_playerDead && !GameManager.instance.GameEnded())
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _playerDead = true;
                    GetOpponentPlayer();
                    _cameraText.SetActive(false);
                }
            }
        }
    }

    private void GetOpponentPlayer()
    {
        foreach (PlayerCharacter p_player in GameManager.instance._players)
        {
            if (p_player != _playerCharacter)
            {
                _playerCharacter = p_player;
                _player = _playerCharacter.gameObject;
                return;
            }
        }
    }
}