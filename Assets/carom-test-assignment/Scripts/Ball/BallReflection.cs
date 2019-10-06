using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BallReflection : MonoBehaviour
{
    public float reflectionDampingFactor = 0.8f;
    public Action<BallIdentity, Vector3, float> onBallCollision;
    public Action<Vector3, float> onWallCollision;

    Rigidbody rb;
    BallEntity ballIdentity;
    Vector3 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ballIdentity = GetComponent<BallEntity>();
    }

    void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // I am not good with Unity physics, I am using velocity from a frame before
        // collision, because otherwise I am getting a velocity already altered by
        // unity collision/physics whatever system, which didnt want to work with any
        // kinds of Physics Materials and rigid body settings. Balls would just get stuck
        // over the Walls' surfaces.
        var reflectedVector = Vector3.Reflect(lastVelocity, collision.GetContact(0).normal);

        // Rather simplified physics behaviour
        if (collision.gameObject.tag == "TableWall")
        {
            if (onWallCollision != null)
            {
                onWallCollision(transform.position, rb.velocity.magnitude);
            }

            rb.velocity = reflectedVector * reflectionDampingFactor;

        }
        else if (collision.gameObject.tag == "GameBall")
        {
            if (rb.velocity.magnitude > collision.rigidbody.velocity.magnitude)
            {
                if (onBallCollision != null)
                {
                    onBallCollision(
                        collision.gameObject.GetComponent<BallEntity>().Identity,
                        transform.position,
                        rb.velocity.magnitude);
                }
                
                collision.rigidbody.velocity = -reflectedVector / 2;
                rb.velocity = reflectedVector / 2;
            }
            else return;
        }

    }
}
