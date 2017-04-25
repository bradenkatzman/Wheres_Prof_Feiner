using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperAirplane_Flying_Script : MonoBehaviour {

	// used to translate toward these points to simulate flying
	public GameObject TopLeft;
	public GameObject TopRight;
	public GameObject BottomLeft;
	public GameObject BottomRight;

	public float speed;

	private int curr_direction;
	private int towardBottomRight = 1;
	private int towardTopRight = 2;
	private int towardBottomLeft = 3;
	private int towardTopLeft = 4;

	void Start () {
		curr_direction = towardBottomRight;
	}

	void Update () {
		// check which direction
		if (curr_direction == towardBottomRight) {
			Debug.Log ("Moving");
			// check if the plane has reached the destination
			if (transform.position == BottomRight.transform.position) {
				// change the direction
				curr_direction = towardTopRight;
			} else {
				// move toward the target
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, BottomRight.transform.position, step);
			}
		} else if (curr_direction == towardTopRight) {

		} else if (curr_direction == towardBottomLeft) {

		} else if (curr_direction == towardTopLeft) {

		}
	}
}