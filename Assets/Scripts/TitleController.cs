using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	public SpriteRenderer text;

	void Update () {
		if (text.color.a != 1) {
			Color c = new Color(1, 1, 1, text.color.a + (2 * Time.deltaTime));
			text.color = c;
		}

		if (Input.GetKeyDown("return") || Input.GetKeyDown("enter")) {
			if (Application.loadedLevel + 1 >= Application.levelCount) {
				Debug.Log("Problem with build!");
			} else {
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}

	}
}
