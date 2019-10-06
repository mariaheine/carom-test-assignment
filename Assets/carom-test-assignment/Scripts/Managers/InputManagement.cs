using UnityEngine;

public class InputManagement : MonoBehaviour
{
    public static InputManagement Instance { get { return instance; } }
    public Vector3 MouseAxis { get { return mouseAxis; } }
    public Vector2 Rotation { get { return rotation; } }
    public bool Strike { get { return strike; } }

    static InputManagement instance;
    Vector2 mouseAxis;
    Vector2 rotation;
    bool strike;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);

        mouseAxis = new Vector2(0f, 0f);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            strike = true;
        }
        else strike = false;

        mouseAxis.x = Input.GetAxis("Mouse Y");
        mouseAxis.y = Input.GetAxis("Mouse X");

        rotation.x = Input.GetAxis("Horizontal");
        rotation.y = Input.GetAxis("Vertical");
    }
}
