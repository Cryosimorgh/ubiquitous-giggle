using UnityEngine;
public class InputSubscriber : MonoBehaviour
{
    private InputMan inputManagerScript;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SubscribetheButtons();
    }
    private void SubscribetheButtons()
    {
        inputManagerScript.Movement.WASD.performed += wasdcontext => WASDPressed(wasdcontext.ReadValue<bool>());
        inputManagerScript.Movement.DoAction.performed += wasdcontext => DoAction(wasdcontext.ReadValue<bool>());
    }
    protected virtual void WASDPressed(Vector2 directions) {}
    protected virtual void DoAction(bool performed) {}
    void OnEnable()
    {
        SingleInstanceTheScript();
        inputManagerScript.Movement.Enable();
    }
    void OnDisable()
    {
        inputManagerScript.Movement.Disable();
    }
    private void SingleInstanceTheScript()
    {
        if (inputManagerScript is null)
        {
            inputManagerScript = new InputMan();
        }
    }
}