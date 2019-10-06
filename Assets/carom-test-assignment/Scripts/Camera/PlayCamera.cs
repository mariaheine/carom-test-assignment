using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 3f;
    public float offsetY = 2;
    public float rotateSpeed = 20f;
    public Action<Vector3> onCameraDirectionUpdate;

    InputManagement input;
    float ySpeed = 100;
    float y, x;

    void Start()
    {
        input = InputManagement.Instance;

        var angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    void Update()
    {
        y += input.Rotation.x * ySpeed * rotateSpeed / 100 * Time.deltaTime;
        x += input.Rotation.y * ySpeed * rotateSpeed / 100 * Time.deltaTime;

        x = Mathf.Clamp(x, 19f, 90f);

        Quaternion rotation = Quaternion.Euler(x, y, 0);

        transform.rotation = rotation;

        Vector3 position = -transform.forward * distance + target.position;

        position.y += offsetY;

        transform.position = position;

        Vector3 tableFlatDirection = Quaternion.AngleAxis(-x, transform.right) * transform.forward;

        onCameraDirectionUpdate(tableFlatDirection);
    }
}