using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour {

	public GameObject playGameBtn;
	public GameObject howToBtn;

	// Use this for initialization
	void Start () {
		playGameBtn.GetComponent<Button> ().onClick.AddListener (GoToSettings);
		howToBtn.GetComponent<Button> ().onClick.AddListener (GoToHowTo);
	}
	
	void GoToSettings() {
		SceneManager.LoadScene ("Settings");
	}

	void GoToHowTo() {
		SceneManager.LoadScene ("HowTo");
	}
}
