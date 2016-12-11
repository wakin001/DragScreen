using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TestDualTouchControls : MonoBehaviour
{
	[SerializeField]
	private float m_speedScale = 5.0f;
	[SerializeField]
	private float m_turnSpeedScale = 45.0f;

	private float m_turnSpeed = 0.0f;


	void Update ()
	{
		float vertical = CrossPlatformInputManager.GetAxis ("Vertical");
		float horizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		Vector3 addPos = new Vector3 (horizontal, vertical, 0.0f) * m_speedScale;
		transform.position += addPos * Time.deltaTime;

		m_turnSpeed = CrossPlatformInputManager.GetAxis ("Mouse X") * m_turnSpeedScale;
		transform.rotation *= Quaternion.AngleAxis (m_turnSpeed * Time.deltaTime, Vector3.back);

		if (CrossPlatformInputManager.GetButton ("Jump")) {
			GetComponent<Renderer> ().material.color = Color.red;
		} else {
			GetComponent<Renderer> ().material.color = Color.white;
		}
	}

}
 // class Test