using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public GameObject playerScore, enemyScore;
    // Start is called before the first frame update
    void Start()
    {
        Score.playerScore = playerScore.GetComponent<Score>();
        Score.enemyScore = enemyScore.GetComponent<Score>();
    }
}
