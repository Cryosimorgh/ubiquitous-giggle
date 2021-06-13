using System;
using UnityEngine;

public enum LightCycles
{
    Day = 0,
    Night = 1,
}

[RequireComponent(typeof(Light))]
public class LightCycle : MonoBehaviour
{
    public float SunSpeed = 1.0f;
    
    public event System.Action<LightCycles> OnCycleChange;

    private LightCycles _currentCycle;

    private float _repeatingTick = 0.05f;

    void Start()
    {
        InvokeRepeating(nameof(SunRotator), 0, _repeatingTick);
        InvokeRepeating(nameof(DayNightFlipper), 0, 120f);
        SetNewCycle(LightCycles.Day);

        // Set sun rotation to Zero
        this.transform.eulerAngles = Vector3.zero;
    }

    private void DayNightFlipper()
    {
        if (_currentCycle == LightCycles.Day)
        {
            SetNewCycle(LightCycles.Night);
        }
        else
        {
            SetNewCycle(LightCycles.Day);
        }
    }

    private void SunRotator()
    {
        // 0.075f
        float rotationTick = SunSpeed * _repeatingTick;
        transform.Rotate(rotationTick, 0, 0);
    }

    /// <summary>
    /// Sets the cycle to a new state, triggering event and updating script
    /// </summary>
    /// <param name="newCycle"></param>
    private void SetNewCycle(LightCycles newCycle)
    {
        _currentCycle = newCycle;

        OnCycleChange?.Invoke(_currentCycle);

        Debug.Log($"New Cycle: '{_currentCycle}'");
    }
}
