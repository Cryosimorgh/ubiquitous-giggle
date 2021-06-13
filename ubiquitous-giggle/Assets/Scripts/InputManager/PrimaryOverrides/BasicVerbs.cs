using System;
using UnityEngine;
using static UnityEngine.Debug;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class BasicVerbs : InputSubscriber
{
    [SerializeField] private FloatSO speedMod;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anime;
    [SerializeField] private BoolSO isTree;
    [SerializeField] private BoolSO isHoldingATree;
    private Rigidbody rb;
    private Vector3 directions;
    private Vector3 previousPos;
    protected override void Start()
    {
        previousPos = transform.position;
        base.Start();
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(IsMoving), 0, 0.1f);
    }
    private void IsMoving()
    {
        if (previousPos.z != transform.position.z || previousPos.x != transform.position.x)
        {
            anime.SetBool("isMoving",true);
            previousPos = transform.position;
        }
        else
        {
            anime.SetBool("isMoving",false);
        }
    }
    protected override void ADAction(float directionx)
    {
        directions.x = directionx;
    }
    protected override void WSAction(float directionz)
    {
        directions.z = directionz;
    }
    protected override void InteractAction(bool performed)
    {
        int carryLayerIndex = anime.GetLayerIndex("CarryLayer");
        if (performed)
        {
            if (isTree.boolean)
            {
                // Set carry layer to 1, enabling carry anim
                if (anime)
                {
                    isHoldingATree.boolean = true;
                    anime.SetLayerWeight(carryLayerIndex, 1.0f);
                }
            }
            else
            {
                if (anime)
                {
                    anime.SetTrigger("onAttack");
                }
                return;
            }
        }
        else
        {
            //drop the loot
            isHoldingATree.boolean = false;

            // Set weight of Carry layer to 0, removing carry anim
            if (anime.GetLayerWeight(carryLayerIndex) > 0)
            {
                anime.SetLayerWeight(carryLayerIndex, 0.0f);
            }
        }
    }
    private void Move()
    {
        rb.velocity = (directions * speedMod.number);
        if (directions.x != 0 || directions.z != 0)
        {
            anime.Play("standing_run_forward");
        }
    }
    void FixedUpdate()
    {
        Move();
    }
}