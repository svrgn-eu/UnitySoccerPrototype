using UnityEngine;
using System.Collections;


public class SimpleRotation : MonoBehaviour {

	public float RotateSpeed = 200f;
	public Vector3 RotateAxis;

	private float m_currentAngle;

	private void Update() {
		m_currentAngle = Mathf.Repeat(m_currentAngle + RotateSpeed * Time.deltaTime, 360f);
		transform.rotation = Quaternion.AngleAxis(m_currentAngle, RotateAxis);
	}

}
