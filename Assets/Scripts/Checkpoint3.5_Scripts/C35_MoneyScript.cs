using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vuforia {

	public class C35_MoneyScript : MonoBehaviour {

		public GameObject Wand;
		public GameObject MoneyMenu;
		public Text MoneyTranslateTxt;
		public GameObject BagGO;

		private bool translateMode;
		private Vector3 translateModeOffset;

		private WandScript WScript;
		private GameObject selectedObject;

		private Bag BagScript;

		// Use this for initialization
		void Start () {
			WScript = Wand.GetComponent<WandScript> ();
			BagScript = BagGO.GetComponent<Bag> ();

			translateMode = false;
			translateModeOffset = new Vector3 (0f, 1f, 0f);
		}
		
		// Update is called once per frame
		void Update () {
			
			selectedObject = WScript.CurSelectedObject; // get current selected obj from the wand

			if (translateMode) {
				transform.position = new Vector3 (
					Wand.transform.position.x + translateModeOffset.x,
					Wand.transform.position.y + translateModeOffset.y,
					Wand.transform.position.z + translateModeOffset.z);
			}

			if (!GroundTracked ()) {
				//Deselect ();
				MoneyMenu.SetActive (false);
			}
		}

		void OnTriggerEnter (Collider other) {

			if (selectedObject != null && selectedObject.name == gameObject.name) {
				Deselect ();
				return;
			}

			WScript.CurSelectedObject = gameObject;
			MoneyMenu.SetActive (true);
			BagScript.setAddMoneyFlag (true);
			BagScript.showAdd ();
		}

		public void TranslateToggle() {
			if (!translateMode) {
				translateMode = true;
				MoneyTranslateTxt.text = "Done Moving";

			} else {
				StopTranslating ();
			}
		}

		private void StopTranslating() {
			translateMode = false;
			MoneyTranslateTxt.text = "Move";
		}

		public void Deselect() {
			// exit all transformation modes
			StopTranslating();
			MoneyMenu.SetActive(false);
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
				//	Debug.Log("Trackable: " + tb.TrackableName);
				if (tb.TrackableName == "checkpoint-3-5") {
					return true;
				}
			}
			return false;
		}
	}
}
