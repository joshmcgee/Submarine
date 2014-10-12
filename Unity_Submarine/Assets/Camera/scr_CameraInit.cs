using UnityEngine;
using System.Collections;

public class scr_CameraInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        camera.orthographicSize = Screen.height / 2;
	}
}
