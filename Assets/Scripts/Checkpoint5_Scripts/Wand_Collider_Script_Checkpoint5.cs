using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wand_Collider_Script_Checkpoint5 : MonoBehaviour {

	public GameObject PaperAirplane;
	public GameObject Letter_Panel;
	public Button Letter_Dismiss_Button;

	private bool planeSelected;

	void Start() {
		Letter_Dismiss_Button.onClick.AddListener (LetterDismissButtonPressed);
	}

	void LetterDismissButtonPressed() {
		if (Letter_Panel.activeSelf) {
			Letter_Panel.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == PaperAirplane.name) {
			if (planeSelected) {
				Debug.Log ("Plane deselected");
				planeSelected = false;
			} else {
				Debug.Log ("paper airplane selected");
				planeSelected = true;
				Letter_Panel.SetActive (true);
			}

			// enter the dialogue that displays the contents of the letter/email
		}
	}
}

