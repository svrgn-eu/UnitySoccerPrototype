using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleObject : MonoBehaviour {

	public GameObject Target;
	public KeyCode KeyToToggle;

	public Text Label;

	private string m_templateText;

	private void Awake() {
		m_templateText = Label.text;
	}

	private void Update () {
		if(Input.GetKeyDown(KeyToToggle)) {
			Target.SetActive(!Target.activeSelf);
		}

		Label.text = string.Format(m_templateText, 
		                           Target.activeSelf ? "ON" : "OFF",
		                           KeyToToggle.ToString());
	}
}
