using NET.efilnukefesin.Unity.Base.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace NET.efilnukefesin.Unity.Base.Interactions
{
    //TODO rebuild with backwards compatibility in mind!
    public class InteractionController : BaseBehaviour
    {
        #region Properties

        [Header("Interaction settings")]
        public float MaxDistance = 5f;
        public LayerMask InteractableLayers;
        public Camera FpsCamera;  //TODO: perhaps widen to GameObject? For NPCs as well?
        public Transform HeadLookAt;

        [Header("Input")]
        public FpsInputCommands Input;

        [Header("UI")]
        public Text InteractionText;
        //public Button InteractButton; //TODO: replace by keyboard input stuff, probably a text like "press F to interact" or even specify the interaction more

        private Interactable currentInteractable = default;
        private string lastInteractableName = string.Empty;
        private bool hasRecentlyBeenSetActive = false;

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Update
        private void Update()
        {
            Vector3 fromPosition = this.FpsCamera.transform.position;
            Vector3 toPosition = this.HeadLookAt.transform.position;
            Vector3 direction = toPosition - fromPosition;

            //Debug.DrawRay(this.FpsCamera.transform.position, direction * this.MaxDistance, Color.green);

            //TODO: use camera transform
            if (Physics.Raycast(this.FpsCamera.transform.position, direction.normalized, out RaycastHit hit, this.MaxDistance, this.InteractableLayers))
            {
                this.currentInteractable = hit.collider.transform.parent.gameObject.GetComponent<Interactable>();  //TODO: make simpler, we want to use the parent of the collider object in this case
            }
            else
            {
                this.currentInteractable = null;
            }

            this.InteractionText.gameObject.SetActive(this.currentInteractable != null);
            if (this.currentInteractable != null)
            {
                this.hasRecentlyBeenSetActive = !this.currentInteractable.name.Equals(this.lastInteractableName);
                if (this.hasRecentlyBeenSetActive)
                {
                    //TODO: get current key from input
                    this.InteractionText.text = $"'{this.Input.GetInteractionMapping()}' to {this.currentInteractable.Verb}";
                    this.hasRecentlyBeenSetActive = false;  //only pass one time
                }

                if (this.Input.IsInteracting)
                {
                    this.Interact();
                }

                this.lastInteractableName = this.currentInteractable.Name;
            }
            else
            {
                this.lastInteractableName = string.Empty;
                this.hasRecentlyBeenSetActive = false;
            }
        }
        #endregion Update

        #region Interact
        public void Interact()
        {
            if (this.currentInteractable != null)
            {
                this.currentInteractable.Interact();
            }
        }
        #endregion Interact

        #endregion Methods

        #region Events

        #endregion Events
    }
}
