// GENERATED AUTOMATICALLY FROM 'Assets/MiniProjectsAssets/PlayerControlls/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""eebbda54-2ebe-4f12-8278-fca8683ab273"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""251353b1-366a-45b6-b6c9-bbc049bba67e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLoc"",
                    ""type"": ""PassThrough"",
                    ""id"": ""67f4bca1-db4b-4a04-a2df-5a48d1a62589"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""864ab081-2f61-4d96-bb4a-027448202ebb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OnRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""5b3dba35-7905-45a7-b898-e7ceb0ffe09d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""b7d64320-b5fb-44de-8175-10eb95176b73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6143ca61-d445-4a5f-a0bf-d3f889e46fc7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""7ed84758-7b73-49fa-942e-240c26866610"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4284a5cb-e854-4fbc-897a-f28c8a1dca9f"",
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
                    ""id"": ""b9f72550-fdf7-4298-aa06-91538c727668"",
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
                    ""id"": ""e3fdc463-605c-43ed-a791-77a4b74af7bb"",
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
                    ""id"": ""d5007062-9291-46af-98ae-011bb5dcae52"",
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
                    ""id"": ""63c50952-f51a-4ea9-9552-6fecf387d9c7"",
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
                    ""id"": ""3aa124b1-1340-41b2-9e4c-794bee078005"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLoc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60ab28f5-d75d-4740-a07c-28bb9d75a746"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bcf8442-8630-432d-b44d-7e2950d6747c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6e8c841-803e-41dc-b8ef-58c27326c30c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c153d36-24ba-44d3-a848-a21394c5ac81"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3889827-6960-4afa-a3d6-48f28b4e9b0f"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_MouseLoc = m_Gameplay.FindAction("MouseLoc", throwIfNotFound: true);
        m_Gameplay_OnLeftClick = m_Gameplay.FindAction("OnLeftClick", throwIfNotFound: true);
        m_Gameplay_OnRightClick = m_Gameplay.FindAction("OnRightClick", throwIfNotFound: true);
        m_Gameplay_Dodge = m_Gameplay.FindAction("Dodge", throwIfNotFound: true);
        m_Gameplay_MouseWheel = m_Gameplay.FindAction("MouseWheel", throwIfNotFound: true);
        m_Gameplay_Reload = m_Gameplay.FindAction("Reload", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_MouseLoc;
    private readonly InputAction m_Gameplay_OnLeftClick;
    private readonly InputAction m_Gameplay_OnRightClick;
    private readonly InputAction m_Gameplay_Dodge;
    private readonly InputAction m_Gameplay_MouseWheel;
    private readonly InputAction m_Gameplay_Reload;
    public struct GameplayActions
    {
        private @PlayerInputs m_Wrapper;
        public GameplayActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @MouseLoc => m_Wrapper.m_Gameplay_MouseLoc;
        public InputAction @OnLeftClick => m_Wrapper.m_Gameplay_OnLeftClick;
        public InputAction @OnRightClick => m_Wrapper.m_Gameplay_OnRightClick;
        public InputAction @Dodge => m_Wrapper.m_Gameplay_Dodge;
        public InputAction @MouseWheel => m_Wrapper.m_Gameplay_MouseWheel;
        public InputAction @Reload => m_Wrapper.m_Gameplay_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @MouseLoc.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLoc;
                @MouseLoc.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLoc;
                @MouseLoc.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLoc;
                @OnLeftClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOnLeftClick;
                @OnLeftClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOnLeftClick;
                @OnLeftClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOnLeftClick;
                @OnRightClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOnRightClick;
                @OnRightClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOnRightClick;
                @OnRightClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOnRightClick;
                @Dodge.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDodge;
                @MouseWheel.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseWheel;
                @Reload.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MouseLoc.started += instance.OnMouseLoc;
                @MouseLoc.performed += instance.OnMouseLoc;
                @MouseLoc.canceled += instance.OnMouseLoc;
                @OnLeftClick.started += instance.OnOnLeftClick;
                @OnLeftClick.performed += instance.OnOnLeftClick;
                @OnLeftClick.canceled += instance.OnOnLeftClick;
                @OnRightClick.started += instance.OnOnRightClick;
                @OnRightClick.performed += instance.OnOnRightClick;
                @OnRightClick.canceled += instance.OnOnRightClick;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @MouseWheel.started += instance.OnMouseWheel;
                @MouseWheel.performed += instance.OnMouseWheel;
                @MouseWheel.canceled += instance.OnMouseWheel;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouseLoc(InputAction.CallbackContext context);
        void OnOnLeftClick(InputAction.CallbackContext context);
        void OnOnRightClick(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
}
