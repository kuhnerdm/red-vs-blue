using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject player;
	public GameController gc;
	float velY;
	public Sprite blue;
	public Sprite red;

	GameController.type color = GameController.type.UNASSIGNED;

	public GameController.type getColor() {
		return color;
	}

	public void setColor(GameController.type c) {
		this.color = c;
		if (c == GameController.type.BLUE) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = blue;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().sprite = red;
		}
	}



	void Update () {
		if (player != null) {
			Vector3 toPlayerVec = player.transform.position - gameObject.transform.position;
			float toPlayer = toPlayerVec.x;
			if (Mathf.Abs(toPlayerVec.magnitude) < 10f && gc.getColor() != this.color) {
				float toMove = toPlayer * Time.deltaTime * 0.7f;
				if (Mathf.Abs(toMove) < 0.03f) {
					if(toPlayer > 0f) {
						toMove = 0.03f;
					} else {
						toMove = -0.03f;
					}
				}
				float newX = gameObject.transform.position.x + toMove;
				
				// Collision check in three spots on the given side
				Vector3 lowBound;
				Vector3 highBound;
				Vector3 midBound;
				if (toPlayer > 0) {
					lowBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 0.2f, 0f);
					highBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 1.9f, 0f);
					midBound = new Vector3 (newX + 0.5f, gameObject.transform.position.y + 1f, 0f);
				} else {
					lowBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 0.2f, 0f);
					highBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 3.9f, 0f);
					midBound = new Vector3 (newX - 0.5f, gameObject.transform.position.y + 1f, 0f);
				}
				
				if (Physics.OverlapSphere (lowBound, 0.01f).GetLength(0) == 0 && Physics.OverlapSphere (highBound, 0.01f).GetLength(0) == 0
				    && Physics.OverlapSphere (midBound, 0.01f).GetLength(0) == 0) {
					gameObject.transform.position = new Vector3 (newX, gameObject.transform.position.y, 0f);
				} else if (Physics.OverlapSphere (midBound, 0.01f).GetLength(0) > 0) {
					if(Physics.OverlapSphere (midBound, 0.01f)[0].tag == "Enemy") {
						gameObject.transform.position = new Vector3 (newX, gameObject.transform.position.y, 0f);
					}
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
			}
		}
	}
}