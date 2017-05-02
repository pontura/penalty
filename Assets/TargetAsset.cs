using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetAsset : MonoBehaviour {

	public GameObject asset;
	public GameObject asset2;

	public Image[] balls;
	public GameObject counter;
	public Text field;

	private ResultsManager resultsManager;

	void Start () {
		resultsManager = GetComponent<ResultsManager> ();
		OnIntroScreen ();
		Events.OnShoot += OnShoot;
		Events.OnRestart += OnRestart;
		Events.OnIntroScreen += OnIntroScreen;
		field.text = "";
	}
	void OnShoot () {
		counter.SetActive (false);
		asset.SetActive (false);
		asset2.SetActive (true);
	}
	void OnRestart()
	{
		counter.SetActive (true);
		asset2.SetActive (false);
		asset.SetActive (true);
		int id = 0;
		foreach (bool r in resultsManager.penalesPateados) {
			balls [id].enabled = true;
			if (r)
				balls [id].color = Color.green;
			else
				balls [id].color = Color.red;

			balls [id].GetComponent<Animation> ().enabled = false;

			id++;
		}

		Setfield ();

		if (id >= 3) {
			id = 0;
			OnIntroScreen ();
		}
		balls [id].enabled = true;
		balls [id].GetComponent<Animation> ().enabled = true;

	}
	void OnIntroScreen()
	{
		foreach (Image i in balls) {
			i.color = Color.white;
			i.enabled = false;
			i.gameObject.GetComponent<Animation> ().enabled = false;
		}
		field.text = "PENAL 1";
	}
	void Setfield()
	{
		field.text = "PENAL " + (resultsManager.penalesPateados.Count+1);
	}
}
