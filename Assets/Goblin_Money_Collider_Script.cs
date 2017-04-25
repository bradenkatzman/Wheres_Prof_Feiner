using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Money_Collider_Script : MonoBehaviour {

	private string Money_GameObject_Name;
	private bool goblinPaid;

	// Use this for initialization
	void Start () {
		goblinPaid = false;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == Money_GameObject_Name) {
			Debug.Log ("Goblin paid");
			goblinPaid = true;
		}
	}

	public bool isGoblinPaid() {
		return this.goblinPaid;
	}
}