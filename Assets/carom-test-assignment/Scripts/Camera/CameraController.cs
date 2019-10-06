using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] List<GameCamera> Cameras;

    [System.Serializable]
    public struct GameCamera
    {
        public CameraType Type;
        public GameObject GO;
    }

    public enum CameraType { Play, Watch, Idle }

    void Awake()
    {
        RequestCamera(CameraType.Play);
    }

    public void RequestCamera(CameraType type)
    {
        foreach(var cam in Cameras)
        {
            if(cam.Type == type) cam.GO.SetActive(true);
            else cam.GO.SetActive(false);
        }
    }
}
