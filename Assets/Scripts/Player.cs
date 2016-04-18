using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// For polling for color and such
	public GameController gc;

	// Possible sprites
	public Sprite blueLeft;
	public Sprite blueRight;
	public Sprite redLeft;
	public Sprite redRight;

	// Current sprite
	Sprite sprite;

	// Misc
	float velY;
	float cd;
	public GameObject bulletPrefab;

	void setSprite(Sprite s) {
		gameObject.GetComponent<SpriteRenderer> ().sprite = s;
		this.sprite = s;
	}

	void Start() {
		setSprite (redRight);
		velY = 0f;
		cd = 0.25f;
	}

	// Spawns a bullet, which does its motion on its own
	void shoot() {
		GameObject bullet;
		bullet = Instantiate (bulletPrefab);
		bullet.GetComponent<Bullet> ().setGC (gc);
		if (this.sprite == redLeft || this.sprite == blueLeft) {
			bullet.transform.position = new Vector3 (gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 1, 0);
			bullet.GetComponent<Bullet>().setSprite ("left");
		} else {
			bullet.transform.position = new Vector3 (gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 1, 0);
			bullet.GetComponent<Bullet>().setSprite ("right");
		}
	}

	void Update() {

		// Change sprite
		if (Input.GetAxis ("Horizontal") > 0) {
			if (gc.getColor() == GameController.type.RED) {
				setSprite(redRight);
			} else {
				setSprite(blueRight);
			}
		} else if (Input.GetAxis ("Horizontal") < 0) {
			if (gc.getColor() == GameController.type.RED) {
				setSprite (redLeft);
			} else {
				setSprite (blueLeft);
			}
		} // Do nothing if == 0

		// Get new horizontal position
		float newX = gameObject.transform.position.x + (Input.GetAxis ("Horizontal") * 10f * Time.deltaTime);

		// Collision check in three spots on the given side
		Vector3 lowBound;
		Vector3 highBound;
		Vector3 midBound;
		Vector3 backLowBound;
		Vector3 backHighBound;
		Vector3 backMidBound;
		Vector3 topBound = new Vector3 (newX, gameObject.transform.position.y + 2.1f, 0f);
		Vector3 bottomBound = new Vector3 (newX, gameObject.transform.position.y - 0.3f, 0f);
		if (Input.GetAxis ("Horizontal") > 0) {
			lowBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 0.2f, 0f);
			highBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 1.9f, 0f);
			midBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 1f, 0f);
			backLowBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 0.2f, 0f);
			backHighBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 3.9f, 0f);
			backMidBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 1f, 0f);
		} else {
			lowBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 0.2f, 0f);
			highBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 3.9f, 0f);
			midBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 1f, 0f);
			backLowBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 0.2f, 0f);
			backHighBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 1.9f, 0f);
			backMidBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 1f, 0f);
		}

		if (Physics.OverlapSphere (lowBound, 0.01f).GetLength (0) == 0 && Physics.OverlapSphere (highBound, 0.01f).GetLength (0) == 0
			&& Physics.OverlapSphere (midBound, 0.01f).GetLength (0) == 0) {
			gameObject.transform.position = new Vector3 (newX, gameObject.transform.position.y, 0f);
		} if (Physics.OverlapSphere (midBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (midBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (backMidBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (backMidBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (highBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (highBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (backHighBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (backHighBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (lowBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (lowBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (backLowBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (backLowBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (topBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (topBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		} else if (Physics.OverlapSphere (bottomBound, 0.01f).GetLength (0) == 1) {
			GameObject other = Physics.OverlapSphere (bottomBound, 0.01f)[0].gameObject;
			if(other.tag == "Enemy" && other.GetComponent<Enemy>().getColor() != gc.getColor()) {
				gc.lose();
			}
		}

		// Collision check in two spots below
		Vector3 belowMeLeft = new Vector3 (gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.06f, 0f);
		Vector3 belowMeRight = new Vector3 (gameObject.transform.position.x + 0.5f, gameObject.transform.position.y - 0.06f, 0f);
		if (Physics.OverlapSphere (belowMeLeft, 0.0001f).GetLength(0) == 0 && Physics.OverlapSphere (belowMeRight, 0.0001f).GetLength(0) == 0) {
			// Calc new velocity
			if(velY < 0.005f && velY > -0.005f) {
				velY = -0.005f;
			}
			else {
				velY -= 15f * Time.deltaTime;
			}

			// Fall
			float newY = gameObject.transform.position.y + (velY * Time.deltaTime);
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, newY, 0f);


		} else { // Something is immediately below me
			velY = 0;

			// Jump
			if(Input.GetKey("up") || Input.GetKey("space") || Input.GetKey("w")) {
				velY = 10;
				float newY = gameObject.transform.position.y + (velY * Time.deltaTime);
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, newY, 0f);
			}
		}

		// Handle shoot
		if (cd >= 0) {
			cd -= Time.deltaTime;
		} else if (Input.GetKey("right shift") || Input.GetKey("left shift")){
			cd = 0.25f;
			shoot ();
		}
	}
	
	public void switchBlue() {
		if (this.sprite == redLeft) {
			setSprite (blueLeft);
		}
		else {
			setSprite (blueRight);
		}
	}

	public void switchRed() {
		if (this.sprite == blueLeft) {
			setSprite (redLeft);
		} else {
			setSprite (redRight);
		}
	}



}
