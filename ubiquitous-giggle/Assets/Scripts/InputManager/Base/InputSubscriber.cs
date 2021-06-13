using System;
using UnityEngine;
public class InputSubscriber : MonoBehaviour
{
    private InputMan inputManagerScript;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        SubscribetheButtons();
        Time.timeScale = 1;
    }
    private void SubscribetheButtons()
    {
        inputManagerScript.Movement.AD.performed += ctx => ADAction(ctx.ReadValue<float>());
        inputManagerScript.Movement.WS.performed += ctx => WSAction(ctx.ReadValue<float>());
        inputManagerScript.Movement.DoAction.performed += ctx => InteractAction(ctx.ReadValueAsButton());
        inputManagerScript.Movement.Pause.performed += ctx => OnPauseButtonPressed(ctx.ReadValueAsButton());
    }
    protected virtual void OnPauseButtonPressed(bool v) {}
    protected virtual void ADAction(float direction) {}
    protected virtual void WSAction(float direction) {}
    protected virtual void InteractAction(bool performed) {}
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