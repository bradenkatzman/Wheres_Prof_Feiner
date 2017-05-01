using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour {
	//controls to add to the bag
	public Button add;
	public Text addText;

	//to know what were adding
	public bool addMoney;
	public bool addBottle;

	//wand stuff
	public Button attach;
	public GameObject wand;

	//the model
	public GameObject bagModel;

	public Text openBagButtonText;

	public GameObject checkpointMoney;

	//used for attaching to wand
	public GameObject bottle;
	public GameObject money;
	public GameObject tix;

	//used for toggling attach
	private bool bottleSelected = false;
	private bool moneySelected = false;
	private bool tixSelected = false;

	//used for updateing bag info
	public int bottleNum = 0;
	public Text bottleText;
	public int moneyNum = 0;
	public Text moneyText;
	public int tixNum = 1;
	public Text tixText;

	public void OpenModel() {
		if (bagModel.activeSelf) {
			openBagButtonText.text = "Open Bag";
			bagModel.SetActive (false);
		} else {
			openBagButtonText.text = "Close Bag";
			bagModel.SetActive (true);
		}
		Debug.Log ("Open Model");
	}

	public void showAdd() {
		if (add.gameObject.activeSelf) {
			add.gameObject.SetActive (false);
		} else {
			add.gameObject.SetActive (true);
		}
		if (addText.gameObject.activeSelf) {
			addText.gameObject.SetActive (false);
		} else {
			addText.gameObject.SetActive (true);
		}
		Debug.Log ("Toggle Add Control");
	}

	void showAttach() {
		if (bottleSelected || moneySelected || tixSelected) {
			attach.gameObject.SetActive (true);
		} else {
			attach.gameObject.SetActive (false);
		}
	}

	public void Attach() {
		//attach to wand
		if (bottleSelected) {
			if (bottleText.text != "0") {
				if (bottle.activeSelf) {
					bottle.SetActive (false);
				} else {
					bottle.SetActive (true);
				}			}
//			bottleNum = 0;
//			bottleText.text = bottleNum.ToString();

		} else if (moneySelected) {
//			moneyNum = 0;
//			moneyText.text =  moneyNum.ToString();
			if (moneyText.text != "0") {
				if (money.activeSelf) {
					money.SetActive (false);
				} else {
					money.SetActive (true);
				}
			}
		} else if (tixSelected) {
			//attach
//			tixNum = 0;
//			tixText.text = tixNum.ToString();
			if (tixText.text != "0") {
				if (tix.activeSelf) {
					tix.SetActive (false);
				} else {
					tix.SetActive (true);
				}
			}
		}
	}

	public void Add(){
		//add money
		//turn off money in checkpoint
		if (addMoney) {
			moneyNum = 20;
			moneyText.text =  moneyNum.ToString();
			checkpointMoney.SetActive (false);
			showAdd ();
			OpenModel ();
		} else if (addBottle){
			//turn off bottle in checkpoint
			bottleNum = 1;
			bottleText.text = bottleNum.ToString();
		}
	}

	public void Select() {
		showAdd ();
	}
		
	public void bottleSelect(bool toggle) {
		bottleSelected = !bottleSelected;
		showAttach ();
	}
	public void moneySelect(bool toggle) {
		moneySelected = !moneySelected;
		showAttach ();
	}
	public void tixSelect(bool toggle) {
		tixSelected = !tixSelected;
		showAttach ();
	}

	public void setAddMoneyFlag(bool flag) {
		this.addMoney = flag;
	}
}