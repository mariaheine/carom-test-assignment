using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;
    [SerializeField] StrikeResolver strikeResolver;
    [SerializeField] IntVariable playerScore;

    public Action onGameWon;

    void Start()
    {
        gameStateManager.onGameStarted += () => { playerScore.Value = 0; };
        strikeResolver.onPlayerScored += AwardPoint;
    }

    void AwardPoint()
    {
        playerScore.Value += 1;
        CheckScore();
    }

    void CheckScore()
    {
        if(playerScore.Value >= 3)
        {
            if(onGameWon != null) onGameWon();
        }
    }
}