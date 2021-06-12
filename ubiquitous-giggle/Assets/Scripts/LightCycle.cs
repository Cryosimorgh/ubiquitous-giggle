using UnityEngine;
[RequireComponent(typeof(Light))]
public class LightCycle : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating(nameof(DayTimer), 0, 0.1f);
    }

    private void DayTimer()
    {
        transform.Rotate(0.15f, 0, 0);
    }
}
