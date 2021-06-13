using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;
public class PlayerStatsManager : MonoBehaviour
{
    public float MeleeAtkDamage = 5.0f;
    
    [SerializeField] private FloatSO playerhealth;
    [SerializeField] private GameObject deathUIGameObject;

    public List<ArmColliderListener> ArmListeners;

    // Main source of audio for the player
    [SerializeField] private AudioSource _audioSource;
    // Sound to play when player dies
    [SerializeField] private AudioClip _deathClip;
    // Sound to play when player is hurt, recieved damage
    [SerializeField] private AudioClip _hurtClip;

    private void Start()
    {
        playerhealth.number = 100f;
        if (ArmListeners.Count > 0)
        {
            foreach(ArmColliderListener arm in ArmListeners)
            {
                arm.OnTriggerOverlap += OnArmAttackOverlap;
            }
        }

        if (!_audioSource)
        {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (playerhealth.number <= 0)
        {
            playerhealth.number = 100f;
            StartCoroutine(PlayerDied());
        }
    }

    private IEnumerator PlayerDied()
    {
        if (deathUIGameObject)
        { 
            deathUIGameObject.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        if (deathUIGameObject)
        {
            deathUIGameObject.SetActive(false);
        }
        LoadScene(GetActiveScene().buildIndex);
        StopAllCoroutines();

        AudioHelper.PlayClipAtSource(_audioSource, _deathClip);
    }

    public void RecieveDamage(float dmg)
    {
        playerhealth.number -= dmg;

        AudioHelper.PlayClipAtSource(_audioSource, _hurtClip);
    }

    private void OnArmAttackOverlap(Collider other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy)
        {
            enemy.RecieveDamage(MeleeAtkDamage);
        }
    }
}