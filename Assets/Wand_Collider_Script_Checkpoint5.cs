using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_Collider_Script_Checkpoint5 : MonoBehaviour {

	public GameObject PaperAirplane;

	void OnTriggerEnter(Collider other) {
		if (other.name == PaperAirplane.name) {
			Debug.Log ("paper airplane selected");

			// enter the dialogue that displays the contents of the letter/email
		}
	}
}