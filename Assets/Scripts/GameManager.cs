using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<PlayerCharacter> _players = new List<PlayerCharacter>();
    public static bool startGame;
    // Start is called before the first frame update
    void Awake()
    {
        startGame = false;
    }
    // Roda apenas no servidor
    public static void RegisterPlayer(PlayerCharacter player) {
        _players.Add(player);
    }
}
