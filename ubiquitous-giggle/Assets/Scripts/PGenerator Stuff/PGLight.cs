using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGLight : MonoBehaviour
{
    [SerializeField] private Light lightsource;
    [SerializeField] private FloatSO playerHealth;
    [SerializeField] private Material mat;
    [SerializeField] private BoolSO isTethered;
    [SerializeField] private BoolSO isTree;
    private float lightIntensity;
    private bool increase;
    private bool decrease;
    void Start()
    {
        lightIntensity = 12;
        mat.color = Color.red;
        InvokeRepeating(nameof(LightDecline), 0, 5f);
    }
    private void MatChangeColor()
    {
        if (lightIntensity == 0)
        {
            playerHealth.number = 0;
        }
        if (increase)
        {
            mat.color *= 6;
            lightIntensity += 3;
            increase = false;
            isTree.boolean = false;
            return;
        }
        if (decrease)
        {
            lightIntensity -= 1;
            decrease = false;
            mat.color /= 2;
            return;
        }
        return;
    }
    private void LightDecline()
    {
        decrease = true;
    }
    void Update()
    {
        MatChangeColor();
        lightsource.intensity = lightIntensity;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            Destroy(other.gameObject);
            increase = true;
        }
    }
}