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
    Retreat = 5,
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

    /// <summary>
    /// Amount of damage to deal to the target when performing melee atk
    /// </summary>
    public float MeleeAtkDamage;

    /// <summary>
    /// List of arm listener classes that are placed on enemy's swing joints
    /// </summary>
    public List<ArmColliderListener> ArmListeners;

    public delegate void OnDeath(EnemyBase enemy);
    public event OnDeath OnEnemyDeath;

    // Debug: set the target on start
    [SerializeField]
    private GameObject _debugTarget;

    // Animator of enemy model
    [SerializeField]
    private Animator _animator;

    // Prefab to drop below enemy as fuel
    [SerializeField]
    private GameObject _fuelPrefab;

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

    // Scene reference to main power generator player owns
    private PGLight _powerGenerator;

    // Is the enemy currently attacking the target?
    private bool bIsAttacking = false;

    private Vector3 _spawnLocation;

    public EnemyBase()
    {
        _health = 0;
        _currentActionState = ActionStates.Idle;
        _fsmState = FSMState.Start;

        _target = null;

        ChaseDetectionDist = 25;
        ChaseSpeed = 4;
        AttackDist = 2;
        DropPercentChance = 17;
        MeleeAtkDamage = 5.0f;
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

        if (ArmListeners.Count > 0)
        {
            foreach(ArmColliderListener arm in ArmListeners)
            {
                arm.OnTriggerOverlap += this.OnArmTriggerOverlapped;
            }
        }

        // Get reference to Player Generator in scene
        GameObject generator = GameObject.Find("Generator");
        if (generator != null)
        {
            _powerGenerator = generator.GetComponent<PGLight>();
            if (!_powerGenerator)
                Debug.LogError("Unable to find PowerGenerator in level!");
        }

        AttackFinishMessenger atkMessenger = this.transform.GetChild(0).GetComponent<AttackFinishMessenger>();
        if (atkMessenger != null)
        {
            atkMessenger.OnAttackFinished += this.OnAttackFinished;
        }

        // Provide random rotation on start
        this.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

        _spawnLocation = this.transform.position;
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
            case ActionStates.Retreat:
                {
                    if (_fsmState == FSMState.Start) {
                        RetreatStart();
                    } else if (_fsmState == FSMState.Update) {
                        RetreatUpdate();
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
        _fsmState = FSMState.Start;
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
            if (IsInRange(_target, ChaseDetectionDist))
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
            // If target is within atk dist, attack
            if (IsInRange(_target, AttackDist))
            {
                SetActionState(ActionStates.Attack);
                return;
            }

            // If enemy goes within radius of safety generator, stop chasing
            if (_powerGenerator && IsInRange(_powerGenerator.gameObject, _powerGenerator.GetRadiusAsInGameUnits()))
            {
                SetActionState(ActionStates.Retreat);
                return;
            }

            // Move towards target actor if no path or existing path is complete
            if (_nmAgent)
            {
                PathTowards(_target.transform.position);
            }
        }
    }

    private void AttackStart()
    {
        _fsmState = FSMState.Update;
        bIsAttacking = true;
    }

    private void AttackUpdate()
    {
        if (_target && !bIsAttacking)
        {
            // Validate distance between enemy and player to check still in range
            if (!IsInRange(_target, AttackDist))
            {
                SetActionState(ActionStates.Chasing);
            }
            else
            {
                // Restart isAttacking to enabled
                bIsAttacking = true;
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
            GameObject fuelGO = Instantiate(_fuelPrefab, this.transform.position, Quaternion.identity, null);
            fuelGO.transform.eulerAngles = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360));
        }

        if (_animator)
        {
            _animator.SetTrigger("onDeath");
        }

        // trigger death event if any listeners
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath.Invoke(this);
        }

        StartCoroutine(DelayAndRemove(5.0f));
    }

    private void DeathUpdate() { }

    private void RetreatStart()
    {
        _fsmState = FSMState.Update;

        // Retreat towards spawn
        PathTowards(_spawnLocation);
    }

    private void RetreatUpdate()
    {
        if (_target)
        {
            // Check if player copmes within range to chase them bk
            if (IsInRange(_spawnLocation, 10.0f))
            {
                SetActionState(ActionStates.Idle);
                return;
            }
        }
    }

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

    public void RecieveDamage(float dmgAmount)
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

    private void OnArmTriggerOverlapped(Collider otherCollider)
    {
        if (_target)
        {
            PlayerStatsManager playerStats = _target.GetComponent<PlayerStatsManager>();
            if (playerStats != null)
            {
                playerStats.RecieveDamage(MeleeAtkDamage);
            }
        }
    }

    /// <summary>
    /// Checks if the given game object is <= the given distance
    /// </summary>
    /// <param name="go"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    private bool IsInRange(GameObject go, float distance)
    {
        if (go == null)
            return false;

        return IsInRange(go.transform.position, distance);
    }

    private bool IsInRange(Vector3 position, float distance)
    {
        if (position == Vector3.zero|| distance < 0.0f)
            return false;

        float dist = Vector3.Distance(this.gameObject.transform.position, position);
        return dist <= distance;
    }

    private void OnAttackFinished()
    {
        // Once atk finished callback from messenger, set isAttacking to false
        bIsAttacking = false;
    }

    private bool PathTowards(Vector3 worldPosition)
    {
        bool success = NavMesh.CalculatePath(this.transform.position, worldPosition, NavMesh.AllAreas, _nmPath);
        if (!success)
        {
            //Debug.LogError("Unable to create path to target!");
            return false;
        }
        _nmAgent.SetPath(_nmPath);

        return true;
    }

    private IEnumerator DelayAndRemove(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(this);
    }
}
