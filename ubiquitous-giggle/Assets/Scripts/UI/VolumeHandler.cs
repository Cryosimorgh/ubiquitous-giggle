using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private FloatSO previousamount;
    private Slider slideramount;
    void Awake()
    {
        slideramount = GetComponent<Slider>();
        slideramount.value = previousamount.number;
    }
    public void VolumeF(float sliderValue)
    {
        mixer.SetFloat("Vol", Mathf.Log10(sliderValue) * 20);
    }
    void OnDestroy()
    {
        previousamount.number = slideramount.value;
    }
}