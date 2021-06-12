using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Trees : MonoBehaviour
{
    [SerializeField] BoolSO isHoldingATree;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.useGravity = !isHoldingATree.boolean;
    }
}
