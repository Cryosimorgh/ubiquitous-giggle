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
    private Rigidbody rb;
    private Vector3 directions;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
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
        int carryLayerIndex = anime.GetLayerIndex("CarryLayer");

        if (isTree.boolean)
        {
            //do animation
            //add to carry pile
            //slow down the player

            // Set carry layer to 1, enabling carry anim
            if (anime)
            {
                anime.SetLayerWeight(carryLayerIndex, 1.0f);
            }

            Debug.Log("im Groot!");
        }
        else
        {

            Log("Attack");

            if (anime)
            {
                anime.SetTrigger("onAttack");
            }

            return;
        }
        if (performed == false)
        {
            //drop the loot

            // Set weight of Carry layer to 0, removing carry anim
            if (anime.GetLayerWeight(carryLayerIndex) > 0)
            {
                anime.SetLayerWeight(carryLayerIndex, 0.0f);
            }
        }
    }
    protected override void MousePositionAction(Vector2 axis)
    {
        // Covert mouse position to vector3 and set depth to get correct world location
        Vector3 mousePosVector3 = new Vector3(axis.x, axis.y, Camera.main.nearClipPlane);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosVector3);
        //Debug.Log($"mousePos = '{axis}' to worldPos = '{worldPosition}'");

        // Do LookAt world position and ignore X/Z axis'
        //this.transform.LookAt(worldPosition);
        //this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
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