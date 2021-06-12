using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGLight : MonoBehaviour
{
    [SerializeField] private Light lightsource;
    [SerializeField] private Material mat;
    [Range(0,15)]
    [SerializeField] private float lightIntensity;
    [SerializeField] private float matintensity;
    [SerializeField] private BoolSO increase;
    [SerializeField] private BoolSO decrease;
    private Color color;
    void Start()
    {
        color = mat.color;
        InvokeRepeating(nameof(MatChangeColor), 0, 1);
    }
    private void MatChangeColor()
    {
        if (increase.boolean)
        {
            mat.color *= 2;
            increase.boolean = false;
            return;
        }
        if (decrease.boolean)
        {
            decrease.boolean = false;
            mat.color /= 2;
            return;
        }

        return;
    }
    void Update()
    {
        lightsource.intensity = lightIntensity;
    }
}