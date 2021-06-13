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
        InvokeRepeating(nameof(DayTimer), 0, _repeatingTick);
        SetNewCycle(LightCycles.Day);

        // Set sun rotation to Zero
        this.transform.eulerAngles = Vector3.zero;
    }

    private void DayTimer()
    {
        // 0.075f
        float rotationTick = SunSpeed * _repeatingTick;
        transform.Rotate(rotationTick, 0, 0);

        // ToDo
        float angle = 45;
        if (angle == 0.0f || angle == 180.0f)
        {
            // X just pased 180, gone night time
            if (transform.eulerAngles.x > 180.0f && _currentCycle == LightCycles.Day)
            {
                SetNewCycle(LightCycles.Night);
            }
            // X just passed 0, start of new day
            else if (transform.eulerAngles.x > 0.0f && _currentCycle == LightCycles.Night)
            {
                SetNewCycle(LightCycles.Day);
            }
        }
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
