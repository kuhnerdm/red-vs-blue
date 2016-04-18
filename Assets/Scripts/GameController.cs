using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum type {RED, BLUE, UNASSIGNED};

	// Current color
	type color;

	// Misc
	float timeUntilSwitch;
	public Player player;
	bool won;
	bool lost;
	public GameObject wonLostText;
	
	void Start () {
		this.color = type.RED;
		this.timeUntilSwitch = 10;
		this.won = false;
		this.lost = false;
	}

	public type getColor() {
		return this.color;
	}

	void Update () {

		// Decrease cd
		this.timeUntilSwitch -= Time.deltaTime;

		if (player != null) {
			if (timeUntilSwitch < 0) {
				if(this.color == type.RED) { // Switch to Blue
					this.color = type.BLUE;
					this.player.switchBlue();
				}
				else { // Switch to Red
					this.color = type.RED;
					this.player.switchRed();
				}
				this.timeUntilSwitch = 10;
			}
			if (GameObject.FindGameObjectsWithTag ("Enemy").GetLength (0) == 0) {
				win();
			}
		}

		if (Input.GetKey ("return") || Input.GetKey ("enter")) {
			if(lost) {
				Application.LoadLevel(Application.loadedLevel);
			} else if (won) {
				if(Application.loadedLevel + 1 >= Application.levelCount) {
					Debug.Log("Problem with build!");
				} else {
					Application.LoadLevel(Application.loadedLevel + 1);
				}
			}
		}

		if (Input.GetKey ("r")) {
			Application.LoadLevel(Application.loadedLevel);
		}

	}

	void win() {
		this.won = true;
		wonLostText.GetComponent<UnityEngine.UI.Text> ().text = "LEVEL COMPLETE!\nPress enter to continue!";
	}

	public void lose() {
		Destroy (player.gameObject);
		this.lost = true;
		wonLostText.GetComponent<UnityEngine.UI.Text> ().text = "LEVEL FAILED!\nPress enter to continue!";
	}
}
