using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private Score _playerScore;
    [SerializeField]
    private Score _enemyScore;
    
    
    private void Awake()
    {
        Score.playerScore = _playerScore;
        Score.enemyScore = _enemyScore;
    }
}
