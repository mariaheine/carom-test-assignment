/* Created at 06 October 2019 by mria 🌊🐱 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenText : MonoBehaviour
{
    [SerializeField] StringVariable playerName;
    [SerializeField] FloatVariable gameTime;
    [SerializeField] IntVariable strikeCount;
    [SerializeField] TextMeshProUGUI nameSlot;
    [SerializeField] TextMeshProUGUI summarySlot;

    void OnEnable()
    {
        nameSlot. text = "Congrats " + playerName.Value;
        summarySlot.text = $"You scored 3 points in {gameTime.Value.ToString()} seconds and {strikeCount.Value.ToString()} strikes"; 
    }

}