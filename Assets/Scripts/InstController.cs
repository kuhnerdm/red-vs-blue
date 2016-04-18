using UnityEngine;
using System.Collections;

public class InstController : MonoBehaviour {

	public SpriteRenderer text;
	public SpriteRenderer red;
	public SpriteRenderer blue;

	void Update () {
		if (text.color.a != 1) {
			Color c = new Color(1, 1, 1, text.color.a + (2 * Time.deltaTime));
			text.color = c;
			if (red != null && blue != null) {
				red.color = c;
				blue.color = c;
			}
		}
		
		if (Input.GetKeyDown("return") || Input.GetKeyDown("enter")) {
			if (Application.loadedLevel + 1 >= Application.levelCount) {
				Application.LoadLevel(0);
			} else {
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}
	}
}
