using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;
	public GameObject selfspeedPrefab;
	public GameObject enemySpeedPrefab;
	public GameObject selfSlowPrefab;
    public GameObject diamondPrefab;
	public GameObject enemySlowPrefab;

	public GameObject restartButton;
	public GameObject settingsButton;

	private GameObject[] players;
	private GameObject winPanel;
	private int spawnTime;

	// Use this for initialization
	// Use this for initialization
	void Start ()
	{
		restartButton.GetComponent<Button> ().onClick.AddListener (RestartGame);
		settingsButton.GetComponent<Button> ().onClick.AddListener (GoToSettings);
		winPanel = GameObject.Find ("WinPanel");
		winPanel.SetActive (false);
		players = GameObject.FindGameObjectsWithTag ("Player");
		RespawnDiamond ();
		spawnTime = Random.Range(0, 5);
		Invoke("SpawnPowerups", spawnTime);
	}

	// Update is called once per frame
	void Update () {

	}

	void SpawnPowerups()
	{
		float x = Random.Range (-3.9f, 8.8f);
		float y = Random.Range (-3.9f, 4.2f);
		int spawnType = Random.Range (0, 4);

		if (spawnType == 0) {
			Instantiate (selfspeedPrefab, new Vector2 (x, y), Quaternion.identity);
		} else if (spawnType == 1) {
			Instantiate (enemySpeedPrefab, new Vector2 (x, y), Quaternion.identity);
		} else if (spawnType == 2) {
			Instantiate (selfSlowPrefab, new Vector2 (x, y), Quaternion.identity);
		} else if (spawnType == 3) {
			Instantiate (enemySlowPrefab, new Vector2 (x, y), Quaternion.identity);
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
		
	public void ShowWinScreen(string winner, Color color) {
		winPanel.SetActive (true);
		winPanel.GetComponent<Image> ().color = color;
		GameObject.Find ("WinnerNameText").GetComponent<Text> ().text = "Good job " + winner;
		// Deactivate all players
		foreach (GameObject p in players) {
			p.SetActive (false);
		}
		/* foreach (GameObject player in players) {
			if (player.GetComponent<Player> ().timeScore > 50) {
				winPanel.SetActive (true);
				GameObject.Find ("WinnerNameText").GetComponent<Text> ().text = "Good job " + player.GetComponent<Player> ().playerName;
				// Deactivate all players
				foreach (GameObject p in players) {
					p.SetActive (false);
				}
			}
		} */
	}

	void GoToSettings() {
		/*
		foreach (GameObject player in players) {
			Destroy (player);
		}
		Application.LoadLevel ("Settings");
		*/ 
		Debug.Log ("GoToSettingsPlz");
	} 
		

	void RestartGame() {
		Debug.Log ("RESTART THIS");
	}
}
