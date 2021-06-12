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
    KnockBack = 4,
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
    /// <summary>
    /// Percent between 0 and 100 to drop an item on death
    /// </summary>
    public float DropPercentChance;

    // Debug: set the target on start
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
    // Current path of the nav mesh agent
    private NavMeshPath _nmPath;

    public EnemyBase()
    {
        _health = 0;
        _currentActionState = ActionStates.Idle;
        _fsmState = FSMState.Start;

        _target = null;

        ChaseDetectionDist = 50;
        ChaseSpeed = 4;
        AttackDist = 2;
        DropPercentChance = 17;
    }

    #region MonoBehaviours

    protected virtual void Start()
    {
        if (!_nmAgent)
        {
            // Add NavMeshAgent and customize
            _nmAgent = this.gameObject.AddComponent<NavMeshAgent>();
            _nmAgent.stoppingDistance = AttackDist;
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
        
        // Update animator properties
        if (_animator)
        {
            _animator.SetFloat("movementVelocity", _nmAgent.velocity.sqrMagnitude);
            _animator.SetBool("bIsAttacking", _currentActionState == ActionStates.Attack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ToDo: Implement Player axe detection, remove health
        if (false)
        {
            // Get dmg amount for axe swing
            float dmgAmount = 0.0f;
            RecieveDamage(dmgAmount);

            float rndChance = Random.Range(0, 100);
            if (rndChance < 50) // 50% chance to be knocked back
            {
                Knockback();
            }
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
            case ActionStates.KnockBack:
                {
                    // Await anim callback to OnKnockbackFinish()
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

            // Move towards target actor if no path or existing path is complete
            if (_nmAgent && (!_nmAgent.hasPath || _nmAgent.hasPath && _nmAgent.pathStatus == NavMeshPathStatus.PathComplete))
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

        float rnd = Random.Range(0, 100);
        if (rnd <= DropPercentChance)
        {
            // Drop fuel/item on death
        }

        if (_animator)
        {
            _animator.SetTrigger("onDeath");
        }
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

    private void Knockback()
    {
        // Trigger animator trigger
        _animator.SetTrigger("onKnockback");
        // Set state to knockback
        SetActionState(ActionStates.KnockBack);
    }

    // Callback from Knockback animation - Done by animation event on animation
    public void OnKnockbackFinish()
    {
        // Once knockback finished, reset to chasing to continue normal FSM flow
        SetActionState(ActionStates.Chasing);
    }
}
