using UnityEngine;
using System.Collections;

public class scr_TouchscreenInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BoxCollider collider = transform.gameObject.GetComponent<BoxCollider>();
		collider.size = new Vector3(Screen.width, Screen.height, 10);
	}
}
