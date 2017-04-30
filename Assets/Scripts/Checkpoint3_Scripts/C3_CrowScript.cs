using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C3_CrowScript : MonoBehaviour {

	public GameObject CrowDialogueCanvas;

	void OnTriggerEnter (Collider other) {
		// display dialogue -- this is a character not an object so cannot be "selected" or added to bag
		Debug.Log("I was collided with, woo!");
		if (other.name != "Sphere") {
			return;
		}

		if (!CrowDialogueCanvas.activeSelf) {
			CrowDialogueCanvas.SetActive (true);
		}

	}

	public void CloseDialogue() {
		CrowDialogueCanvas.SetActive (false);
	}

}
