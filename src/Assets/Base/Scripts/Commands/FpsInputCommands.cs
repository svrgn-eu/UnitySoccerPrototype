using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NET.efilnukefesin.Unity.Base.Commands
{
    public class FpsInputCommands : BaseBehaviour
    {
        #region Properties

        public PlayerInput Input;

        #region Forward
        public float Forward
        {
            get { return this.movementDelta.y; }
        }
        #endregion Forward

        #region Sideway
        public float Sideway
        {
            get { return this.movementDelta.x; }
        }
        #endregion Sideway

        #region IsAiming
        protected bool isAiming;
        public bool IsAiming
        {
            get { return this.isAiming; }
            //get { return true; }
        }
        #endregion IsAiming

        #region XLookAxis
        public float XLookAxis
        {
            get { return this.lookDelta.x * this.axisSensitivityX; }
        }
        #endregion XLookAxis

        #region YLookAxis
        public float YLookAxis
        {
            get { return this.lookDelta.y * this.axisSensitivityY; }
        }
        #endregion YLookAxis

        public bool IsInteracting { get; private set; }

        private Vector2 movementDelta;
        private Vector2 lookDelta;

        private float axisSensitivityX = 0.2f;
        private float axisSensitivityY = 0.2f;

        private bool mayMove = true;

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Move
        public void Move(InputAction.CallbackContext context)
        {
            if (this.mayMove)
            {
                this.movementDelta = context.ReadValue<Vector2>();
            }
        }
        #endregion Move

        #region Look
        public void Look(InputAction.CallbackContext context)
        {
            this.lookDelta = context.ReadValue<Vector2>();
        }
        #endregion Look

        #region Fire
        public void Fire(InputAction.CallbackContext context)
        {
            //throw new NotImplementedException();
        }
        #endregion Fire

        #region Aim
        public void Aim(InputAction.CallbackContext context)
        {
            this.isAiming = context.ReadValueAsButton();
        }
        #endregion Aim

        #region Interact
        public void Interact(InputAction.CallbackContext context)
        {
            this.IsInteracting = context.ReadValueAsButton();
        }
        #endregion Interact

        #region ChangeWeapon
        public void ChangeWeapon(InputAction.CallbackContext context)
        {
            //TODO: start weapon change - negative value means scrolling to the bottom
            var x = context.ReadValue<float>();
        }
        #endregion ChangeWeapon

        #region Holster
        public void Holster(InputAction.CallbackContext context)
        {
            //TODO: implement
            throw new NotImplementedException();
        }
        #endregion Holster

        #region DisableMovement
        public void DisableMovement()
        {
            this.mayMove = false;
        }
        #endregion DisableMovement

        #region EnableMovement
        public void EnableMovement()
        {
            this.mayMove = true;
        }
        #endregion EnableMovement

        #region GetInteractionMapping
        public string GetInteractionMapping()
        {
            string result = this.GetActionBinding("Interact", "Keyboard&Mouse");
            return result;
        }
        #endregion GetInteractionMapping

        #region GetActionBinding
        private string GetActionBinding(string ActionName, string SchemeName)
        {
            //TODO: move to base lib as extension or helper?
            string result = string.Empty;

            InputAction interactionAction = Input.actions.FindAction(ActionName);

            if (interactionAction != null)
            {
                result = interactionAction.GetBindingDisplayString(InputBinding.MaskByGroup(SchemeName));
            }
            return result;
        }
        #endregion GetActionBinding

        #endregion Methods

        #region Events

        #endregion Events
    }
}
