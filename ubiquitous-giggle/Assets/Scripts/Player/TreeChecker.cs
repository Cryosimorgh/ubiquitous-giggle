using UnityEngine;

public class TreeChecker : MonoBehaviour
{
    [SerializeField] private BoolSO isTree;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            isTree.boolean = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            isTree.boolean = false;
        }
    }
}
