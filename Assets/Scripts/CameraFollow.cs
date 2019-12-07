using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    private GameObject _player;
    private PlayerCharacter playerToFollow;

    private void Start() {
        GameManager.instance.onGameStarts += SetCameraPlayer;
    }

    public void SetCameraPlayer() {
        playerToFollow = GameManager.instance.GetLocalPlayerReference();
        _player = playerToFollow.gameObject;
    }

    private void LateUpdate () 
    {
        if (GameManager.instance.startGame) {
            if (_player.GetComponent<PlayerMovement>().alive) {
                transform.position = new Vector3(_player.transform.position.x + 36, transform.position.y, _player.transform.position.z - 30);
            }
        }
    }
}