using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour {

	public RectTransform scoreboardPanel;
	int[] scores;
	Text[] scoreObjects;
	RectTransform[] boostObjects;
	GameObject[] players;
	public GameObject scoreRow;
	public GameObject boostRow;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		boostObjects = new RectTransform[players.Length];
		scores = new int[players.Length];
		scoreObjects = new Text[players.Length];
		foreach (GameObject player in players) {
			Player tempPlayer = player.GetComponent<Player> ();
			AddPlayer (tempPlayer.playerName, tempPlayer.playerId, tempPlayer.playerColor);
		}
	}

	void OnGUI() {
		for (int x = 0; x < players.Length; x++) {
			scoreObjects [x].text = scores [x].ToString ();
			boostObjects [x].localScale = new Vector3 ((players [x].GetComponent<Player> ().boostCharge / 100), 1, 1);
		}

	}

	public void AddPlayer(string name, int index, Color color) {
		GameObject tempRow = Instantiate (scoreRow);
		GameObject tempBoost = Instantiate (boostRow);
		tempRow.transform.SetParent (scoreboardPanel);
		tempBoost.transform.SetParent (scoreboardPanel);
		tempRow.transform.GetChild (0).GetComponent<Text> ().text = name;
		tempRow.transform.GetChild (0).GetComponent<Text> ().color = color;
		tempRow.transform.GetChild (1).GetComponent<Text> ().text = "0";
		tempRow.transform.GetChild (1).GetComponent<Text> ().color = color;
		tempBoost.GetComponent<Image> ().color = color;

		scores[index] = 0;
		scoreObjects[index] = tempRow.transform.GetChild (1).GetComponent<Text>();
		boostObjects [index] = tempBoost.GetComponent<RectTransform> ();
	}
			
	public void UpdateScore(int index, int newScore) {
		scores [index] = newScore;
	}

}
