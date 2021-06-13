using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensitySync : MonoBehaviour
{
    [SerializeField] private Light generatorsLight;
    private Light thislight;
    void Start()
    {
        thislight = GetComponent<Light>();
        InvokeRepeating(nameof(Setlightintensity), 0, 5);
    }

    private void Setlightintensity()
    {
        thislight.intensity = generatorsLight.intensity;
    }
}
