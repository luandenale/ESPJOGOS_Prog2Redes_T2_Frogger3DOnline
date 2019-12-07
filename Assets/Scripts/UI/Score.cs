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
        UpdateText();
    }
    
    public void UpdateText() {
        points += pointPerSet;
        _text.text = "Score: " + points;
    }

    public void UpdateText(int deathPoint) {
        points += deathPoint;
        _text.text = "Score: " + points;
    }
}
