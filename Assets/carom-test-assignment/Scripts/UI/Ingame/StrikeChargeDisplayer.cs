using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StrikeChargeDisplayer : MonoBehaviour
{
    [SerializeField] CreateStrike createStrike;
    [SerializeField] StrikeResolver strikeResolver;

    Image backgroundImage;
    Image chargeImage;

    void Start()
    {
        backgroundImage = GetComponentsInChildren<Image>()[0];
        chargeImage = GetComponentsInChildren<Image>()[1];
        createStrike.onStrikeCharge += DisplayStrikeCharge;
        createStrike.onStrike += ResetChargeDisplayer;
        strikeResolver.onStrikeStart += () => { backgroundImage.enabled = false; chargeImage.enabled = false; };
    }

    void DisplayStrikeCharge(float charge)
    {
        backgroundImage.enabled = true; 
        chargeImage.enabled = true;
        chargeImage.fillAmount = charge;
    }

    void ResetChargeDisplayer(Strike strike)
    {
        chargeImage.fillAmount = 0f;
    }
}
