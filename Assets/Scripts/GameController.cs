using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		GameObject temp1Player = (GameObject) Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Player player1 = temp1Player.GetComponentInChildren<Player> ();
		player1.setUp (KeyCode.A, KeyCode.B, KeyCode.C);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
