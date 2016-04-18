using UnityEngine;
using System.Collections;

public class BlueEnemy: MonoBehaviour {
	
	void Start () {
		if (gameObject.GetComponent<Enemy> ().getColor ().Equals(GameController.type.UNASSIGNED)) {
			gameObject.GetComponent<Enemy> ().setColor (GameController.type.BLUE);
		}
		gameObject.GetComponent<Enemy> ().gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		gameObject.GetComponent<Enemy> ().player = GameObject.FindGameObjectWithTag ("Player");
	}
}
