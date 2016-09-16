using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject selfspeedPrefab;

	// Use this for initialization
	void Start ()
	{
		GameObject temp2Player = (GameObject) Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Player player2 = temp2Player.GetComponentInChildren<Player> ();
		player2.setUp (KeyCode.A, KeyCode.D, KeyCode.Z);

		GameObject temp1Player = (GameObject) Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Player player1 = temp1Player.GetComponentInChildren<Player> ();
		player1.setUp (KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space);
	}
	
	// Update is called once per frame
	void Update () {
		SpawnPowerups ();
	}
		
	void SpawnPowerups()
	{
		float x = Random.Range (-10f, 10f);
		float y = Random.Range (-10f, 10f);
		float spawnTime = Random.Range (1, 100);

		if (spawnTime == 50) {
			Instantiate (selfspeedPrefab, new Vector2(x, y), Quaternion.identity);
		}
	}
			
}
