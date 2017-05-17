using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	public GameObject panel;

	void Start () {
		panel.SetActive (false);
		Events.OnSplash += OnSplash;
	}

	void OnSplash (bool isOn) {
		panel.SetActive (isOn);
	}
}
