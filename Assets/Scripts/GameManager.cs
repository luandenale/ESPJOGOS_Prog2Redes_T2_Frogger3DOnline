using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static List<NetworkPlayerInstance> _players = new List<NetworkPlayerInstance>();
    public static bool startGame;
    // Start is called before the first frame update
    void Awake()
    {
        startGame = false;
    }
    // Roda apenas no servidor
    public static void RegisterPlayer(NetworkPlayerInstance player) {
        _players.Add(player);
        if (_players.Count == 2) {
            startGame = true;
        }
    }
}
