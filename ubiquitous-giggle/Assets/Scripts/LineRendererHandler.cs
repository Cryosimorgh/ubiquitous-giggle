using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHandler : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    private LineRenderer lr;
    void Start()
    {
        if (!playerTransform)
        {
            Debug.LogError("Drag Player's Transform here");
        }
        lr = GetComponent<LineRenderer>();
    }
    void FixedUpdate()
    {
        Vector3 playerPosition = playerTransform.position + offset;
        lr.SetPosition(1, playerPosition);
    }

}
