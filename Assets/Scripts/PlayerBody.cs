using UnityEngine;
using System.Collections;

public class PlayerBody : MonoBehaviour {

	private int myOrder;
	private Vector3 movementVelocity;
	[Range(0.0f,1.0f)]
	public float overTime = 0.02f;
	public Transform head;

	// Use this for initialization
	void Start () {
		// head = GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		for (int i = 0; i < head.GetComponent<Player> ().bodyParts.Count; i++) {
			if (gameObject == head.GetComponent<Player> ().bodyParts [i].gameObject) {
				myOrder = i;
			}
		}
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		if (myOrder == 0) {
			transform.position = Vector3.SmoothDamp (transform.position, head.position, ref movementVelocity, overTime);
		} else {
			transform.position = Vector3.SmoothDamp (transform.position, head.GetComponent<Player> ().bodyParts [myOrder - 1].position, ref movementVelocity, overTime);
		}
	} 
		
}
