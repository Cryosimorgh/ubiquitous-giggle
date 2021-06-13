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
    [SerializeField] private FloatSO score;
    [SerializeField] private FloatSO highscore;

    public float SunSpeed = 1.0f;
    
    public event System.Action<LightCycles> OnCycleChange;

    private LightCycles _currentCycle;

    private float _repeatingTick = 0.05f;

    void Start()
    {
        score.number = 0;
        InvokeRepeating(nameof(SunRotator), 0, _repeatingTick);
        InvokeRepeating(nameof(DayNightFlipper), 0, 120f);
        SetNewCycle(LightCycles.Day);

        // Set sun rotation to Zero
        this.transform.eulerAngles = Vector3.zero;
    }

    private void DayNightFlipper()
    {
        ScoreHandlerF();
        if (_currentCycle == LightCycles.Day)
        {
            SetNewCycle(LightCycles.Night);
        }
        else
        {
            SetNewCycle(LightCycles.Day);
        }
    }

    private void ScoreHandlerF()
    {
        score.number += 0.5f;
        if (score.number >= highscore.number)
        {
            highscore.number = score.number;
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
