using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

	public RectTransform parentPanel;
	public GameObject startGameBtn;
	public GameObject addPlayerBtn;
	public GameObject removePlayerBtn;
	public GameObject name;
	public GameObject left;
	public GameObject right;
	public GameObject speed;
	public GameObject color;
	private int playerCount = 2;

	// Use this for initialization
	void Start () {
		startGameBtn.GetComponent<Button> ().onClick.AddListener (StartGame);
		addPlayerBtn.GetComponent<Button> ().onClick.AddListener (AddPlayer);
		removePlayerBtn.GetComponent<Button> ().onClick.AddListener (RemovePlayer);
	}
	
	// Update is called once per frame
	void Update () {

	}
		
	void AddPlayer() {
		if (playerCount < 4) {
			Debug.Log ("Hey, let's add another player");
			GameObject tempName = Instantiate (name);
			GameObject tempLeft = Instantiate (left);
			GameObject tempRight = Instantiate (right);
			GameObject tempSpeed = Instantiate (speed);
			GameObject tempColor = Instantiate (color);

			tempName.transform.SetParent (parentPanel);
			tempLeft.transform.SetParent (parentPanel);
			tempRight.transform.SetParent (parentPanel);
			tempSpeed.transform.SetParent (parentPanel);
			tempColor.transform.SetParent (parentPanel);

			playerCount++;
		}
	}

	void RemovePlayer() {
		Debug.Log ("DIE DIE DIE");
	}

	void StartGame() {
		GameObject[] nameValues = GameObject.FindGameObjectsWithTag ("nameField");
		GameObject[] leftValues = GameObject.FindGameObjectsWithTag ("leftField");

		Debug.Log (nameValues [1].GetComponent<InputField> ().text);
		Debug.Log (nameValues [0].GetComponent<InputField> ().text);

		Application.LoadLevel ("Main");
	}


}
