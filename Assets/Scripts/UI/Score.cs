using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public static int pointPerSet = 10;
    public static Score playerScore, enemyScore;
    public int points;
    public int lastPos; //a posição mais avançada do jogador

    private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        _text = GetComponent<Text>();
        _text.text = null;
        GameManager.instance.onGameStarts += InicialStateText;
    }

    private void InicialStateText() {
        foreach (PlayerCharacter player in GameManager.instance._players) {
            if (player == GameManager.instance.GetLocalPlayerReference()) {
                playerScore._text.text = player.name + ": 0";
            }
            else {
                enemyScore._text.text = player.name + ": 0";
            }
        }
    }
    
    public void UpdateText(string newName) {
        points += pointPerSet;
        _text.text = newName + ": " + points;
    }

    public void UpdateText(string newName, int deathPoint) {
        points += deathPoint;
        _text.text = newName + ": " + points;
    }
}
