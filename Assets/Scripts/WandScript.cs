using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia {

	public class WandScript : MonoBehaviour {
		public GameObject CurSelectedObject;
		private string lastSeenTarget;

		void Start () {
			CurSelectedObject = null;
			lastSeenTarget = "";
		}

		void Update () {

			// if user doesn't deselect a clue before leaving a checkpoint, 
			// deselect if for them once they reach a new one
			if (lastSeenTarget != TargetTracked ()) {
				CurSelectedObject = null;
			}

			lastSeenTarget = TargetTracked ();
		}

		// returns a string of currently tracked trackable
		private string TargetTracked() {
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
				if (tb.TrackableName != "acid-wand") {
					return tb.TrackableName;
				}
			}
			return "";
		}
	}
}