using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultScreen : MonoBehaviour {

	public GameObject asset;
	private ResultsManager resultsManager;

	public GameObject gol;
	public GameObject atajada;
	public GameObject afuera;
	public GameObject palo;



	void Start () {
		resultsManager = GetComponent<ResultsManager> ();
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
		gol.SetActive (false);
		atajada.SetActive (false);
		afuera.SetActive (false);
		palo.SetActive (false);
	}
	void OnShowResult (string text, bool isOn) {
		
		gol.SetActive (false);
		atajada.SetActive (false);
		afuera.SetActive (false);
		palo.SetActive (false);

		if (isOn) {			
			asset.SetActive (true);
			if (text == "gol")
				gol.SetActive (true);
			else if (text == "atajada")
				atajada.SetActive (true);
			else if (text == "afuera")
				afuera.SetActive (true);
			else if (text == "palo")
				palo.SetActive (true);
		}
	}
}
