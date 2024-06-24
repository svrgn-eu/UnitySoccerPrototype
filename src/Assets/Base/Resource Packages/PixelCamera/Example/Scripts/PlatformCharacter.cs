using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlatformCharacter : MonoBehaviour {
	
	public string HorizontalAxis = "Horizontal";
	public string JumpButton = "Jump";

	public float Acceleration = 8f;
	public float Deceleration = 15f;
	public float MaxSpeed = 20f;

	public float JumpSpeed = 10f;
	public float Gravity = -20f;
	
	private float m_currentHorizontalSpeed;
	private float m_currentVerticalSpeed;

	private CharacterController m_controller;
	private bool m_isGrounded;

	private void Awake() {
		m_controller = GetComponent<CharacterController>();
	}

	private void Update() {
		float horizontal = Input.GetAxis(HorizontalAxis);
		var jump = Input.GetButtonDown(JumpButton);

		if(Mathf.Abs(horizontal) > 0.01f) {
			m_currentHorizontalSpeed += horizontal * Acceleration * Time.deltaTime;
		} else {
			m_currentHorizontalSpeed = Mathf.MoveTowards(m_currentHorizontalSpeed, 0f, Deceleration * Time.deltaTime);
		}
		m_currentHorizontalSpeed = Mathf.Clamp(m_currentHorizontalSpeed, -MaxSpeed, MaxSpeed);

		if(jump && m_isGrounded) {
			m_currentVerticalSpeed = JumpSpeed;
		}

		m_currentVerticalSpeed += Gravity * Time.deltaTime;

		Vector3 horizontalMovement = m_currentHorizontalSpeed * Time.deltaTime * Vector3.right;
		Vector3 verticalMovement = m_currentVerticalSpeed * Time.deltaTime * Vector3.up;
		m_controller.Move(horizontalMovement + verticalMovement);

		m_isGrounded = m_controller.isGrounded;
		if(m_isGrounded) {
			m_currentVerticalSpeed = 0f;
		}
	}
}
