using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class SettingsController : MonoBehaviour {

	public GameObject playerPrefab;
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
		GameObject[] nameValues = GameObject.FindGameObjectsWithTag ("nameField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] leftValues = GameObject.FindGameObjectsWithTag ("leftField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] rightValues = GameObject.FindGameObjectsWithTag ("rightField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] speedValues = GameObject.FindGameObjectsWithTag ("speedField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] colorValues = GameObject.FindGameObjectsWithTag ("colorField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();

		Destroy (nameValues [nameValues.Length - 1]);
		Destroy (leftValues [leftValues.Length - 1]);
		Destroy (rightValues [rightValues.Length - 1]);
		Destroy (speedValues [speedValues.Length - 1]);
		Destroy (colorValues [colorValues.Length - 1]);

		playerCount--;
	}

	void StartGame() {
		GameObject[] nameValues = GameObject.FindGameObjectsWithTag ("nameField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] leftValues = GameObject.FindGameObjectsWithTag ("leftField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] rightValues = GameObject.FindGameObjectsWithTag ("rightField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] speedValues = GameObject.FindGameObjectsWithTag ("speedField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();
		GameObject[] colorValues = GameObject.FindGameObjectsWithTag ("colorField").OrderBy (g => g.transform.GetSiblingIndex ()).ToArray();

		Debug.Log ("LENGTH IS " + nameValues.Length);

		for (int x = 0; x < playerCount; x++) {

			GameObject tempPlayer = (GameObject)Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);
			Player player = tempPlayer.GetComponentInChildren<Player> ();
			string playerName = nameValues [x].GetComponent<InputField> ().text.ToString ();
			KeyCode leftKey = (KeyCode) System.Enum.Parse (typeof(KeyCode), leftValues [x].GetComponent<Dropdown> ().GetComponentInChildren<Text>().text);
			KeyCode rightKey = (KeyCode) System.Enum.Parse (typeof(KeyCode), rightValues [x].GetComponent<Dropdown> ().GetComponentInChildren<Text>().text);
			KeyCode speedKey = (KeyCode) System.Enum.Parse (typeof(KeyCode), speedValues [x].GetComponent<Dropdown> ().GetComponentInChildren<Text>().text);
			string colorString = colorValues [x].GetComponent<Dropdown> ().GetComponentInChildren<Text> ().text;
			player.setUp (playerName, leftKey, rightKey, speedKey, colorString, x);

			DontDestroyOnLoad (tempPlayer);
			SceneManager.LoadScene ("Main");
		}
	}

}
