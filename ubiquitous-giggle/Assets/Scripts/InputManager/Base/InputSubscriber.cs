using System;
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
        inputManagerScript.Movement.AD.performed += ctx => ADAction(ctx.ReadValue<float>());
        inputManagerScript.Movement.WS.performed += ctx => WSAction(ctx.ReadValue<float>());
        inputManagerScript.Movement.MousePosAction.performed += ctx => MousePositionAction(ctx.ReadValue<Vector2>());
        inputManagerScript.Movement.DoAction.performed += ctx => DoAction(ctx.ReadValueAsButton());
    }

    protected virtual void MousePositionAction(Vector2 axis) {}
    protected virtual void ADAction(float direction) {}
    protected virtual void WSAction(float direction) {}
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