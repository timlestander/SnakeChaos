using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject selfspeedPrefab;
	public GameObject enemySpeedPrefab;
	public GameObject diamondPrefab;

	// Use this for initialization
	void Start ()
	{
		GameObject temp2Player = (GameObject) Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Player player2 = temp2Player.GetComponentInChildren<Player> ();
		player2.setUp (KeyCode.A, KeyCode.S, KeyCode.Z, Color.green, 0);

		GameObject temp1Player = (GameObject) Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Player player1 = temp1Player.GetComponentInChildren<Player> ();
		player1.setUp (KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, Color.red, 1);

		GameObject temp3Player = (GameObject) Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
		Player player3 = temp3Player.GetComponentInChildren<Player> ();
		player3.setUp (KeyCode.G, KeyCode.H, KeyCode.B, Color.cyan, 2);

		RespawnDiamond ();
	}
	
	// Update is called once per frame
	void Update () {
		SpawnPowerups ();
	}
		
	void SpawnPowerups()
	{
		float x = Random.Range (-3.9f, 8.8f);
		float y = Random.Range (-3.9f, 4.2f);
		float spawnTime = Random.Range (0, 400);

		if (spawnTime == 200) {

			int spawnType = Random.Range (0, 2);

			if (spawnType == 0) {
				Instantiate (selfspeedPrefab, new Vector2(x, y), Quaternion.identity);
			} else if (spawnType == 1) {
				Instantiate (enemySpeedPrefab, new Vector2(x, y), Quaternion.identity);
			}
		}
	}

	public void RespawnDiamond() {
		float x = Random.Range (-3.9f, 8.8f);
		float y = Random.Range (-3.9f, 4.2f);

		// diamond.transform.position = new Vector2 (x, y);
		Instantiate(diamondPrefab, new Vector2(x,y), Quaternion.identity);
	}
		
}
