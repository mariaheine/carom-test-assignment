using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StrikeRenderer : MonoBehaviour
{
    public PlayCamera cameraController;
    public Collider whiteBall;

    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        if (cameraController) cameraController.onCameraDirectionUpdate += UpdateStrikeRenderer;
    }

    void UpdateStrikeRenderer(Vector3 direction)
    {
        lineRenderer.SetPosition(0, whiteBall.bounds.center);

        Ray ray = new Ray(whiteBall.bounds.center, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 4f);
        Vector3 End = hit.point != null ? hit.point : whiteBall.bounds.center;
        lineRenderer.SetPosition(1, End);
    }

    void OnEnable()
    {
        lineRenderer.enabled = true;
    }

    void OnDisable()
    {
        lineRenderer.enabled = false;
    }
}
