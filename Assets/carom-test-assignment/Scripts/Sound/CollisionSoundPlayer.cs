/* Created at 05 October 2019 by mria 🌊🐱 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollisionSoundPlayer : MonoBehaviour
{
    [SerializeField] FloatVariable masterVolume;

    AudioSource audioSource;
    BallReflection[] ballReflections;

    void Awake()
    {
       audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
       ballReflections = GameObject.FindObjectsOfType<BallReflection>();
       foreach(var ball in ballReflections)
       {
           ball.onBallCollision += BallCollisionSound;
           ball.onWallCollision += WallCollisionSound;
       }
    }

    void BallCollisionSound(BallIdentity identity, Vector3 position, float velocityMagnitude)
    {
        audioSource.transform.position = position;
        audioSource.volume = masterVolume.Value * velocityMagnitude;
        audioSource.Play();
    }

    void WallCollisionSound(Vector3 position, float velocityMagnitude)
    {
        audioSource.transform.position = position;
        audioSource.volume = masterVolume.Value * velocityMagnitude;
        audioSource.Play();
    }

}