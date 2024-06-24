using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NET.efilnukefesin.Unity.Base
{
    public static class InputSystem
    {
        // from: https://forum.unity.com/threads/solved-two-playerinputs.762614/
        // to enable the viewmodel to have also player inputs
        public static PlayerInput Initialize(this PlayerInput input, string ControlScheme)
        {
            var instance = PlayerInput.Instantiate(input.gameObject, controlScheme: ControlScheme, pairWithDevices: new InputDevice[] { Keyboard.current, Mouse.current });

            instance.transform.parent = input.transform.parent;
            instance.transform.position = input.transform.position;

            GameObject.Destroy(input.gameObject);
            return instance;
        }
    }
}
