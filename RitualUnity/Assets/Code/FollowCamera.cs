using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public Transform Target;
	public Vector3 Offset = Vector3.zero;

	public float Distance = 10.0f;
	public float Height = 5.0f;

	public float HeightDamping = 2.0f;
	public float RotationDamping = 3.0f;

	protected void LateUpdate () {
		if(!Target) return;

		float targetRotation = Target.eulerAngles.y;
		float targetHeight = Target.position.y + Height;

		float currentRotation = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Camera should not turn with player.
		//currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, RotationDamping * Time.deltaTime);
		currentRotation = Mathf.LerpAngle(currentRotation, 0, RotationDamping * Time.deltaTime);

		currentHeight = Mathf.Lerp(currentHeight, targetHeight, HeightDamping * Time.deltaTime);

		transform.position = Target.position;
		transform.position -= Quaternion.Euler(0, currentRotation, 0) * Vector3.forward * Distance;
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

		transform.LookAt(Target.transform.position + Offset);
	}
}
