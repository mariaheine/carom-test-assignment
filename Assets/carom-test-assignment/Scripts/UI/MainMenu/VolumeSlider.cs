/* Created at 05 October 2019 by mria 🌊🐱 */
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] FloatVariable masterVolume;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateMasterVolume()
    {
        masterVolume.Value = slider.value;
    }
}