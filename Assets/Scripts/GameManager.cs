using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static List<NetworkPlayerInstance> _playerInstances = new List<NetworkPlayerInstance>();
    public List<PlayerCharacter> _players = new List<PlayerCharacter>();
    public bool startGame = false;
    public bool bothPlayersConnected = false;
    public bool localPlayerReady = false;
    public bool opponentReady = false;
    public bool bothPlayersStarted = false;
    public MenuUIManager uiManager;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    // Roda apenas no servidor
    public void RegisterPlayer(PlayerCharacter player)
    {
        _players.Add(player);
        if (_players.Count == 2) {
            bothPlayersConnected = true;
        }
    }

    private void Update()
    {
        if(bothPlayersConnected && bothPlayersStarted)
            startGame = true;
    }

    public PlayerCharacter GetLocalPlayerReference() {

        foreach (PlayerCharacter player in _players) {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer) {
                return player;
            }
        }
        return null;
    }
}
