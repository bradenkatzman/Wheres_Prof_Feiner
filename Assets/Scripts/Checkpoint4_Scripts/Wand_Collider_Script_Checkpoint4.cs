using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wand_Collider_Script_Checkpoint4 : MonoBehaviour {

	public GameObject Goblin;
	private Goblin_Money_Collider_Script goblin_money_collider_script;

	public GameObject Dialogue_Panel;
	public Text Dialogue_PreBribe_Text;
	public Text Dialogue_PostBribe_Text;
	public Text Dialogue_AlreadyPaid_Text;
	public Button Dismiss_Dialogue_Button;

	private bool selected;
	private bool goblinPaid;

	void Start () {
		this.selected = goblinPaid = false;
		this.goblin_money_collider_script = Goblin.GetComponent<Goblin_Money_Collider_Script> ();
		Dismiss_Dialogue_Button.onClick.AddListener (DismissDialogueButtonPressed);
	}

	// Update is called once per frame
	void Update () {
		// if the goblin is selected, continuously check for the bribe payment
		if (selected && !goblinPaid) {
			goblinPaid = goblin_money_collider_script.isGoblinPaid ();

			if (goblinPaid) {
				// enter diaglogue telling the user what the next clue is
				Dialogue_Panel.SetActive(true);
				Dialogue_PreBribe_Text.enabled = false;
				Dialogue_PostBribe_Text.enabled = true;
				Dialogue_AlreadyPaid_Text.enabled = false;
			}
		}
	}


	void OnTriggerEnter(Collider other) {
		if (other.name == Goblin.name) {
			if (selected) {
				selected = false;
				Dialogue_Panel.SetActive(false);
				Dialogue_PreBribe_Text.enabled = false;
				Dialogue_PostBribe_Text.enabled = false;
				Dialogue_AlreadyPaid_Text.enabled = false;
			} else {
				selected = true;

				// check if the Goblin has already been paid
				if (goblinPaid) {
					// enter dialogue telling the user to get a move on with the game
					Dialogue_Panel.SetActive(true);
					Dialogue_PreBribe_Text.enabled = false;
					Dialogue_PostBribe_Text.enabled = false;
					Dialogue_AlreadyPaid_Text.enabled = true;

				} else {
					// enter dialogue where goblin tells the user that it'll need a bribe to give the clue
					Dialogue_Panel.SetActive(true);
					Dialogue_PreBribe_Text.enabled = true;
					Dialogue_PostBribe_Text.enabled = false;
					Dialogue_AlreadyPaid_Text.enabled = false;
				}
			}
		}
	}

	void DismissDialogueButtonPressed() {
		Dialogue_Panel.SetActive(false);
		Dialogue_PreBribe_Text.enabled = false;
		Dialogue_PostBribe_Text.enabled = false;
		Dialogue_AlreadyPaid_Text.enabled = false;
	}
}