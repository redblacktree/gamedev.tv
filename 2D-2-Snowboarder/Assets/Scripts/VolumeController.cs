using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    Slider slider;

    void Awake() {
        slider = GetComponent<Slider>();
        //mixer.SetFloat("MasterVolume", Mathf.Log10(slider.value) * 20);
        mixer.SetFloat("MasterVolume", 0);
    }

    public void SetLevel(float sliderValue)
    {
        //mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}
