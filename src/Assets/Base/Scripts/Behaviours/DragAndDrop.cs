using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace NET.efilnukefesin.Unity.Base
{
    //from: https://www.youtube.com/watch?v=HfqRKy5oFDQ
    public class DragAndDrop : BaseBehaviour
    {
        #region Properties

        [SerializeField]
        private InputAction mouseClick;

        [SerializeField]
        private float MouseDragPhysicsSpeed = 10f;

        [SerializeField]
        [Range(0, 1)]
        private float MouseDragSpeed = .1f;

        public Camera MainCamera;

        private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        private Vector3 velocity = Vector3.zero;

        #endregion Properties

        #region Methods

        #region OnEnable
        private void OnEnable()
        {
            this.mouseClick.Enable();
            this.mouseClick.performed += this.MouseClick_performed;
        }
        #endregion OnEnable

        #region OnDisable
        private void OnDisable()
        {
            this.mouseClick.performed -= this.MouseClick_performed;
            this.mouseClick.Disable();
        }
        #endregion OnDisable

        #region MouseClick_performed
        private void MouseClick_performed(InputAction.CallbackContext obj)
        {
            //if the collider is a child of this behaviour, then use this.gameObject!
            GameObject dragTarget = null;

            Ray ray = this.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.IsChildOf(this.gameObject))
                    {
                        dragTarget = this.gameObject;
                    }
                }
            }
            else
            {
                // lets see if we have a 2D object here
                // https://answers.unity.com/questions/1087239/get-2d-collider-with-3d-ray.html
                RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
                if (hit2D.collider != null)
                {
                    if (hit2D.collider.gameObject.IsChildOf(this.gameObject))
                    {
                        dragTarget = this.gameObject;
                    }
                }
            }

            if (dragTarget != null)
            {
                this.StartCoroutine(this.DragUpdate(dragTarget));
            }
        }
        #endregion MouseClick_performed

        #region DragUpdate
        private IEnumerator DragUpdate(GameObject clickedObject)
        {
            float initialDistance = Vector3.Distance(clickedObject.transform.position, this.MainCamera.transform.position);
            clickedObject.TryGetComponent<Rigidbody>(out var rigidBody);
            while (this.mouseClick.ReadValue<float>() != 0)
            {
                Ray ray = this.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (rigidBody != null)
                {
                    //physics object
                    Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                    rigidBody.linearVelocity = direction * this.MouseDragPhysicsSpeed;
                    yield return this.waitForFixedUpdate;
                }
                else
                {
                    clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref this.velocity, this.MouseDragSpeed);
                    yield return null;
                }
            }
        }
        #endregion DragUpdate

        #endregion Methods

        #region Events

        #endregion Events
    }
}