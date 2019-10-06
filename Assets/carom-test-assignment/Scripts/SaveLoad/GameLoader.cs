/* Created at 05 October 2019 by mria 🌊🐱 */
using UnityEngine;
using System.IO;
using System;

public class GameLoader : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;

    public Action onDataLoaded;
    public PlayerDataCollection playerDataCollection;

    string jsonPath = "Assets/Resources/data.json";

    void Awake()
    {
        gameStateManager.onMainMenuOpened += FetchSaveData;
    }

    public void FetchSaveData()
    {
        var data = Resources.Load<TextAsset>("data");

        using (StreamReader stream = new StreamReader(jsonPath))
        {
            string json = stream.ReadToEnd();
            playerDataCollection = JsonUtility.FromJson<PlayerDataCollection>(json);
        }

        if(onDataLoaded != null) onDataLoaded();
    }
}