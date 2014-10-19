using UnityEngine;
using System.Collections;

public class scr_Craft : MonoBehaviour {

	// Movement
	public float maxEnginePower = 1000.0f;
	private float currentEnginePower = 0.0f;

	//public float topSpeed = 3.0f;
	//private float currentSpeed = 0.0f;
	//public float accelerationRate = 0.3f;
	//public float rotationSpeed = 5.0f;

	private bool enginesAreActive = false;
	
	// Target
	private bool hasTarget = false;
	public Vector3 targetPosition;
	private float distanceToTarget = 0.0f;
	public float stopThreshold = 10.0f;

	// Debug
	public bool showVelocity = false;
	


	void FixedUpdate() {

		// This alternates between true and false. I don't know why.
		//Debug.Log("enginesAreActive == " + enginesAreActive);

		if (enginesAreActive) {
			// Apply forward force to the craft.
			rigidbody.AddForce(transform.forward * (maxEnginePower * currentEnginePower));
		}
	}

	void Update () {

		// Debug
		if (showVelocity) {
			Debug.Log("Craft Velocity = " + rigidbody.velocity.magnitude);
		}

		if (hasTarget) {
			// Draw a line to the target.
			Debug.DrawLine(transform.position, targetPosition, Color.white);
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

		Debug.Log("Setting throttle to " + throttle);

		// Set the current engine power, based on throttle.
		currentEnginePower = maxEnginePower * throttle;

		// Let everyone know the engines are on/off.
		if (currentEnginePower == 0.0f) {
			enginesAreActive = false;
		}
		else {
			enginesAreActive = true;
		}
	}
	
	protected void ComeTo () {
		// Apply reverse thrust to stop.
		SetThrottle(1.0f);
		currentEnginePower *= -1;

		// TODO: Make sure to cut the engines once the velocity is zero.
	}

	public void CutEngines () {
		Debug.Log("Cutting Engines!");
		SetThrottle(0.0f);
	}
	
	protected void SetCourseForTarget () {
		Debug.Log("Setting course.");
		hasTarget = true;
		SetThrottle(1.0f);
	}
}