using UnityEngine;
using System.Collections;

public class scr_PlayerController : scr_Craft {

	public float tappedStopDistance = 20.0f;

	public GameObject destinationCrosshair;

	// Use this for initialization
	void Start () {

		CreateCrosshair();
	}

	void CreateCrosshair () {
		GameObject newCrosshair = Instantiate(destinationCrosshair, 
		                                      transform.position,	
		                                      Quaternion.Euler(Vector3.zero)) as GameObject;
		//newCrosshair.renderer.material.color.a = 0.0f;

		//destinationCrosshair = newCrosshair;
	}

	//void FixedUpdate() {
		//rigidbody.AddForce(transform.forward * 4000 * Time.deltaTime);
		//Debug.Log("Sub velocity = " + rigidbody.velocity);
	//}
	
	// Update is called once per frame
	void Update () {
		CommonUpdate();
	}

	public void SetNavCoordinates (Vector2 coord2D) {
		targetPosition = new Vector3(coord2D.x, coord2D.y, transform.position.z);

		if (Vector3.Distance(transform.position, targetPosition) < tappedStopDistance) {
			Debug.Log("Hard Stop!");
			ComeTo ();
		}
		else {
			Debug.Log("Set a course. (Player)");
			PlaceDestinationCrosshair(coord2D);
			SetCourseForTarget();
		}
	}

	private void PlaceDestinationCrosshair (Vector2 coord2D) {
		Debug.Log("Moving the crosshairs to (" + coord2D.x + ", " + coord2D.y + ").");
		Vector3 position = new Vector3(coord2D.x, coord2D.y, transform.position.z);
		destinationCrosshair.transform.position = position;
	}
}
