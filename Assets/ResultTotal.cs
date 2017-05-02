using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTotal : MonoBehaviour {

	public GameObject panel;
	public Text field;
	public Image bar;
	public bool done;
	private float speed = 0.2f;

	void Start () {
		panel.SetActive (false);
		bar.fillAmount = 0;
		done = true;
		Events.OnShowTotalResult += OnShowTotalResult;
	}
	void OnShowTotalResult()
	{
		panel.SetActive (true);
		int sum = 0;
		string result = "";

		foreach (bool b in GetComponent<ResultsManager>().penalesPateados)
			if (b)
				sum++;
		
		if (sum == 0)
			result = "Los penales no son lo tuyo…";
		else if (sum == 1)
			result = "¡A entrenar más para la próxima!";
		else if (sum == 2)
			result = "¡Felicitaciones!\n¡Buen promedio!";
		else if (sum == 3)
			result = "¡Felicitaciones goleador!\n¡Sos un campeón TOTAL!";
		
		field.text = result;
		done = false;
	}
	void Update () {
		
		if (done)
			return;
		
		bar.fillAmount += speed * Time.deltaTime;
		if (bar.fillAmount >= 1)			
			Done ();
	}
	void Done()
	{
		bar.fillAmount = 0;
		done = true;
		panel.SetActive (false);
		Events.OnIntroScreen ();
	}
}
