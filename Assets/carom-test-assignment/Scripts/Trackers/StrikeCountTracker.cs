/* Created at 05 October 2019 by mria 🌊🐱 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeCountTracker : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;
    [SerializeField] CreateStrike createStrike;
    [SerializeField] IntVariable strikeCount;

    void Start()
    {
        gameStateManager.onGameStarted += () => { strikeCount.Value = 0; };
        createStrike.onStrike += CountStrike;
    }

    void CountStrike(Strike strike)
    {
        strikeCount.Value += 1;
    }
}