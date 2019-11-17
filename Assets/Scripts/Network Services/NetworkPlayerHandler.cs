using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetworkPlayerHandler
{

    // private static BoardController _boardController;
    // public static GameStates _currentState = GameStates.MENU;

    private static List<NetworkPlayerInstance> _players = new List<NetworkPlayerInstance>();

    // Roda apenas no servidor
    public static void RegisterPlayer(NetworkPlayerInstance player)
    {
        _players.Add(player);
    }
    
}
