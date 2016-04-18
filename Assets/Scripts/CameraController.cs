using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Player player;
	
	void Update () {
		if (player != null) {
			Vector2 toPlayer = new Vector2 (player.transform.position.x - gameObject.transform.position.x, (player.transform.position.y + 1f) - gameObject.transform.position.y);
			float newX = gameObject.transform.position.x + toPlayer.x * Time.deltaTime;
			float newY = gameObject.transform.position.y + toPlayer.y * Time.deltaTime;
			gameObject.transform.position = new Vector3 (newX, newY, -10);
		}
	}
}
