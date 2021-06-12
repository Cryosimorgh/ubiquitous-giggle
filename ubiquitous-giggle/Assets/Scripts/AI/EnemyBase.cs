using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All states this enemy uses
/// </summary>
enum ActionStates
{
    Idle = 0,
    Chasing = 1,
    Attack = 2,
    Death = 3,
}

/// <summary>
///  All Finite State Machine states to run during each ActionState
/// </summary>
enum FSMState
{
    Start = 0,
    Update = 1,
}

/// <summary>
/// Base class for enemies
/// </summary>
public class EnemyBase : MonoBehaviour
{
    // Health of the enemy
    private float _health;

    // Current action state of the enemy
    private ActionStates _currentState;
    // Current state of the FSM
    private FSMState _fsmState;

    #region MonoBehaviours

    void Start()
    {
        
    }

    void Update()
    {
        FSMUpdate();
    }

    #endregion

    private void FSMUpdate()
    {
        switch(_currentState)
        {
            case ActionStates.Idle:
                {
                    if (_fsmState == FSMState.Start) {
                        IdleStart();
                    } else if (_fsmState == FSMState.Update) {
                        IdleUpdate();
                    }
                    break;
                }
            case ActionStates.Attack:
                {
                    if (_fsmState == FSMState.Start) {
                        AttackStart();
                    } else if (_fsmState == FSMState.Update) {
                        AttackUpdate();
                    }
                    break;
                }
            case ActionStates.Chasing:
                {
                    if (_fsmState == FSMState.Start) {
                        ChasingStart();
                    } else if (_fsmState == FSMState.Update) {
                        ChasingUpdate();
                    }
                    break;
                }
            case ActionStates.Death:
                {
                    if (_fsmState == FSMState.Start) {
                        DeathStart();
                    } else if (_fsmState == FSMState.Update) {
                        DeathUpdate();
                    }
                    break;
                }
            default:
                Debug.LogError($"Unknown state '{_currentState}'");
                break;
        }
    }

    public float GetHealth()
    {
        return _health;
    }

    private void IdleStart()
    {

    }

    private void IdleUpdate()
    {

    }

    private void AttackStart()
    {

    }

    private void AttackUpdate()
    {

    }

    private void ChasingStart()
    {

    }

    private void ChasingUpdate()
    {

    }

    private void DeathStart()
    {

    }

    private void DeathUpdate()
    {

    }
}
