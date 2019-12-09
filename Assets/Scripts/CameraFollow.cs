using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    private GameObject _player;
    [SerializeField]
    private GameObject cameraText;
    private PlayerCharacter playerToFollow;
    private PlayerMovement playerMovement;
    private bool playerDead = false;

    private void Start() {
        GameManager.instance.onGameStarts += SetCameraPlayer;
    }

    public void SetCameraPlayer() {
        playerToFollow = GameManager.instance.GetLocalPlayerReference();
        _player = playerToFollow.gameObject;
        playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void LateUpdate () 
    {
        if (GameManager.instance.startGame) {
            if (playerMovement.alive || (!playerMovement.alive && playerDead)) {
                transform.position = new Vector3(transform.position.y, transform.position.y, _player.transform.position.z - 30);
            }
            else if (!playerMovement.alive && !cameraText.activeSelf && !GameManager.instance.GameEnded()) {
                cameraText.SetActive(true);
            }
            else if(!playerMovement.alive && !playerDead && !GameManager.instance.GameEnded()) {
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
            if (player != playerToFollow) {
                playerToFollow = player;
                _player = playerToFollow.gameObject;
                return;
            }
        }
    }

}