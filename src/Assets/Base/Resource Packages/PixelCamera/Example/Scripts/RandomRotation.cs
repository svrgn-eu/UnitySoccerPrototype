using UnityEngine;
using System.Collections;


public class RandomRotation : MonoBehaviour {

	public float RotateSpeed = 200f;
	public float ChangeAxisSpeed = 50f;
	public float TimeToChangeAxis = 3f;

	private Vector3 m_currentDirection;
	private Vector3 m_targetDirection;
	private float m_currentAngle;
	private float m_timeUntilAxisChange;

	private void Awake() {
		m_currentDirection = Random.onUnitSphere;
		m_timeUntilAxisChange = TimeToChangeAxis;
	}

	private void Update() {
		m_timeUntilAxisChange -= Time.deltaTime;
		if(m_timeUntilAxisChange <= 0) {
			m_targetDirection = Random.onUnitSphere;

			m_timeUntilAxisChange = TimeToChangeAxis;
		}

		m_currentDirection = Vector3.RotateTowards(m_currentDirection, m_targetDirection, 
		                                           ChangeAxisSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
		m_currentAngle = Mathf.Repeat(m_currentAngle + RotateSpeed * Time.deltaTime, 360f);
		transform.rotation = Quaternion.AngleAxis(m_currentAngle, m_currentDirection);
	}

}
