using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    private GameObject _player;
    [SerializeField]
    private GameObject cameraText;
    private PlayerCharacter _playerCharacter;
    private bool playerDead = false;

    private void Start() {
        GameManager.instance.onGameStarts += SetCameraPlayer;
    }

    public void SetCameraPlayer() {
        _playerCharacter = GameManager.instance.GetLocalPlayerReference();
        _player = _playerCharacter.gameObject;
        _playerCharacter = _player.GetComponent<PlayerCharacter>();
    }

    private void LateUpdate () 
    {
        if (GameManager.instance.startGame) {
            if (_playerCharacter.playerAlive || (!_playerCharacter.playerAlive && playerDead)) {
                transform.position = new Vector3(transform.position.y, transform.position.y, _player.transform.position.z - 30);
            }
            else if (!_playerCharacter.playerAlive && !cameraText.activeSelf && !GameManager.instance.GameEnded()) {
                cameraText.SetActive(true);
            }
            else if(!_playerCharacter.playerAlive && !playerDead && !GameManager.instance.GameEnded()) {
                if (Input.GetKey(KeyCode.Space)) {
                    playerDead = true;
                    GetOpponentPlayer();
                    cameraText.SetActive(false);
                }
            }
        }
    }

    private void GetOpponentPlayer() {
        foreach (PlayerCharacter player in GameManager.instance._players) {
            if (player != _playerCharacter) {
                _playerCharacter = player;
                _player = _playerCharacter.gameObject;
                return;
            }
        }
    }

}