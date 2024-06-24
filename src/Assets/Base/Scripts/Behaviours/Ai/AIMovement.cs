using NET.efilnukefesin.Unity.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NET.efilnukefesin.Unity.Base
{
    [RequireComponent(typeof(Animator))]  // for playing the animations
    [RequireComponent(typeof(PropertyList))]  // for acquiring the right values
    [RequireComponent(typeof(Movement))]  // for moving the object
    public class AIMovement : BaseBehaviour
    {
        #region Properties

        private Animator animator;
        private int Speed;
        private Vector2 movementVector = Vector2.zero;

        private Movement movement;

        #endregion Properties

        #region Methods

        #region Awake
        private void Awake()
        {
            this.animator = this.GetComponent<Animator>();
            this.movement = this.GetComponent<Movement>();
            this.Speed = this.GetComponent<PropertyList>().Speed;
        }
        #endregion Awake

        #region MoveLeft
        public void MoveLeft(bool ShouldStop = false)
        {
            //TODO: consider right vector of transform, currently static
            if (!ShouldStop)
            {
                if (this.movementVector.x != +1)
                {
                    this.taskService.ExecuteOnMainThread(() => {
                        this.animator.Play("MoveLeft");
                    });
                }

                this.movementVector.x = +1;
            }
            else if (ShouldStop)
            {
                this.movementVector.x = 0;  //TODO: Fade
                this.taskService.ExecuteOnMainThread(() => { 
                    this.animator.Play("StopMovingLeft"); 
                } );
            }
        }
        #endregion MoveLeft

        #region MoveRight
        public void MoveRight(bool ShouldStop = false)
        {
            if (!ShouldStop)
            {
                if (this.movementVector.x != -1)
                {
                    this.taskService.ExecuteOnMainThread(() => {
                        this.animator.Play("MoveRight");
                    });
                }

                this.movementVector.x = -1;
            }
            else if (ShouldStop)
            {
                this.movementVector.x = 0;  //TODO: Fade
                this.taskService.ExecuteOnMainThread(() => {
                    this.animator.Play("StopMovingRight");
                });
            }
        }
        #endregion MoveRight
        /*
        #region UpPressedPerformed
        public void UpPressedPerformed(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Started))
            {
                if (this.movementVector.y != +1)
                {
                    this.animator.Play("MoveUp");
                }

                this.movementVector.y = +1;
            }
            else if (context.phase.Equals(InputActionPhase.Canceled))
            {
                this.movementVector.y = 0;  //TODO: Fade
                this.animator.Play("StopMovingUp");
            }
        }
        #endregion UpPressedPerformed

        #region DownPressedPerformed
        public void DownPressedPerformed(InputAction.CallbackContext context)
        {
            if (context.phase.Equals(InputActionPhase.Started))
            {
                if (this.movementVector.y != -1)
                {
                    this.animator.Play("MoveDown");
                }

                this.movementVector.y = -1;
            }
            else if (context.phase.Equals(InputActionPhase.Canceled))
            {
                this.movementVector.y = 0;  //TODO: Fade
                this.animator.Play("StopMovingDown");
            }
        }
        #endregion DownPressedPerformed
        */
        #region Update
        private void Update()
        {
            this.movement.AddToDelta(new Vector3(this.movementVector.x * this.Speed, this.movementVector.y * this.Speed));
        }
        #endregion Update

        #endregion Methods
    }
}
