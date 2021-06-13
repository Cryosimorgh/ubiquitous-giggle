using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private float seconds;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private Transform[] spawnLocations;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(seconds);
        int index = Random.Range(0, spawnLocations.Length);
        Instantiate(treePrefab, spawnLocations[index].position, Quaternion.identity);
        seconds = Random.Range(5, 15);
        StartCoroutine(Spawn());
    }
}
