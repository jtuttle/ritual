using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public Transform Target;
	public float Distance = 10.0f;
	public float Height = 5.0f;

	public float HeightDamping = 2.0f;
	public float RotationDamping = 3.0f;

	protected void LateUpdate () {
		if(!Target) return;

		// Calculate the current rotation angles
		float wantedRotationAngle = Target.eulerAngles.y;
		float wantedHeight = Target.position.y + Height;

		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, RotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		transform.position = Target.position;
		transform.position -= currentRotation * Vector3.forward * Distance;

		transform.position = new Vector3(transform.position.x,currentHeight,transform.position.z);

		// Always look at the target
		transform.LookAt(Target);
	}
}
