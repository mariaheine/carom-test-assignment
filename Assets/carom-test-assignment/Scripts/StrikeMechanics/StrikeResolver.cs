using System;
using System.Collections;
using UnityEngine;

public class StrikeResolver : MonoBehaviour
{
    [SerializeField] IntVariable playerScore;
    [SerializeField] BallsCollector ballsCollector;

    public Action onPlayerScored;
    public Action onStrikeStart;
    public Action onReplayStart;
    public Action<ResolverType> onResolved;

    bool isResolving = false;

    public enum ResolverType { Strike, Replay }
    ResolverType resolverType;

    struct HitTracker
    {
        public bool hitYellow;
        public bool hitRed;
    }
    HitTracker hitTracker;

    void Update()
    {
        if (isResolving)
        {
            GameObject whiteBall = ballsCollector.GetBall(BallIdentity.White);
            Rigidbody whiteBallRB = whiteBall.GetComponent<Rigidbody>();
            int stoppedBalls = 0;

            ballsCollector.Iterate((identity, ball) =>
            {
                var rb = ball.GetComponent<Rigidbody>();

                if (rb.velocity.magnitude < 0.01f)
                {
                    stoppedBalls += 1;
                    rb.velocity = Vector3.zero;
                }
            });

            if (stoppedBalls == 3)
            {
                isResolving = false;
                if (resolverType == ResolverType.Strike)
                {
                    if (CheckScore()) onPlayerScored();
                }

                if (onResolved != null) onResolved(resolverType);
            }
        }
    }

    public void Resolve(ResolverType type, Strike strike)
    {
        resolverType = type;

        GameObject whiteBall = ballsCollector.GetBall(BallIdentity.White);
        Rigidbody whiteBallRB = whiteBall.GetComponent<Rigidbody>();

        if (type == ResolverType.Strike)
        {
            hitTracker = new HitTracker();
            var whiteBallReflection = whiteBall.GetComponent<BallReflection>();
            whiteBallReflection.onBallCollision += TrackScore;
            onResolved += (resolverType) => { whiteBallReflection.onBallCollision -= TrackScore; };

            if (onStrikeStart != null) onStrikeStart();

        }
        else if (type == ResolverType.Replay)
        {
            Time.timeScale = 0.8f;
            ballsCollector.Iterate((identity, ball) =>
            {
                ball.transform.position = strike.GetBallStartingPosition(identity);
            });

            if (onReplayStart != null) onReplayStart();
        }

        whiteBallRB.AddForce(strike.StrikeForce, ForceMode.Impulse);

        StartCoroutine(WaitForForceToAddUp());
    }

    IEnumerator WaitForForceToAddUp()
    {
        yield return new WaitForFixedUpdate();

        isResolving = true;
    }

    void TrackScore(BallIdentity identity, Vector3 position, float magnitude)
    {
        if (identity == BallIdentity.Yellow) hitTracker.hitYellow = true;
        if (identity == BallIdentity.Red) hitTracker.hitRed = true;
    }

    bool CheckScore()
    {
        if (hitTracker.hitRed == true && hitTracker.hitYellow == true) return true;
        else return false;
    }

}
