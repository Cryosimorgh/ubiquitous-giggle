using System;
using UnityEngine;
using static UnityEngine.Debug;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(BoxCollider))]
public class BasicVerbs : InputSubscriber
{
    [SerializeField] private FloatSO speedMod;
    [SerializeField] private Camera cam;
    private Rigidbody rb;
    private BoxCollider triggerCap;
    private Vector3 directions;
    private bool canPickUp;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        triggerCap = GetComponent<BoxCollider>();
    }
    protected override void ADAction(float directionx)
    {
        directions.x = directionx;
    }
    protected override void WSAction(float directionz)
    {
        directions.z = directionz;
    }
    protected override void DoAction(bool performed)
    {
        if (canPickUp)
        {
            Log("DoAction/Pick Up");
            return;
        }
        else
        {
            Log("Attack");
            return;
        }
    }
    protected override void MousePositionAction(Vector2 axis)
    {
    }
    private void Move()
    {
        rb.velocity = (directions * speedMod.number);
    }
    void FixedUpdate()
    {
        Move();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPickUp = true;
            //pickup
            Log("Pick Up");
            return;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canPickUp = false;
            //pickup
            Log("Pick Down");
            return;
        }
    }
}