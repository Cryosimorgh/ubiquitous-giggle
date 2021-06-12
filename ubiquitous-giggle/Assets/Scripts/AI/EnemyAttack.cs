using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private FloatSO playerHealth;
    [SerializeField] private FloatSO Damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.number -= Damage.number;
        }
    }
    void OnDisable()
    {
        return;
    }
}
