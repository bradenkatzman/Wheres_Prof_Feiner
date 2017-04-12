using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location_Diagnostics_Script : MonoBehaviour {

	public Text il_status_field;
	public Text lat_field;
	public Text long_field;

	void Start () {
		Input.location.Start ();
	}

	void Update() {
		lat_field.text = Input.location.lastData.latitude.ToString();
		long_field.text = Input.location.lastData.longitude.ToString();
		il_status_field.text = Input.location.status.ToString();
	}
}
