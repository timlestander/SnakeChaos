using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour {

	public List<Transform> bodyParts = new List<Transform> ();
	public float rotationSensitivity = 50.0f;
	public float speed = 3.5f;
	public float boostCharge = 100f;
	public Text boostText;
    public bool powerupTriggered = false;

	public KeyCode leftTurn;
	public KeyCode rightTurn;

	// Use this for initialization
	void Start () {
		SpawnPlayer ();
		boostText.text = "Boost: " + boostCharge + "%";
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
            if (Input.GetKeyDown(KeyCode.Space) && boostCharge > 1)
            {
                speed = 7.0f;
                boostCharge = boostCharge - 2f;
                setBoostText();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {

                speed = 3.5f;
            }
        }

         if (boostCharge < 99) {
			boostCharge = boostCharge + 0.1f;
			setBoostText ();
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
        }else if (other.gameObject.CompareTag("enemySpeed"))
        {
            activateEnemySpeed();
            Invoke("deactivateEnemySpeed", powerupTime);
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

	void setBoostText() {
		boostText.text = "Boost: " + Mathf.Ceil(boostCharge) + "%";
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

	void Boost() {
		speed = 7f;
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

    void activateEnemySpeed()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player != gameObject)
            {
                player.GetComponent<SnakeMovement>().speed = 7.0f;
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
                player.GetComponent<SnakeMovement>().speed = 3.5f;
            }
        }
    }
}
