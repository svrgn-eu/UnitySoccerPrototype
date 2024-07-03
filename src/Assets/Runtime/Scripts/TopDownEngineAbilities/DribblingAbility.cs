using UnityEngine;
using System.Collections;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

namespace Runtime.Scripts.TopDownEngineAbilities // you might want to use your own namespace here
{
    /// <summary>
    /// TODO_DESCRIPTION
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/DribblingAbility")]
    public class DribblingAbility : CharacterAbility
    {
        /// This method is only used to display a helpbox text
        /// at the beginning of the ability's inspector
        public override string HelpBoxText() { return "This Ability is used by characters which shall dribble the ball."; }

        [Header("Specific Parameters")]
        /// declare your parameters here
        public GameObject Ball;
        public Collider2D PickupArea;
        public Transform PlayerTransform;
        public Vector2 BallOffset;
        public CharacterOrientation2D CharacterOrientation;

        protected const string _yourAbilityAnimationParameterName = "YourAnimationParameterName";
        protected int _yourAbilityAnimationParameter;

        private Collider2D ballCollider;
        private int directionFactor = 1;

        /// <summary>
        /// Here you should initialize our parameters
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();

            this.ballCollider = this.Ball.GetComponent<Collider2D>();
        }

        /// <summary>
        /// Every frame, we check if we're crouched and if we still should be
        /// </summary>
        public override void ProcessAbility()
        {
            base.ProcessAbility();

            if (this.PickupArea.IsTouching(this.ballCollider))
            {
                if ((!this._movement.CurrentState.Equals(CharacterStates.MovementStates.Dribbling)))
                {
                    // start dribbling
                    this._movement.ChangeState(CharacterStates.MovementStates.Dribbling);
                }
                else
                {
                    // we're already dribbling
                    // make sure the ball object is within the offset of the player object
                    Transform ballTransform = this.Ball.GetComponent<Transform>();
                    if (this.CharacterOrientation.CurrentFacingDirection.Equals(Character.FacingDirections.East))
                    {
                        // right
                        this.directionFactor = 1;
                    }
                    else if (this.CharacterOrientation.CurrentFacingDirection.Equals(Character.FacingDirections.West))
                    {
                        //left
                        this.directionFactor = -1;
                    }
                    ballTransform.position = this.PlayerTransform.position + new Vector3(this.BallOffset.x * directionFactor, this.BallOffset.y, 0);
                }
            }
            else
            {
                this._movement.ChangeState(CharacterStates.MovementStates.Walking);
            }
        }

        /// <summary>
        /// Called at the start of the ability's cycle, this is where you'll check for input
        /// </summary>
        protected override void HandleInput()
        {
            // here as an example we check if we're pressing down
            // on our main stick/direction pad/keyboard
            if (_inputManager.PrimaryMovement.y < -_inputManager.Threshold.y)
            {
                DoSomething();
            }
        }

        /// <summary>
        /// If we're pressing down, we check for a few conditions to see if we can perform our action
        /// </summary>
        protected virtual void DoSomething()
        {
            // if the ability is not permitted
            if (!AbilityPermitted
                // or if we're not in our normal stance
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal)
                // or if we're grounded
                || (!_controller.Grounded))
            {
                // we do nothing and exit
                return;
            }

            // if we're still here, we display a text log in the console
            MMDebug.DebugLogTime("We're doing something yay!");
        }

        /// <summary>
        /// Adds required animator parameters to the animator parameters list if they exist
        /// </summary>
        protected override void InitializeAnimatorParameters()
        {
            RegisterAnimatorParameter(_yourAbilityAnimationParameterName, AnimatorControllerParameterType.Bool, out _yourAbilityAnimationParameter);
        }

        /// <summary>
        /// At the end of the ability's cycle,
        /// we send our current crouching and crawling states to the animator
        /// </summary>
        public override void UpdateAnimator()
        {

            bool myCondition = true;
            MMAnimatorExtensions.UpdateAnimatorBool(_animator, _yourAbilityAnimationParameter, myCondition, _character._animatorParameters);
        }
    }
}