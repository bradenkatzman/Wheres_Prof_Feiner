using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapResize_Script : MonoBehaviour {

	public Button ResizeMap_Button;
	private RectTransform rTrans_Panel; // reference to panel transform for efficiency
	private Vector3 initialScale_Panel; // the initial size of the minimap panel
	private float initialYPos;

	private bool expanded;
	private string EXPAND = "Expand";
	private string MINIMIZE = "Minimize";

	void Start () {
		this.rTrans_Panel = transform.GetComponent<RectTransform> ();
		this.initialScale_Panel = rTrans_Panel.localScale;
		this.initialYPos = rTrans_Panel.anchoredPosition.y;

		ResizeMap_Button.onClick.AddListener (ResizeMap);

		this.expanded = false;
	}

	void ResizeMap() {
		if (expanded) {
			rTrans_Panel.localScale = new Vector3 (initialScale_Panel.x, initialScale_Panel.y, initialScale_Panel.z);
			rTrans_Panel.anchoredPosition = new Vector3 (0, initialYPos, 0);
			ResizeMap_Button.GetComponentInChildren<Text> ().text = EXPAND;
			expanded = false;
		} else {
			rTrans_Panel.localScale = new Vector3(initialScale_Panel.x * 2.0f, initialScale_Panel.y * 2.0f, initialScale_Panel.z * 2.0f);
			rTrans_Panel.anchoredPosition = new Vector3 (0, -223.0f, 0);
			ResizeMap_Button.GetComponentInChildren<Text> ().text = MINIMIZE;
			expanded = true;
		}
	}
}