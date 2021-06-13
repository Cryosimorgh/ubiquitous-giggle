// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputManager/Base/InputMan.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMan : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMan()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMan"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""69df88f9-5fb0-46db-a22a-34449e654da4"",
            ""actions"": [
                {
                    ""name"": ""AD"",
                    ""type"": ""Value"",
                    ""id"": ""30a4a978-e1ba-414b-86b5-8f9e9510ab89"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""WS"",
                    ""type"": ""Value"",
                    ""id"": ""39cb86e9-4d4e-43df-b192-d7b7045d86ac"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""DoAction"",
                    ""type"": ""Button"",
                    ""id"": ""055ecd28-2dd4-4bbe-89d2-0b893c642207"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MousePosAction"",
                    ""type"": ""Value"",
                    ""id"": ""2041b297-9cbe-417c-ab58-41392b1da0c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""0b8bf2e7-336d-46b0-9f4f-63f77cff9fab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40ddf75e-3941-4a27-af10-cc484769be63"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""438a0f18-3a07-4d93-95ce-cee6767c1d0a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""470dcc45-9511-4557-9be4-0b7495cadd0c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b53551b-71b7-4819-a56a-b8d44787afdc"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""AD"",
                    ""id"": ""16bd8e1e-c01a-4d66-af55-afb832fa9bfe"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""159c1d5b-b556-4bd4-9b34-fa1a39b43914"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cafc4a78-289f-4406-92e2-800bc3c16cdb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WS"",
                    ""id"": ""09d638a2-fb85-4471-8554-f32a8acd5179"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WS"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1e5eafcd-ac2c-49a7-b595-0d0d2b92c1a8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2fccbd5e-358d-4397-b9ff-7b0f515c0492"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b282de50-fc71-4611-a838-a4abacc7e6ff"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_AD = m_Movement.FindAction("AD", throwIfNotFound: true);
        m_Movement_WS = m_Movement.FindAction("WS", throwIfNotFound: true);
        m_Movement_DoAction = m_Movement.FindAction("DoAction", throwIfNotFound: true);
        m_Movement_MousePosAction = m_Movement.FindAction("MousePosAction", throwIfNotFound: true);
        m_Movement_Pause = m_Movement.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_AD;
    private readonly InputAction m_Movement_WS;
    private readonly InputAction m_Movement_DoAction;
    private readonly InputAction m_Movement_MousePosAction;
    private readonly InputAction m_Movement_Pause;
    public struct MovementActions
    {
        private @InputMan m_Wrapper;
        public MovementActions(@InputMan wrapper) { m_Wrapper = wrapper; }
        public InputAction @AD => m_Wrapper.m_Movement_AD;
        public InputAction @WS => m_Wrapper.m_Movement_WS;
        public InputAction @DoAction => m_Wrapper.m_Movement_DoAction;
        public InputAction @MousePosAction => m_Wrapper.m_Movement_MousePosAction;
        public InputAction @Pause => m_Wrapper.m_Movement_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @AD.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnAD;
                @AD.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnAD;
                @AD.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnAD;
                @WS.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnWS;
                @WS.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnWS;
                @WS.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnWS;
                @DoAction.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnDoAction;
                @DoAction.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnDoAction;
                @DoAction.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnDoAction;
                @MousePosAction.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosAction;
                @MousePosAction.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosAction;
                @MousePosAction.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMousePosAction;
                @Pause.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AD.started += instance.OnAD;
                @AD.performed += instance.OnAD;
                @AD.canceled += instance.OnAD;
                @WS.started += instance.OnWS;
                @WS.performed += instance.OnWS;
                @WS.canceled += instance.OnWS;
                @DoAction.started += instance.OnDoAction;
                @DoAction.performed += instance.OnDoAction;
                @DoAction.canceled += instance.OnDoAction;
                @MousePosAction.started += instance.OnMousePosAction;
                @MousePosAction.performed += instance.OnMousePosAction;
                @MousePosAction.canceled += instance.OnMousePosAction;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnAD(InputAction.CallbackContext context);
        void OnWS(InputAction.CallbackContext context);
        void OnDoAction(InputAction.CallbackContext context);
        void OnMousePosAction(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
