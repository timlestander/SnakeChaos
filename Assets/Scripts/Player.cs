using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform> ();
	public Transform bodypart;
	public float rotationSensitivity = 50.0f;
	public float speed = 3.5f;
	public float boostCharge = 100f;

	bool powerupTriggered = false;
	bool boostTriggered = false;

	KeyCode leftTurn;
	KeyCode rightTurn;
	KeyCode boostKey;

	public Color playerColor;
	public int playerId; 

	[HideInInspector]
	public int timeScore;
	public int killScore;
	public string playerName;

	bool isIt = false;

	public void setUp(string name, KeyCode left, KeyCode right, KeyCode boost, string color, int id) {
		playerName = name;
		leftTurn = left;
		rightTurn = right;
		boostKey = boost;
		playerColor = getPlayerColor (color);
		playerId = id;
	}

	// Use this for initialization
	void Start () {
		SpawnPlayer ();
		InvokeRepeating ("IncreaseScore", 1.0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (leftTurn)) {
			currentRotation += rotationSensitivity * Time.deltaTime;
		}

		if (Input.GetKey (rightTurn)) {
			currentRotation -= rotationSensitivity * Time.deltaTime;
		}

		if (powerupTriggered == false)
		{
			if (Input.GetKey (boostKey) && boostCharge > 1) {
				speed = 7.0f;
				boostCharge = boostCharge - 2f;
				boostTriggered = true;
			}

			if (Input.GetKeyUp (boostKey) || boostCharge < 1) {
				speed = 3.5f;
			}

		}

		if (boostCharge < 99) {
			boostCharge = boostCharge + 0.1f;
		}
	}

	public int respawnTime = 2;
	public int powerupTime = 3;
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag ("Wall")) {
			KillPlayer ();
		} else if (other.gameObject.CompareTag ("selfSpeed")) {
			activateSelfSpeed ();
			Invoke ("deactivateSelfSpeed", powerupTime);
			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("selfSlow")) {
			activateSlowSpeed ();
			Invoke ("deactivateSlowSpeed", powerupTime);
			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("enemySpeed")) {
			activateEnemySpeed ();
			Invoke ("deactivateEnemySpeed", powerupTime);
			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("enemySlow")) {
			activateEnemySlow ();
			Invoke ("deactivateEnemySlow", powerupTime);
			Destroy (other.gameObject);
		} else if (other.gameObject.CompareTag ("Diamond")) {
			isIt = true;
			StartGlowing ();
			other.gameObject.SetActive (false);
		} else if (other.gameObject.CompareTag("Bodypart")) {
			if (!bodyParts.Contains (other.transform)) {
				if (isIt) {
					GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
					foreach (GameObject player in players) {
						Player otherPlayer = player.GetComponent<Player> ();
						if (otherPlayer.bodyParts.Contains (other.transform)) {
							otherPlayer.isIt = true;
							otherPlayer.StartGlowing ();
						}
					}
				}
				isIt = false;
				KillPlayer ();
			}
		}
	}

	void KillPlayer() {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		//gameObject.SetActive(false);
		foreach (Transform body in bodyParts) {
			body.gameObject.SetActive (false);
		}
		Invoke ("SpawnPlayer", respawnTime);

		if (isIt) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().RespawnDiamond ();
		}

		isIt = false;
		powerupTriggered = false;
	}

	void SpawnPlayer() {
		if (gameObject.GetComponent<SpriteRenderer> ().enabled == false) {
			float xPos = Random.Range (-3f, 7.5f);
			float yPos = Random.Range (-3.3f, 3.3f);
			speed = 3.5f;
			currentRotation = Random.Range (0f, 360f);
			transform.position = new Vector3 (xPos, yPos, 0);
			gameObject.SetActive (true);
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			gameObject.GetComponent<SpriteRenderer> ().color = playerColor;
			foreach (Transform body in bodyParts) {
				body.gameObject.transform.position = new Vector3 (xPos, yPos, 0);
				body.gameObject.SetActive (true);
				body.gameObject.GetComponent<SpriteRenderer> ().color = playerColor;
			}
		}
	}

	void StartGlowing() {
		StartCoroutine ("MakeItGlow");
	}

	public float currentRotation;
	void FixedUpdate() {
		MoveForward ();
		Rotation ();
	}

	void MoveForward() {
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void Rotation() {
		transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.x, transform.rotation.y, currentRotation));
	}

	string getScoreString() {
		return playerName + ": " + timeScore + " & " + killScore;
	}

	void activateSelfSpeed()
	{
		if (boostTriggered)
		{
			speed = 5.25f;
		}else
		{
			speed = speed + 1.75f;
		}
		powerupTriggered = true;

	}

	void deactivateSelfSpeed()
	{
		if (speed != 3.5f) { 
			speed = speed - 1.75f;
			powerupTriggered = false;
			boostTriggered = false;
		}
	}

	void activateEnemySpeed()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player != gameObject)
			{
				player.GetComponent<Player>().speed = player.GetComponent<Player>().speed + 1.75f;
				player.GetComponent<Player>().powerupTriggered = true;
			}
		}
	}

	void deactivateEnemySpeed()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player != gameObject)
			{
				if (player.GetComponent<Player>().speed != 3.5f) { 
					player.GetComponent<Player>().speed = player.GetComponent<Player>().speed - 1.75f;
					player.GetComponent<Player>().powerupTriggered = false;
					player.GetComponent<Player>().boostTriggered = false;
				}
			}
		}
	}

	void activateEnemySlow()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player != gameObject)
			{
				player.GetComponent<Player>().speed = player.GetComponent<Player>().speed - 1.5f;
				player.GetComponent<Player>().powerupTriggered = true;
			}
		}
	}

	void deactivateEnemySlow()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			if (player != gameObject)
			{
				if (player.GetComponent<Player>().speed != 3.5f) { 
					player.GetComponent<Player>().speed = player.GetComponent<Player>().speed + 1.5f;
					player.GetComponent<Player>().powerupTriggered = false;
					player.GetComponent<Player>().boostTriggered = false;
				}
			}
		}
	}

	void activateSlowSpeed()
	{
		if (boostTriggered)
		{
			speed = 2.5f;
		}else
		{
			speed = speed - 1.5f;
		}
		powerupTriggered = true;
	}

	void deactivateSlowSpeed()
	{
		if (speed != 3.5f)
		{
			speed = speed + 1.5f;
			powerupTriggered = false;
			boostTriggered = false;
		}
	}

	float lerpTime = 0.1f;
	IEnumerator MakeItGlow() {
		while (isIt) 
		{
			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp(playerColor, Color.white, lerpTime);
			foreach (Transform body in bodyParts) {
				body.GetComponent<SpriteRenderer> ().color = Color.Lerp(playerColor, Color.white, lerpTime);
			}

			yield return new WaitForSeconds (lerpTime);

			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp(Color.black, Color.white, lerpTime);
			foreach (Transform body in bodyParts) {
				body.GetComponent<SpriteRenderer> ().color = Color.Lerp(Color.black, Color.white, lerpTime);
			}

			yield return new WaitForSeconds (lerpTime);

			foreach (Transform body in bodyParts) {
				body.GetComponent<SpriteRenderer> ().color = Color.Lerp (Color.white, Color.black, lerpTime);
			}

			yield return true;
		}
	}

	void IncreaseScore() {
		if (isIt) {
			timeScore += 1;
			GameObject.Find ("ScoreboardController").GetComponent<ScoreboardController> ().UpdateScore (playerId, timeScore);

			if (timeScore >= 50) {
				GameObject.Find ("Game Controller").GetComponent<GameController> ().ShowWinScreen (playerName, playerColor);
			}
		}
	}

	Color getPlayerColor(string colorString) {
		Color color;
		switch (colorString) {
		case "GREEN":
			color = new Color (0.5f, 1f, 0.5f);
				break;
		case "RED":
			color = new Color (1f, 0.5f, 0.5f);
				break;
		case "YELLOW":
			color = new Color (0.99f, 1f, 0f);
				break;
		case "BLUE":
			color = new Color(0.5f, 0.5f, 1f);
				break;
		default: 
			color = Color.gray;
				break;
		}

		return color;
	}

}
