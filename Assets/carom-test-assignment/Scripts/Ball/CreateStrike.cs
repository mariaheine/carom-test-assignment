using System;
using UnityEngine;

public class CreateStrike : MonoBehaviour
{
    [SerializeField] PlayCamera playCamera;
    [SerializeField] float maxStrikeChargeTime = 3f;

    public Action<float> onStrikeCharge;
    public Action<Strike> onStrike;

    Rigidbody rb;
    InputManagement input;
    Vector3 strikeDirection;
    BallEntity[] gameBalls;
    float strikeCharge;
    // int chargeDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = InputManagement.Instance;
        gameBalls = GameObject.FindObjectsOfType<BallEntity>();
        if (playCamera) playCamera.onCameraDirectionUpdate += dir => strikeDirection = dir;
    }

    void Update()
    {
        if (input.Strike)
        {
            ChargeStrike();
        }

        if(!input.Strike && strikeCharge != 0f)
        {            
            Vector3 force = strikeDirection * strikeCharge;
            Strike newStrike = new Strike(force, gameBalls);
            strikeCharge = 0f;
            if(onStrike != null) onStrike(newStrike);
        }
    }

    void ChargeStrike()
    {
        if (strikeCharge <= maxStrikeChargeTime)
        {
            strikeCharge += Time.deltaTime;
            var powerScale = strikeCharge / maxStrikeChargeTime;
            if(onStrikeCharge != null) onStrikeCharge(powerScale);
        }
    }
}
