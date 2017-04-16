using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location_Script : MonoBehaviour {

	public Text lat_field;
	public Text long_field;

	private float currLat;
	private float currLong;

	IEnumerator Start () {
		this.currLat = 0.0f;
		this.currLong = 0.0f;

		// check if location enabled
		if (!Input.location.isEnabledByUser) {
			Debug.Log ("Location not enabled on this device");
			yield break;
		}

		/* start service
		 * @param desiredAccuracyInMeters
		 * @param updateDistanceInMeters
		 */ 
		Input.location.Start (10.0f, 10.0f);

		// wait for the service to initialize
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds (1);
			maxWait--;
		}

		// service didn't initialize in seconds
		if (maxWait < 1) {
			Debug.Log ("Timed out");
			yield break;
		}

		// connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			Debug.Log ("Unable to determine device location");
			yield break;
		}
	}

	void Update() {
		//Input.location.lastData.altitude
		//Input.location.lastData.horizontalAccuracy
		//Input.location.lastData.timestamp

		currLat = Input.location.lastData.latitude;
		currLong = Input.location.lastData.longitude;

		lat_field.text = currLat.ToString();
		long_field.text = currLong.ToString();
	}

	public float getCurrLatitute() {
		return this.currLat;
	}

	public float getCurrLongitude() {
		return this.currLong;
	}
}