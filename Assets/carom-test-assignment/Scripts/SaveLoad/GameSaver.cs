/* Created at 05 October 2019 by mria 🌊🐱 */
using System;
using System.IO;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    [SerializeField] GameLoader gameLoader;
    [SerializeField] ScoreTracker scoreTracker;
    [SerializeField] StringVariable playerName;
    [SerializeField] FloatVariable gameTime;
    [SerializeField] IntVariable strikeCount;

    string jsonPath = "Assets/Resources/data.json";

    void Start()
    {
        scoreTracker.onGameWon += SaveGame;
    }

    void SaveGame()
    {
        DataSaveFormat[] oldPlayersData = gameLoader.playerDataCollection.playersData;

        DataSaveFormat[] newPlayersData = GetNewPlayersDataArray(oldPlayersData);

        newPlayersData[0] = new DataSaveFormat { playerName = playerName.Value, gameTime = gameTime.Value, strikeCount = strikeCount.Value };
        
        gameLoader.playerDataCollection.playersData = newPlayersData;

        using (StreamWriter stream = new StreamWriter(jsonPath))
        {
            string json = JsonUtility.ToJson(gameLoader.playerDataCollection);
            stream.Write(json);
        }
    }

    DataSaveFormat[] GetNewPlayersDataArray(DataSaveFormat[] oldPlayersData)
    {
        if (oldPlayersData.Length < 8)
        {
            DataSaveFormat[] newPlayersData;
            newPlayersData = new DataSaveFormat[oldPlayersData.Length + 1];
            oldPlayersData.CopyTo(newPlayersData, 1);
            return newPlayersData;
        }
        else if (oldPlayersData.Length == 8)
        {
            DataSaveFormat[] newPlayersData;
            Array.Resize(ref oldPlayersData, oldPlayersData.Length - 1);
            newPlayersData = new DataSaveFormat[8];
            oldPlayersData.CopyTo(newPlayersData, 1);
            return newPlayersData;
        }
        else
        {
            Debug.LogError("Unexpected player data rray length", transform);
            return null;
        }
    }
}