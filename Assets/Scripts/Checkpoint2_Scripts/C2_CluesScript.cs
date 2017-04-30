using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vuforia {
	public class C2_CluesScript : MonoBehaviour {

		public GameObject Wand;

		public GameObject ClueMenu;
		public Text ClueTranslateTxt;
		public GameObject LegibleClue;

		private WandScript WScript;
		private GameObject selectedObject;

		private bool translateMode;
		private Vector3 translateModeOffset;

		// Use this for initialization
		void Start () {
			WScript = Wand.GetComponent<WandScript> ();
			translateMode = false;
			translateModeOffset = new Vector3 (0f, 1f, 0f);
		}
		
		void Update () {

			selectedObject = WScript.CurSelectedObject; // get current selected obj from the wand
			if (translateMode) {
				transform.position = new Vector3 (
					Wand.transform.position.x + translateModeOffset.x,
					Wand.transform.position.y + translateModeOffset.y,
					Wand.transform.position.z + translateModeOffset.z);

			}

			if (!GroundTracked ()) {
				ClueMenu.SetActive (false);
				//Deselect();
			}
		}

		void OnTriggerEnter (Collider other) {


			Debug.Log ("cur: " + gameObject.name);
			Debug.Log (other.gameObject.name);
			if (selectedObject != null) {
				Debug.Log ("selected: "  + selectedObject.name);
			}


			if (other.gameObject.name != "Sphere") {
				return;
			}

			// if this clue is currently selected, deselect it
			if (selectedObject != null && selectedObject.name == gameObject.name) {
				Deselect ();
				return;
			}

			//if another clue is selected, deselect it 
			if (selectedObject != null) {
				// this is v hacky since you'll get null reference exceptions but...it won't break so w/e
				WScript.CurSelectedObject.GetComponent<C2_CluesScript> ().Deselect ();
				//DeselectOtherObject();
			}

			WScript.CurSelectedObject = gameObject;
			ClueMenu.SetActive (true);

		}

		/*
		private void DeselectOtherObject() {
			GameObject sel = WScript.CurSelectedObject;
			if (sel.tag == "C2") {
				WScript.CurSelectedObject.GetComponent<C2_CluesScript> ().Deselect ();
			} else if (sel.tag == "C35") {
				WScript.CurSelectedObject.GetComponent<C35_MoneyScript> ().Deselect ();
			}

		}*/

		public void ViewClue() {
			LegibleClue.SetActive (true);
		}

		public void CloseClue() {
			LegibleClue.SetActive (false);
		}

		public void TranslateToggle() {
			if (!translateMode) {
				translateMode = true;
				ClueTranslateTxt.text = "Done Moving";

			} else {
				StopTranslating ();
			}
		}

		private void StopTranslating() {
			translateMode = false;
			ClueTranslateTxt.text = "Move";
		}

		public void Deselect() {
			// exit all transformation modes
			StopTranslating();
			ClueMenu.SetActive(false);
			LegibleClue.SetActive (false);
			WScript.CurSelectedObject = null;
		}

		private bool GroundTracked() {
			// Get the Vuforia StateManager
			StateManager sm = TrackerManager.Instance.GetStateManager ();

			// Query the StateManager to retrieve the list of
			// currently 'active' trackables 
			//(i.e. the ones currently being tracked by Vuforia)
			IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

			// Iterate through the list of active trackables
			//Debug.Log ("List of trackables currently active (tracked): ");
			foreach (TrackableBehaviour tb in activeTrackables) {
				//Debug.Log("Trackable: " + tb.TrackableName);
				if (tb.TrackableName == "checkpoint-2") {
					return true;
				}
			}
			return false;
		}
	}
}