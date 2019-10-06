using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameTimer : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;
    [SerializeField] FloatVariable gameTime;
    [SerializeField] StrikeResolver strikeResolver;

    TextMeshProUGUI textField;
    float startTime;
    bool counting = true;
    int savedTime = 0;

    void Awake()
    {
        textField = GetComponent<TextMeshProUGUI>();
    }


    void Start()
    {
        strikeResolver.onReplayStart += () =>
        {
            savedTime += (int)Mathf.Floor(Time.time - startTime);
            counting = false;
        };
        strikeResolver.onResolved += (resolverType) =>
        {
            if(resolverType != StrikeResolver.ResolverType.Replay) return;
            startTime = Time.time;
            counting = true;
        };
    }

    void Update()
    {
        if (!counting) return;
        gameTime.Value = savedTime + Time.time - startTime;
        textField.text = Mathf.Floor(gameTime.Value).ToString();
    }

    void OnEnable()
    {
        savedTime = 0;
        startTime = Time.time;
    }
}
