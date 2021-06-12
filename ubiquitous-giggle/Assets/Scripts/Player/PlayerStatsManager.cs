using System;
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

    private void Start()
    {
        if (ArmListeners.Count > 0)
        {
            foreach(ArmColliderListener arm in ArmListeners)
            {
                arm.OnTriggerOverlap += this.OnArmAttackOverlap;
            }
        }
    }

    void Update()
    {
        if (playerhealth.number <= 0)
        {
            StartCoroutine(PlayerDied());
        }
    }

    private IEnumerator PlayerDied()
    {
        if (deathUIGameObject)
            deathUIGameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        if (deathUIGameObject)
            deathUIGameObject.SetActive(false);
        LoadScene(GetActiveScene().buildIndex);
    }

    public void RecieveDamage(float dmg)
    {
        playerhealth.number -= dmg;
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