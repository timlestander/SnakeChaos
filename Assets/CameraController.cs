using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float widthToBeSeen = 21f;  // Desired width 
	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {
		camera.orthographicSize = widthToBeSeen * Screen.height / Screen.width * 0.5f;
	}
}
