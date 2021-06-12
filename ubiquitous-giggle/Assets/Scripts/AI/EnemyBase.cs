using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    /// <summary>
    /// Detection distance to start chasing the target
    /// </summary>
    public float ChaseDetectionDist;
    /// <summary>
    ///  Speed of enemy when chasing after the player
    /// </summary>
    public float ChaseSpeed;
    /// <summary>
    /// Distance enemy need to be <= to perform an attack
    /// </summary>
    public float AttackDist;

    [SerializeField]
    private GameObject _debugTarget;

    // Animator of enemy model
    [SerializeField]
    private Animator _animator;

    // Health of the enemy
    private float _health;
    // Target game object (actor) to perform at
    private GameObject _target;

    // Current action state of the enemy
    private ActionStates _currentActionState;
    // Current state of the FSM
    private FSMState _fsmState;
    // Rigidbody of the enemy
    private NavMeshAgent _nmAgent;
    private NavMeshPath _nmPath;

    public EnemyBase()
    {
        _health = 0;
        _currentActionState = ActionStates.Idle;
        _fsmState = FSMState.Start;

        _target = null;

        ChaseDetectionDist = 50;
        AttackDist = 5;
    }

    #region MonoBehaviours

    protected virtual void Start()
    {
        if (!_nmAgent)
        {
            // Add NavMeshAgent and customize
            _nmAgent = this.gameObject.AddComponent<NavMeshAgent>();
            _nmAgent.stoppingDistance = 4.0f;
            _nmAgent.radius = 0.50f;
            _nmAgent.speed = ChaseSpeed;
        }
        if (_nmPath == null)
        {
            _nmPath = new NavMeshPath();
        }

        if (!_animator)
        {
            Debug.LogError($"No animator set for enemy character '{this.name}'");
        }

        if (_debugTarget)
        {
            SetTarget(_debugTarget);
        }
    }

    protected virtual void Update()
    {
        FSMUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ToDo: Implement Player axe detection, remove health
        if (false)
        {
            // Get dmg amount for axe swing
            float dmgAmount = 0.0f;
            RecieveDamage(dmgAmount);
        }
    }

    #endregion

    private void FSMUpdate()
    {
        switch(_currentActionState)
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
                Debug.LogError($"Unknown state '{_currentActionState}'");
                break;
        }
    }

    /// <summary>
    ///  Set the current action state of the FSM
    /// </summary>
    /// <param name="newState">New state to transition to</param>
    private void SetActionState(ActionStates newState)
    {
        _currentActionState = newState;
    }

    private void IdleStart()
    {
        _fsmState = FSMState.Update;
    }

    private void IdleUpdate()
    {
        if (_target)
        {
            // Check if target is within detection radius to chase
            float dist = Vector3.Distance(this.transform.position, _target.transform.position);
            if (dist <= ChaseDetectionDist)
            {
                SetActionState(ActionStates.Chasing);
            }
        }
    }

    private void ChasingStart()
    {
        _fsmState = FSMState.Update;
    }

    private void ChasingUpdate()
    {
        if (_target)
        {
            float dist = Vector3.Distance(this.transform.position, _target.transform.position);
            if (dist <= AttackDist)
            {
                SetActionState(ActionStates.Attack);
            }

            // Move towards target actor
            if (_nmAgent && !_nmAgent.hasPath)
            {
                bool success = NavMesh.CalculatePath(this.transform.position, _target.transform.position, NavMesh.AllAreas, _nmPath);
                if (!success)
                {
                    Debug.LogError("Unable to create path to target!");
                    return;
                }
                _nmAgent.SetPath(_nmPath);
            }
        }
    }

    private void AttackStart()
    {
        _fsmState = FSMState.Update;
    }

    private void AttackUpdate()
    {
        if (_target)
        {
            if (false) //!isAttacking
            {

            }

            // Validate distance between enemy and player to check still in range
            float dist = Vector3.Distance(this.transform.position, _target.transform.position);
            if (dist > AttackDist)
            {
                SetActionState(ActionStates.Chasing);
            }
        }
    }

    private void DeathStart()
    {
        Debug.Log($"Enemy '{this.name}' died!");
        _fsmState = FSMState.Update;
    }

    private void DeathUpdate() { }

    /// <summary>
    /// Gets the current amount of health (hp) the enemy has
    /// </summary>
    /// <returns></returns>
    public float GetHealth()
    {
        return _health;
    }

    private void SetHealth(float newHealth)
    {
        _health = newHealth;
    }

    /// <summary>
    /// Sets the target the enemy should perform towards
    /// </summary>
    /// <param name="targetObject"></param>
    public void SetTarget(GameObject targetObject)
    {
        _target = targetObject;
    }

    /// <summary>
    /// Gets the current target the enemy performs actions to
    /// </summary>
    /// <returns></returns>
    public GameObject GetTarget()
    {
        return _target;
    }

    private void RecieveDamage(float dmgAmount)
    {
        // Check if enemy dies from this amount
        float currentHp = GetHealth();
        if (currentHp - dmgAmount <= 0)
        {
            SetActionState(ActionStates.Death);
        }
        else
        {
            float newHp = currentHp - dmgAmount;
            SetHealth(newHp);

            Debug.Log($"Enemy '{this.name}' recieved '{dmgAmount}' dmg (New health: '{newHp}')");
        }
    }
}
