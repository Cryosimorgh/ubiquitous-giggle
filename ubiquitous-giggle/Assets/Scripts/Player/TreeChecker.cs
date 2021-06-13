using UnityEngine;

public class TreeChecker : MonoBehaviour
{
    [SerializeField] private BoolSO isTree;
    [SerializeField] private BoolSO isHoldingATree;
    [SerializeField] private Transform pos;
    private GameObject treeGameObject;
    void Start()
    {
        treeGameObject = null;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            treeGameObject = other.gameObject;
            isTree.boolean = true;
            if (isHoldingATree.boolean)
            {
                other.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
    void FixedUpdate()
    {
        if (isTree.boolean && isHoldingATree.boolean && treeGameObject != null)
        {
            treeGameObject.transform.position = pos.position;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            other.GetComponent<CapsuleCollider>().enabled = true;
            treeGameObject = null;
            isTree.boolean = false;
        }
    }
}
