using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToController : MonoBehaviour {

	public GameObject backBtn;

	// Use this for initialization
	void Start () {
		backBtn.GetComponent<Button> ().onClick.AddListener (GoBack);
	}
	
	void GoBack() {
		SceneManager.LoadScene ("Start");
	}
}
