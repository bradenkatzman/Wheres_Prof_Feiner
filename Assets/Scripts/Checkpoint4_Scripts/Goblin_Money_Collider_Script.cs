using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin_Money_Collider_Script : MonoBehaviour {

	private string Money_GameObject_Name;
	private bool goblinPaid;

	public GameObject left;
	public GameObject right;
	public GameObject WandTip;

	private int curr_direction;
	private int left_direction;
	private int right_direction;

	public float translateSpeed;

	public GameObject Dialogue_Panel;
	public GameObject Dialogue_PreBribe_Text;
	public GameObject Dialogue_PostBribe_Text;
	public Button Dismiss_Dialogue_Button;

	public Text bagMoneyText;

	private bool selected;

	// Use this for initialization
	void Start () {
		goblinPaid = false;
		Money_GameObject_Name = "money";

		left_direction = 0;
		right_direction = 1;
		curr_direction = left_direction;

		this.selected = goblinPaid = false;
		Dismiss_Dialogue_Button.onClick.AddListener (DismissDialogueButtonPressed);
	}

	void DismissDialogueButtonPressed() {
		Dialogue_Panel.SetActive(false);
		Dialogue_PreBribe_Text.SetActive(false);
		Dialogue_PostBribe_Text.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.name.ToLower() == Money_GameObject_Name.ToLower()) {
			//Debug.Log ("Goblin paid");
			goblinPaid = true;
			other.gameObject.SetActive (false);
			// subtract money text in bag to read 0
			bagMoneyText.text = "0";
		} else if (other.name == WandTip.name) {
			if (selected) {
				selected = false;
				Dialogue_Panel.SetActive(false);
				Dialogue_PreBribe_Text.SetActive(false);
				Dialogue_PostBribe_Text.SetActive(false);
			} else {
				selected = true;

				// check if the Goblin has already been paid
				if (goblinPaid) {
					// enter dialogue telling the user to get a move on with the game
					Dialogue_Panel.SetActive(true);
					Dialogue_PreBribe_Text.SetActive(false);
					Dialogue_PostBribe_Text.SetActive(false);

				} else {
					// enter dialogue where goblin tells the user that it'll need a bribe to give the clue
					Dialogue_Panel.SetActive(true);
					Dialogue_PreBribe_Text.SetActive(true);
					Dialogue_PostBribe_Text.SetActive(false);
				}
			}
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

		// if the goblin is selected, continuously check for the bribe payment
		if (selected && goblinPaid) {
				// enter diaglogue telling the user what the next clue is
				Dialogue_Panel.SetActive(true);
				Dialogue_PreBribe_Text.SetActive(false);
				Dialogue_PostBribe_Text.SetActive(true);
		}
	}

	public bool isGoblinPaid() {
		return this.goblinPaid;
	}
}