using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFinishMessenger : MonoBehaviour
{
    public event System.Action OnAttackFinished;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnAttackFinish()
    {
        if (OnAttackFinished != null)
        {
            OnAttackFinished.Invoke();
        }
    }
}
