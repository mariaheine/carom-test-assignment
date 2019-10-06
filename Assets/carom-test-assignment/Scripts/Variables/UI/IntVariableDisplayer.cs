using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntVariableDisplayer : MonoBehaviour
{
    public IntVariable variable;

    int oldValue;
    TextMeshProUGUI textDisplayer;

    void Awake()
    {
        textDisplayer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(variable.Value != oldValue)
        {
            oldValue = variable.Value;
            textDisplayer.text = oldValue.ToString();
        }
    }
}
