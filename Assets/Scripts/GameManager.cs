using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static List<NetworkPlayerInstance> _playerInstances = new List<NetworkPlayerInstance>();
    public static List<PlayerCharacter> _players = new List<PlayerCharacter>();
    public static bool startGame = false;
    public static bool bothPlayersConnected = false;
    public static bool localPlayerReady = false;
    public static bool opponentReady = false;
    public static bool bothPlayersStarted = false;

    // Roda apenas no servidor
    public static void RegisterPlayer(NetworkPlayerInstance p_player)
    {
        _playerInstances.Add(p_player);
        if (_playerInstances.Count == 2)
            bothPlayersConnected = true;
    }

    private void Update()
    {
        if(bothPlayersConnected && bothPlayersStarted)
            startGame = true;
    }
}
