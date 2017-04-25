using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {

	public DialoguePanel dialogue;
	public Canvas canvas;


	// Use this for initialization
	void Start () {
		//DO NOT REMOVE
		dialogue.close ();
		//DO NOT REMOVE

		DialoguePanel.onCloseFunction f = sayOtherThing;
		sayThen("where's prof. feiner?", "welcome to the chase, holmes!", f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void say(string title, string text){
		dialogue.info = text;
		dialogue.title = title;
		dialogue.open ();
	}

	void sayThen(string title, string text, DialoguePanel.onCloseFunction f){
		dialogue.info = text;
		dialogue.title = title;
		dialogue.open (f);
	}

	public void sayOtherThing(){
		say("where's prof. feiner?", "okay, moriarty!");
	}
}
