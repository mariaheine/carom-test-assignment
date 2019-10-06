using UnityEngine;

public enum BallIdentity { White, Yellow, Red }

public class BallEntity : MonoBehaviour
{
    [SerializeField] BallIdentity identity;

    public BallIdentity Identity { get { return identity; } }

    void Awake()
    {
        if(gameObject.tag != "GameBall")
        {
            Debug.LogError("BallEntity component attached to incorrect GameObject not being a Game Ball", transform);
        }
    }
}
