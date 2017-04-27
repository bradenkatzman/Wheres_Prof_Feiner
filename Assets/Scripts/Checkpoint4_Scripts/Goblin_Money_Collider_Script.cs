using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Money_Collider_Script : MonoBehaviour {

	private string Money_GameObject_Name;
	private bool goblinPaid;

	public GameObject left;
	public GameObject right;

	private int curr_direction;
	private int left_direction;
	private int right_direction;


	public float translateSpeed;

	// Use this for initialization
	void Start () {
		goblinPaid = false;
		Money_GameObject_Name = "money";

		left_direction = 0;
		right_direction = 1;
		curr_direction = left_direction;
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.ToLower() == Money_GameObject_Name.ToLower()) {
			//Debug.Log ("Goblin paid");
			goblinPaid = true;
		}
	}

	void Update() {
		if (curr_direction == left_direction) {
			if (transform.position == left.transform.position) {
				// turn around
				curr_direction = right_direction;

				// change directions

			} else {
				// move toward the target
				float translateStep = translateSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, left.transform.position, translateStep);
			}
		} else if (curr_direction == right_direction) {
			if (transform.position == right.transform.position) {
				curr_direction = left_direction;
				// change the direction of goblin
			} else {
				float translateStep = translateSpeed * Time.deltaTime;
				transform.position = Vector3.MoveTowards (transform.position, right.transform.position, translateStep);
			}
		}
	}

	public bool isGoblinPaid() {
		return this.goblinPaid;
	}
}