using UnityEngine;

public class IdleCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 XYRotationSpeed = new Vector2(100,100);
    [SerializeField] float distance = 10f;
    [SerializeField] float offsetY = 2;

    float x;
    float y;

    Vector2 rotationDirections = new Vector2(1f, 1f);

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    void Update()
    {
        if (target)
        {
            x += rotationDirections.x * XYRotationSpeed.x  * Time.deltaTime;
            y += rotationDirections.y * XYRotationSpeed.y  * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(x,y,0);

            transform.rotation = rotation;
            
            Vector3 position = -transform.forward * distance + target.position;

            position.y += offsetY;
            
            transform.position = position;
        }
    }
}
