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
                    ""name"": ""WASD"",
                    ""type"": ""PassThrough"",
                    ""id"": ""30a4a978-e1ba-414b-86b5-8f9e9510ab89"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DoAction"",
                    ""type"": ""Button"",
                    ""id"": ""055ecd28-2dd4-4bbe-89d2-0b893c642207"",
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
                    ""name"": ""2D Vector"",
                    ""id"": ""1f5c333d-06ab-450c-84ce-c78fba00dcb0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c6d300bf-03e7-4063-b988-88a89ea8e379"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1dd3fcac-ab11-4d60-bc10-1ef1952e6203"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4050d742-77a2-4cba-bcc2-b3a4db17cd18"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b42e1553-b6ec-4056-af88-0caed926f9c4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WASD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_WASD = m_Movement.FindAction("WASD", throwIfNotFound: true);
        m_Movement_DoAction = m_Movement.FindAction("DoAction", throwIfNotFound: true);
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
    private readonly InputAction m_Movement_WASD;
    private readonly InputAction m_Movement_DoAction;
    public struct MovementActions
    {
        private @InputMan m_Wrapper;
        public MovementActions(@InputMan wrapper) { m_Wrapper = wrapper; }
        public InputAction @WASD => m_Wrapper.m_Movement_WASD;
        public InputAction @DoAction => m_Wrapper.m_Movement_DoAction;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @WASD.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnWASD;
                @WASD.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnWASD;
                @WASD.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnWASD;
                @DoAction.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnDoAction;
                @DoAction.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnDoAction;
                @DoAction.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnDoAction;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @WASD.started += instance.OnWASD;
                @WASD.performed += instance.OnWASD;
                @WASD.canceled += instance.OnWASD;
                @DoAction.started += instance.OnDoAction;
                @DoAction.performed += instance.OnDoAction;
                @DoAction.canceled += instance.OnDoAction;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnWASD(InputAction.CallbackContext context);
        void OnDoAction(InputAction.CallbackContext context);
    }
}
