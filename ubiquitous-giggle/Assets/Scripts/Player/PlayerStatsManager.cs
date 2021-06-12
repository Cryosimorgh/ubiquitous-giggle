using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private FloatSO playerhealth;
    [SerializeField] private GameObject deathUIGameObject;

    void Update()
    {
        if (playerhealth.number <= 0)
        {
            StartCoroutine(PlayerDied());
        }
    }

    private IEnumerator PlayerDied()
    {
        deathUIGameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        deathUIGameObject.SetActive(false);
        LoadScene(GetActiveScene().buildIndex);
    }
}