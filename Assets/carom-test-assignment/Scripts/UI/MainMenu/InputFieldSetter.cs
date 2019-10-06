using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldSetter : MonoBehaviour
{
    [SerializeField] StringVariable stringVariable;

    TMP_InputField inputField;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void UpdateVariable()
    {
        stringVariable.Value = inputField.text;
    }
}
