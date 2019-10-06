using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Strike
{
    IDictionary<BallIdentity, Vector3> gameBalls;
    Vector3 strikeForce;
    public Vector3 StrikeForce { get { return strikeForce; } }

    public Strike(Vector3 force, BallEntity[] ballEntities)
    {
        this.strikeForce = force;
        this.gameBalls = new Dictionary<BallIdentity, Vector3>();

        foreach (var entity in ballEntities)
        {
            gameBalls.Add(entity.Identity, GetEntityPosition(entity));
        }
    }

    public Vector3 GetBallStartingPosition(BallIdentity identity)
    {
        Vector3 position = Vector3.zero;
        if(!gameBalls.TryGetValue(identity, out position))
        {
            Debug.LogError("Strike object is missing entry for identity: " + identity.ToString());
        }
        return position;
    }

    Vector3 GetEntityPosition(BallEntity entity)
    {
        return entity.gameObject.transform.position;
    }
}