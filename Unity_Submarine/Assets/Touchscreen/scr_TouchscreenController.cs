using UnityEngine;
using System.Collections;

public class scr_TouchscreenController : MonoBehaviour {

	public Transform localPlayer;
	private scr_PlayerController playerController;

	private bool heldTap = false;

	private Vector3 mouseDownPosition;
	private Vector3 mouseUpPosition;
	private bool currentlyTapped = false;
	public float dragDeadzone = 10.0f;

	private Vector3 navPosition;

	// Use this for initialization
	void Start () {
		playerController = localPlayer.GetComponent<scr_PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentlyTapped) {

		}
	}

	void OnMouseDown() {
		Debug.Log("Tapped Down");
		currentlyTapped = true;
		mouseDownPosition = Input.mousePosition;
	}

	void OnMouseUp() {
		Debug.Log("Tap Released");
		currentlyTapped = false;
		mouseUpPosition = Input.mousePosition;

		float dragDistance = Vector3.Distance(mouseDownPosition, mouseUpPosition);
		//Debug.Log("dragDistance = " + dragDistance);
		if (dragDistance < dragDeadzone) {
			Debug.Log("Just a tap.");
			mouseUpPosition.z = transform.position.z;
			
			navPosition = Camera.main.ScreenToWorldPoint(mouseUpPosition);
			playerController.SetNavCoordinates(navPosition);
		}
		else {
			Debug.Log("Tap was dragged.");

			// Dragged vertical.
			if (Mathf.Abs(mouseUpPosition.x - mouseDownPosition.x) < (dragDistance/3)) {
				// Up
				if (mouseUpPosition.y - mouseDownPosition.y > 0.0f) {
					Debug.Log("Fire Torpedoes!");
				}
				// Down
				else {
					Debug.Log("Ping!");
				}
			}
			// Dragged horizontal.
			else if (Mathf.Abs(mouseUpPosition.y - mouseDownPosition.y) < (dragDistance/3)) {
				// Right
				if (mouseUpPosition.x - mouseDownPosition.x > 0.0f) {
					Debug.Log("Dragged Right!");
				}
				// Left
				else {
					Debug.Log("Shutdown Engines!");
					playerController.CutEngines();
				}
			}
		}
	}
}
