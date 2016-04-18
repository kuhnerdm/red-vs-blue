using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// For polling for color and such
	GameController gc;

	// Possible sprites
	public Sprite left;
	public Sprite right;

	// Current sprite
	Sprite sprite;

	// Misc
	float travelled;

	void Start () {
		travelled = 0;
	}

	public void setGC(GameController gc) {
		this.gc = gc;
	}

	public void setSprite(string dir) {
		if (dir == "left") {
			this.sprite = left;
		} else {
			this.sprite = right;
		}
		gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
	}

	void Update () {

		// Metroid 1-style bullets
		if (travelled > 10) {
			Destroy(gameObject);
		}

		// Move
		float newX;
		if (this.sprite == this.left) {
			newX = gameObject.transform.position.x - Time.deltaTime * 20;
		} else {
			newX = gameObject.transform.position.x + Time.deltaTime * 20;
		}
		travelled += Time.deltaTime * 20;
		gameObject.transform.position = new Vector3 (newX, gameObject.transform.position.y, 0);

		// Check if I'm in another object
		Collider[] collidersImIn = Physics.OverlapSphere (gameObject.transform.position, 0.0001f);
		if (collidersImIn.GetLength(0) > 0) {
			GameObject other = collidersImIn[0].gameObject;
			if (other.tag == "Wall") {
				Destroy(gameObject);
			} else if(other.tag == "Enemy") {
				if (other.GetComponent<Enemy>().getColor() != gc.getColor()) {
					Destroy(other);
					}
				else {
					GameObject newE = Instantiate(other);
					float eNewX;
					if(this.sprite == left) {
						eNewX = other.transform.position.x - 1;
						while(Physics.OverlapSphere (new Vector3(eNewX, other.transform.position.y+1, 0), 0.001f).GetLength(0) > 0) {
							eNewX--;
						}
					} else {
						eNewX = other.transform.position.x + 1;
						while(Physics.OverlapSphere (new Vector3(eNewX, other.transform.position.y+1, 0), 0.001f).GetLength(0) > 0) {
							eNewX++;
						}
					}
					newE.transform.position = new Vector3(eNewX, other.transform.position.y, 0);
					if(other.GetComponent<Enemy>().getColor() == GameController.type.RED) {
						newE.GetComponent<Enemy>().setColor(GameController.type.BLUE);
					} else {
						newE.GetComponent<Enemy>().setColor(GameController.type.RED);
					}
					Destroy(gameObject);
				}
			}
		}
	}
}
