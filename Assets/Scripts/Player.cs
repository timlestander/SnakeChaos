using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform> ();
	public float rotationSensitivity = 50.0f;
	public float speed = 3.5f;
	public float boostCharge = 100f;
    public bool powerupTriggered = false;

	KeyCode leftTurn;
	KeyCode rightTurn;
	KeyCode boostKey;

	Rect boostRect;
	Texture2D boostTexture;

	bool isIt = true;

	public void setUp(KeyCode left, KeyCode right, KeyCode boost) {
		leftTurn = left;
		rightTurn = right;
		boostKey = boost;
	}

	// Use this for initialization
	void Start () {
		SpawnPlayer ();
		boostRect = new Rect (20, 20, 100, 5);
		boostTexture = new Texture2D (1, 1);
		boostTexture.SetPixel (0, 0, Color.red);
		boostTexture.Apply ();
		StartCoroutine ("MakeItGlow");
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
            if (Input.GetKeyDown(boostKey) && boostCharge > 1)
            {
                speed = 7.0f;
                boostCharge = boostCharge - 2f;
            }

			if (Input.GetKeyUp(boostKey) || boostCharge < 1)
            {

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
        if (other.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            foreach (Transform body in bodyParts)
            {
                body.gameObject.SetActive(false);
            }
            Invoke("SpawnPlayer", respawnTime);
        }
        else if (other.gameObject.CompareTag("selfSpeed"))
        {
            activateSelfSpeed();
            Invoke("deactivateSelfSpeed", powerupTime);
            other.gameObject.SetActive(false);
        }
    }

	void SpawnPlayer() {
		float xPos = Random.Range (-3f, 7.5f);
		float yPos = Random.Range (-3.3f, 3.3f);
		transform.position = new Vector3(xPos, yPos, 0);
		gameObject.SetActive (true);
		foreach (Transform body in bodyParts) {
			body.gameObject.transform.position = new Vector3 (xPos, yPos, 0);
			body.gameObject.SetActive (true);
		}
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

	void OnGUI() {
		float ratio = boostCharge / 100;
		float rectWidth = ratio * 100;
		boostRect.width = rectWidth;
		GUI.DrawTexture (boostRect, boostTexture);
	}
		
    void activateSelfSpeed()
    {
        speed = 7.0f;
        powerupTriggered = true;

    }

    void deactivateSelfSpeed()
    {
        speed = 3.5f;
        powerupTriggered = false;
    }

	float lerpTime = 1f;
	IEnumerator MakeItGlow() { 

		while (isIt) {
			
			gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp(Color.white, Color.black, lerpTime);
			foreach (Transform body in bodyParts) {
				body.GetComponent<SpriteRenderer> ().color = Color.Lerp(Color.white, Color.black, lerpTime);
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
}
