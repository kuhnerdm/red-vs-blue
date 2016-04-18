using UnityEngine;
using System.Collections;

public class QuitController : MonoBehaviour {

	void Update () {
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}
}
