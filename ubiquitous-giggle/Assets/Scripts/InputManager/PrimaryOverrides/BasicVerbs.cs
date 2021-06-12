using UnityEngine;
using static UnityEngine.Debug;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(BoxCollider))]
public class BasicVerbs : InputSubscriber
{
    [SerializeField] private FloatSO speedMod;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anime;
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
        // Covert mouse position to vector3 and set depth to get correct world location
        Vector3 mousePosVector3 = new Vector3(axis.x, axis.y, Camera.main.nearClipPlane);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosVector3);
        //Debug.Log($"mousePos = '{axis}' to worldPos = '{worldPosition}'");

        // Do LookAt world position and ignore X/Z axis'
        this.transform.LookAt(worldPosition);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
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