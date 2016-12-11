using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TestCarTiltControls : MonoBehaviour
{
	[SerializeField]
	private float m_speedScale = 0.1f;
	[SerializeField]
	private float m_turnSpeedScale = 45.0f;

	private float m_moveSpeed = 0.0f;
	private float m_turnSpeed = 0.0f;

	void Update ()
	{
		m_moveSpeed += CrossPlatformInputManager.GetAxis ("Vertical") * m_speedScale;
		m_moveSpeed = Mathf.Max (m_moveSpeed, 0.0f);

		m_turnSpeed = CrossPlatformInputManager.GetAxis ("Mouse X") * m_turnSpeedScale;

		transform.position += transform.up * m_moveSpeed * Time.deltaTime;
		transform.rotation *= Quaternion.AngleAxis (m_turnSpeed * Time.deltaTime, Vector3.back);
	}


}
 // class Test