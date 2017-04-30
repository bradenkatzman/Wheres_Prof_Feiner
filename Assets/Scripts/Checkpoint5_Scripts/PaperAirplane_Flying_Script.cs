using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperAirplane_Flying_Script : MonoBehaviour {

	// used to translate toward these points to simulate flying
	public GameObject TopLeft;
	public GameObject TopRight;
	public GameObject BottomLeft;
	public GameObject BottomRight;
	public GameObject WandTip;

	public GameObject Letter_Panel;
	public Button Letter_Dismiss_Button;

	private bool planeSelected;

	public float translateSpeed;

	private int curr_direction;
	private int towardBottomRight = 1;
	private int towardTopRight = 2;
	private int towardBottomLeft = 3;
	private int towardTopLeft = 4;

	void Start () {
		curr_direction = towardBottomRight;
		Letter_Dismiss_Button.onClick.AddListener (LetterDismissButtonPressed);
	}

	void LetterDismissButtonPressed() {
		if (Letter_Panel.activeSelf) {
			Letter_Panel.SetActive (false);
		}
	}

	void Update () {
		// check which direction
		if (curr_direction == towardBottomRight) {
			// check if the plane has reached the destination
			if (transform.position == BottomRight.transform.position) {
				// change the direction
				curr_direction = towardTopRight;

				// turn the plane toward its target
				Vector3 euler = TopRight.transform.eulerAngles;
				euler.y += 180;
				transform.eulerAngles = euler;
			} else {
				// move toward the target
				float translateStep = translateSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, BottomRight.transform.position, translateStep);
			}
		} else if (curr_direction == towardTopRight) {
			if (transform.position == TopRight.transform.position) {
				curr_direction = towardBottomLeft;
				Vector3 euler = BottomLeft.transform.eulerAngles;
				euler.y += 180;
				transform.eulerAngles = euler;
			} else {
				float step = translateSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, TopRight.transform.position, step);
			}
		} else if (curr_direction == towardBottomLeft) {
			if (transform.position == BottomLeft.transform.position) {
				curr_direction = towardTopLeft;
				Vector3 euler = TopLeft.transform.eulerAngles;
				euler.y += 180;
				transform.eulerAngles = euler;
			} else {
				float translateStep = translateSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, BottomLeft.transform.position, translateStep);
			}
		} else if (curr_direction == towardTopLeft) {
			if (transform.position == TopLeft.transform.position) {
				curr_direction = towardBottomRight;
				Vector3 euler = BottomRight.transform.eulerAngles;
				euler.y += 180;
				transform.eulerAngles = euler;
			} else {
				float translateStep = translateSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, TopLeft.transform.position, translateStep);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Collision");
		if (other.name == WandTip.name) {
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