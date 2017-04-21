using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

	public GameObject asset;
	public Text field;

	void Start () {
		asset.SetActive (false);
		Events.OnShowResult += OnShowResult;
	}
	void OnDestroy () {
		Events.OnShowResult -= OnShowResult;
	}
	void OnShowResult (string text, bool isOn) {
		if (isOn) {
			asset.SetActive (true);
			field.text = text;
		} else {
			asset.SetActive (false);
			field.text = "";
		}
	}
}
