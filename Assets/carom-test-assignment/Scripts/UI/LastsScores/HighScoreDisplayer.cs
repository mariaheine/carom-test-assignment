/* Created at 05 October 2019 by mria 🌊🐱 */
using System.Collections.Generic;
using UnityEngine;

public class HighScoreDisplayer : MonoBehaviour
{
    [SerializeField] GameLoader gameLoader;
    [SerializeField] RectTransform layoutParent;
    [SerializeField] GameObject slotPrefab;

    List<GameObject> layoutChildren;

    void Awake()
    {
        layoutChildren = new List<GameObject>(8);
    }

    void OnEnable()
    {
        gameLoader.onDataLoaded += LoadHighScoreData;
    }

    void OnDisable()
    {
        gameLoader.onDataLoaded -= LoadHighScoreData;
    }

    void LoadHighScoreData()
    {
        var playersData = gameLoader.playerDataCollection.playersData;

        PreloadLayoutChildren(playersData.Length);

        if (playersData.Length != layoutChildren.Count)
            Debug.LogError("oops!");

        for (int i = 0; i < playersData.Length; i++)
        {
            var entry = playersData[i];
            layoutChildren[i].GetComponent<PlayerEntry>().SetDisplayer(entry.playerName, entry.gameTime, entry.strikeCount);
        }

        // foreach (var entry in playersData)
        // {
        //     // var listEntryPrefab = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
        //     listEntryPrefab
        //     listEntryPrefab.transform.SetParent(layoutParent, false);
        // }
    }

    void PreloadLayoutChildren(int count)
    {
        if (layoutChildren.Count == 0)
        {
            SpawnPrefabs(count);
        }
        else if (layoutChildren.Count == count) return;
        else if (layoutChildren.Count < count)
        {
            SpawnPrefabs(count - layoutChildren.Count);
        }
        else if (layoutChildren.Count > count)
        {
            // That shouldnt happen though
            DestroyPrefabs(layoutChildren.Count - count);
        }
    }

    void SpawnPrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var listEntryPrefab = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            listEntryPrefab.transform.SetParent(layoutParent, false);
            layoutChildren.Add(listEntryPrefab);
        }

        if (layoutChildren.Count > 8) Debug.LogError("Too many last scores layout children");
    }

    void DestroyPrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject toDestroy = layoutChildren[layoutChildren.Count - 1];
            layoutChildren.Remove(toDestroy);
            Destroy(toDestroy);
        }
    }
}