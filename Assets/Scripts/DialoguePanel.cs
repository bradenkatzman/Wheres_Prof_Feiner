using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour {
	public bool run;
	public string info;
	public string title;
	public Button next;
	public Text infoText;
	public Text titleText;
	public delegate void onCloseFunction ();
	public onCloseFunction f;

	// Use this for initialization
	void Start () {
		Button btn = next.GetComponent<Button>();
		btn.onClick.AddListener(close);
	}
	
	// Update is called once per frame
	void Update () {
		infoText.text = info;
		titleText.text = title;
	}

	public void open(onCloseFunction fun){
		run = true;
		f = fun;
		this.gameObject.SetActive (true);
	}

	public void open(){
		run = true;
		f = null;
		this.gameObject.SetActive (true);
	}

	public void close(){
		this.gameObject.SetActive (false);
		run = false;
		if (f != null){f ();}
	}


}
