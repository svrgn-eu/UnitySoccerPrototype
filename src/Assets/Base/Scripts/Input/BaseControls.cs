// GENERATED AUTOMATICALLY FROM 'Assets/Base/Input/BaseControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace NET.efilnukefesin.Unity.Base
{
    public class @BaseControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @BaseControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""BaseControls"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""1ce81da4-fb2b-4c83-b951-82e170a7a506"",
            ""actions"": [
                {
                    ""name"": ""TogglePixelation"",
                    ""type"": ""Button"",
                    ""id"": ""45bc98e2-4696-44c5-a510-00e62638d4e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""acdc0086-4fd2-48b5-9c36-e0fd6d690f41"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Standard"",
                    ""action"": ""TogglePixelation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Standard"",
            ""bindingGroup"": ""Standard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Default
            m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
            m_Default_TogglePixelation = m_Default.FindAction("TogglePixelation", throwIfNotFound: true);
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

        // Default
        private readonly InputActionMap m_Default;
        private IDefaultActions m_DefaultActionsCallbackInterface;
        private readonly InputAction m_Default_TogglePixelation;
        public struct DefaultActions
        {
            private @BaseControls m_Wrapper;
            public DefaultActions(@BaseControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @TogglePixelation => m_Wrapper.m_Default_TogglePixelation;
            public InputActionMap Get() { return m_Wrapper.m_Default; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
            public void SetCallbacks(IDefaultActions instance)
            {
                if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
                {
                    @TogglePixelation.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnTogglePixelation;
                    @TogglePixelation.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnTogglePixelation;
                    @TogglePixelation.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnTogglePixelation;
                }
                m_Wrapper.m_DefaultActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TogglePixelation.started += instance.OnTogglePixelation;
                    @TogglePixelation.performed += instance.OnTogglePixelation;
                    @TogglePixelation.canceled += instance.OnTogglePixelation;
                }
            }
        }
        public DefaultActions @Default => new DefaultActions(this);
        private int m_StandardSchemeIndex = -1;
        public InputControlScheme StandardScheme
        {
            get
            {
                if (m_StandardSchemeIndex == -1) m_StandardSchemeIndex = asset.FindControlSchemeIndex("Standard");
                return asset.controlSchemes[m_StandardSchemeIndex];
            }
        }
        public interface IDefaultActions
        {
            void OnTogglePixelation(InputAction.CallbackContext context);
        }
    }
}
