using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject selfspeedPrefab;
	public GameObject enemySpeedPrefab;
    public GameObject diamondPrefab;

	private GameObject[] players;

	// Use this for initialization
	void Start ()
	{
		players = GameObject.FindGameObjectsWithTag ("Player");
		RespawnDiamond ();
		SpawnPowerups ();
	}
	
	// Update is called once per frame
	void Update () {
		checkIfSomeoneWon ();
	}
		
	float spawnTime = 4f;
	void SpawnPowerups()
	{
		float x = Random.Range (-3.9f, 8.8f);
		float y = Random.Range (-3.9f, 4.2f);
		int spawnType = Random.Range (0, 2);

		if (spawnType == 0) {
			Instantiate (selfspeedPrefab, new Vector2 (x, y), Quaternion.identity);
		} else if (spawnType == 1) {
			Instantiate (enemySpeedPrefab, new Vector2 (x, y), Quaternion.identity);
		}
			
		int spawnTime = Random.Range (2, 20);
		Invoke ("SpawnPowerups", spawnTime);
	}

	public void RespawnDiamond() {
		float x = Random.Range (-3.9f, 8.8f);
		float y = Random.Range (-3.9f, 4.2f);
       
        // diamond.transform.position = new Vector2 (x, y);
        Instantiate(diamondPrefab, new Vector2(x,y), Quaternion.identity);
	}
		
	void checkIfSomeoneWon() {
		foreach (GameObject player in players) {
			if (player.GetComponent<Player> ().killScore > 1) {
				Debug.Log ("You fucking won");
			}
		}
	}
}
