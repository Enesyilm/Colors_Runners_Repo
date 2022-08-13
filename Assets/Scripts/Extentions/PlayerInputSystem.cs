// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Extentions/PlayerInputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputSystem"",
    ""maps"": [
        {
            ""name"": ""Runner"",
            ""id"": ""a0d8e69b-cecc-41d2-a22d-4447d5d85a2c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f582461e-94e6-4fab-8df6-36fefdfbfc85"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""bafda0cb-db9a-4bda-b8e2-1a277f246f6f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fa227208-4c43-491d-a952-ee66ef64478f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9513ee13-5b54-47d3-b907-1c6793e93dbf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4f0e433e-984e-4484-af63-288bd0469a32"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ff1c3f3f-3b7b-41dd-874d-0a8918740acb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c94689c2-22f2-43d6-bd54-450132d4bc6f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""281eec36-1a67-4908-af0e-9083ea842977"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33e6df1b-05ca-4a58-aba6-19e1d9f69b4c"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Idle"",
            ""id"": ""f0c5fb19-fca3-486e-976a-3a028b4e08c5"",
            ""actions"": [
                {
                    ""name"": ""JoyStick"",
                    ""type"": ""Value"",
                    ""id"": ""abacc251-3e5d-4c9c-8806-de7077f690a5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4295c63a-6d3d-46e1-ae32-209ec765654e"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec3c1c2d-c6f5-4f85-9e8b-110cd848cfeb"",
                    ""path"": ""<AndroidJoystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f478029-e6ec-41d3-af95-f9308663bb0e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoyStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Runner
        m_Runner = asset.FindActionMap("Runner", throwIfNotFound: true);
        m_Runner_Move = m_Runner.FindAction("Move", throwIfNotFound: true);
        m_Runner_MouseDelta = m_Runner.FindAction("MouseDelta", throwIfNotFound: true);
        // Idle
        m_Idle = asset.FindActionMap("Idle", throwIfNotFound: true);
        m_Idle_JoyStick = m_Idle.FindAction("JoyStick", throwIfNotFound: true);
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

    // Runner
    private readonly InputActionMap m_Runner;
    private IRunnerActions m_RunnerActionsCallbackInterface;
    private readonly InputAction m_Runner_Move;
    private readonly InputAction m_Runner_MouseDelta;
    public struct RunnerActions
    {
        private @PlayerInputSystem m_Wrapper;
        public RunnerActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Runner_Move;
        public InputAction @MouseDelta => m_Wrapper.m_Runner_MouseDelta;
        public InputActionMap Get() { return m_Wrapper.m_Runner; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RunnerActions set) { return set.Get(); }
        public void SetCallbacks(IRunnerActions instance)
        {
            if (m_Wrapper.m_RunnerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMove;
                @MouseDelta.started -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.performed -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMouseDelta;
                @MouseDelta.canceled -= m_Wrapper.m_RunnerActionsCallbackInterface.OnMouseDelta;
            }
            m_Wrapper.m_RunnerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MouseDelta.started += instance.OnMouseDelta;
                @MouseDelta.performed += instance.OnMouseDelta;
                @MouseDelta.canceled += instance.OnMouseDelta;
            }
        }
    }
    public RunnerActions @Runner => new RunnerActions(this);

    // Idle
    private readonly InputActionMap m_Idle;
    private IIdleActions m_IdleActionsCallbackInterface;
    private readonly InputAction m_Idle_JoyStick;
    public struct IdleActions
    {
        private @PlayerInputSystem m_Wrapper;
        public IdleActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @JoyStick => m_Wrapper.m_Idle_JoyStick;
        public InputActionMap Get() { return m_Wrapper.m_Idle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(IdleActions set) { return set.Get(); }
        public void SetCallbacks(IIdleActions instance)
        {
            if (m_Wrapper.m_IdleActionsCallbackInterface != null)
            {
                @JoyStick.started -= m_Wrapper.m_IdleActionsCallbackInterface.OnJoyStick;
                @JoyStick.performed -= m_Wrapper.m_IdleActionsCallbackInterface.OnJoyStick;
                @JoyStick.canceled -= m_Wrapper.m_IdleActionsCallbackInterface.OnJoyStick;
            }
            m_Wrapper.m_IdleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @JoyStick.started += instance.OnJoyStick;
                @JoyStick.performed += instance.OnJoyStick;
                @JoyStick.canceled += instance.OnJoyStick;
            }
        }
    }
    public IdleActions @Idle => new IdleActions(this);
    public interface IRunnerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouseDelta(InputAction.CallbackContext context);
    }
    public interface IIdleActions
    {
        void OnJoyStick(InputAction.CallbackContext context);
    }
}
