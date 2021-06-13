using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PGLight : Singleton<PGLight>
{
    /// <summary>
    /// Radius of the safety circle around the generator
    /// </summary>
    public float SafetyRadius = 1.0f;

    [SerializeField] private Light lightsource;
    [SerializeField] private FloatSO playerHealth;
    [SerializeField] private Material mat;
    [SerializeField] private BoolSO isTethered;
    [SerializeField] private BoolSO isTree;
    [SerializeField] private Image _generatorRadiusImg;
    private float lightIntensity;
    private bool increase;
    private bool decrease;
    void Start()
    {
        lightIntensity = 12;
        mat.color = Color.red;
        InvokeRepeating(nameof(LightDecline), 0, 5f);

        // Update image size to set radius
        SetRadius(SafetyRadius);
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

    public void SetRadius(float newRadius)
    {
        SafetyRadius = newRadius;
        if (_generatorRadiusImg)
        {
            RectTransform rect = _generatorRadiusImg.GetComponent<RectTransform>();
            float newPixelSize = 100 * SafetyRadius;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newPixelSize);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newPixelSize);
        }
    }

    public float GetRadius()
    {
        return SafetyRadius;
    }

    public float GetRadiusAsInGameUnits()
    {
        // 12 in game units = 100 pixels for img
        return 12.0f * SafetyRadius;
    }
}