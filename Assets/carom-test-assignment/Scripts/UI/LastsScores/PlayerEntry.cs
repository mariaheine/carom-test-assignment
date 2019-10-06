using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameSlot;
    [SerializeField] TextMeshProUGUI timeSlot;
    [SerializeField] TextMeshProUGUI strikeSlot;

    public void SetDisplayer(string name, float time, int strikes)
    {
        nameSlot.text = name;
        timeSlot.text = Mathf.Floor(time).ToString();
        strikeSlot.text = strikes.ToString();
    }
}
