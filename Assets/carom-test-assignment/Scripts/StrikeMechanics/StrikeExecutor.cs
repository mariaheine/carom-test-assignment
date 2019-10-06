using UnityEngine;

public class StrikeExecutor : MonoBehaviour
{   
    [SerializeField] CameraController cameraController;
    [SerializeField] CreateStrike createStrike;
    [SerializeField] StrikeRenderer strikeRenderer;
    [SerializeField] GameObject ReplayButton;

    static StrikeExecutor instance;
    StrikeResolver strikeResolver;
    Strike lastStrike;

    void Awake()
    {
        instance = this;
        strikeResolver = GetComponent<StrikeResolver>();
        ReplayButton.SetActive(false);
    }

    void Start()
    {
        createStrike.onStrike += strike => Execute(strike);
        strikeResolver.onResolved += ReturnToGame;
    }

    public void Execute(Strike strike)
    {
        Debug.Log("Execute", transform);

        lastStrike = strike;

        cameraController.RequestCamera(CameraController.CameraType.Watch);

        createStrike.enabled = false;
        strikeRenderer.enabled = false;
        ReplayButton.SetActive(false);

        strikeResolver.Resolve(StrikeResolver.ResolverType.Strike, lastStrike);
    }

    public void Replay()
    {
        Debug.Log("Replay", transform);

        cameraController.RequestCamera(CameraController.CameraType.Watch);
        createStrike.enabled = false;
        strikeRenderer.enabled = false;
        ReplayButton.SetActive(false);

        strikeResolver.Resolve(StrikeResolver.ResolverType.Replay, lastStrike);     
    }

    void ReturnToGame(StrikeResolver.ResolverType resolverType)
    {
        Debug.Log("Resolved", transform);
        
        cameraController.RequestCamera(CameraController.CameraType.Play);
        createStrike.enabled = true;
        strikeRenderer.enabled = true;
        ReplayButton.SetActive(true);
    }
}
