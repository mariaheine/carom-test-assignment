using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallsCollector : MonoBehaviour
{
    IDictionary<BallIdentity, GameObject> gameBalls;

    void Awake()
    {
        gameBalls = new Dictionary<BallIdentity, GameObject>();
    }

    void Start()
    {
        var balls = GameObject.FindObjectsOfType<BallEntity>();

        if (balls.Length != 3)
        {
            Debug.LogError("Invalid number of game balls identified (desired 3): " + balls.Length, transform);
        }

        foreach (var ball in balls)
        {
            gameBalls.Add(ball.Identity, ball.gameObject);
        }
    }

    public GameObject GetBall(BallIdentity identity)
    {
        return gameBalls[identity];
    }

    public int Count()
    {
        return gameBalls.Count;
    }

    public void Iterate(Action<BallIdentity, GameObject> action)
    {
        foreach (KeyValuePair<BallIdentity, GameObject> ballEntry in gameBalls)
        {
            action(ballEntry.Key, ballEntry.Value);
        }
    }
}
