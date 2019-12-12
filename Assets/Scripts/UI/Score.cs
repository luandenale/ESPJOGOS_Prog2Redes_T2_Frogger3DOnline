using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public static int pointPerSet = 10;
    public static Score playerScore, enemyScore;
    private int _points;
    public int Points
    {
        get
        {
            return _points;
        }
    }

    public int lastPos;

    private Text _text;

    private void Awake()
    {
        _points = 0;
        _text = GetComponent<Text>();
        _text.text = null;
        GameManager.instance.onGameStarts += InitialStateText;
    }

    private void InitialStateText()
    {
        foreach (PlayerCharacter player in GameManager.instance._players)
        {
            if (player == GameManager.instance.GetLocalPlayerReference())
                playerScore._text.text = player.name + ": 0";
            else
                enemyScore._text.text = player.name + ": 0";
        }
    }
    
    public void UpdateText(string p_newName)
    {
        _points += pointPerSet;
        _text.text = p_newName + ": " + _points;
    }

    public void UpdateText(string p_newName, int p_deathPoint)
    {
        _points += p_deathPoint;
        _text.text = p_newName + ": " + _points;
    }
}
