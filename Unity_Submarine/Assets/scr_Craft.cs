using UnityEngine;
using System.Collections;

public class scr_Craft : MonoBehaviour {

	// Movement
	public float maxEnginePower = 1000.0f;
	private float currentEnginePower = 0.0f;

	public float topSpeed = 3.0f;
	private float currentSpeed = 0.0f;
	public float accelerationRate = 0.3f;
	public float rotationSpeed = 5.0f;

	private bool enginesAreActive = true;
	
	// Target
	private bool hasTarget = false;
	public Vector3 targetPosition;
	private float distanceToTarget = 0.0f;
	public float stopThreshold = 10.0f;


	void Update() {
		if (hasTarget) {
			Debug.Log("Drawing a line to target.");
			Debug.DrawLine(transform.position, targetPosition, Color.white);
		}

		if (enginesAreActive) {
			Debug.Log("Moving!");
			rigidbody.AddForce(Vector3.forward * (maxEnginePower * currentEnginePower));
		}
	}

	public void SetNavCoordinates (Vector2 coord2D) {
		targetPosition = new Vector3(coord2D.x, coord2D.y, transform.position.z);
		if (Vector3.Distance(transform.position, targetPosition) > stopThreshold) {
			Debug.Log("Set a course.");
			SetCourseForTarget();
		}
	}

	// Throttle = % of maxEnginePower to use.
	protected void SetThrottle (float throttle) {

		// Make sure it's 1 - 0.
		if (throttle > 1.0f) {
			throttle = 1.0f;
		}
		else if (throttle < 0.0f) {
			throttle = 0.0f;
		}

		// Set the current engine power, based on throttle.
		currentEnginePower = maxEnginePower * throttle;

		// Let everyone know the engines are on/off.
		if (currentEnginePower != 0.0f) {
			enginesAreActive = true;
		}
		else {
			enginesAreActive = false;
		}
	}
	
	protected void ComeTo () {
		// Apply reverse thrust to stop.
		SetThrottle(1.0f);
		currentEnginePower *= -1;
	}
	
	protected void SetCourseForTarget () {
		Debug.Log("Setting course.");
		hasTarget = true;
		SetThrottle(1.0f);
	}
}
