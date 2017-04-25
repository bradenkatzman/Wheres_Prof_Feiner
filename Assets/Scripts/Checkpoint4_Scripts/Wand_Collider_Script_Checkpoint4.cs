using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_Collider_Script_Checkpoint4 : MonoBehaviour {

	public GameObject Goblin;
	private Goblin_Money_Collider_Script goblin_money_collider_script;
	private GameObject money; // attached to the wand via accessory bag scripts

	private bool selected;
	private bool goblinPaid;

	void Start () {
		this.selected = goblinPaid = false;
		this.goblin_money_collider_script = Goblin.GetComponent<Goblin_Money_Collider_Script> ();
	}

	// Update is called once per frame
	void Update () {
		// if the goblin is selected, continuously check for the bribe payment
		if (selected && !goblinPaid) {
			goblinPaid = goblin_money_collider_script.isGoblinPaid ();

			if (goblinPaid) {
				Debug.Log ("Payment notification received");

				// enter diaglogue telling the user what the next clue is
			}
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.name == Goblin.name) {
			if (selected) {
				Debug.Log ("Goblin deselected");
				selected = false;
			} else {
				Debug.Log ("Goblin selected");
				selected = true;

				// check if the Goblin has already been paid
				if (goblinPaid) {
					// enter dialogue telling the user to get a move on with the game
					Debug.Log ("You already paid me!");

				} else {
					// enter dialogue where goblin tells the user that it'll need a bribe to give the clue
					Debug.Log("Give me money for clue");

				}
			}
		}
	}
}