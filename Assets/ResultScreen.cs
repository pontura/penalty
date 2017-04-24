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
		Events.OnIntroScreen += OnIntroScreen;
	}
	void OnDestroy () {
		Events.OnShowResult -= OnShowResult;
		Events.OnIntroScreen -= OnIntroScreen;
	}
	void OnIntroScreen()
	{
		asset.SetActive (false);
		field.text = "";
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
