using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Money_Collider_Script : MonoBehaviour {

	private string Money_GameObject_Name;
	private bool goblinPaid;

	// Use this for initialization
	void Start () {
		goblinPaid = false;
		Money_GameObject_Name = "money";
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.ToLower() == Money_GameObject_Name.ToLower()) {
			Debug.Log ("Goblin paid");
			goblinPaid = true;
		}
	}

	public bool isGoblinPaid() {
		return this.goblinPaid;
	}
}