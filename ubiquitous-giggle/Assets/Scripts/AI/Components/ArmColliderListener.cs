using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class ArmColliderListener : MonoBehaviour
{
    public string TagCompare;

    public System.Action<Collider> OnTriggerOverlap;

    private CapsuleCollider _capsule;

    // Start is called before the first frame update
    void Start()
    {
        _capsule = GetComponent<CapsuleCollider>();
        _capsule.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagCompare)
        {
            OnTriggerOverlap?.Invoke(other);
        }
    }
}
