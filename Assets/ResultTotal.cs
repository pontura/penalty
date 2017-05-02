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
			result = "¡No metiste una!";
		else if (sum == 1)
			result = "¡Flojo! Metiste un solo gol...";
		else if (sum == 2)
			result = "¡Bien! Dos goles de tres...";
		else if (sum == 3)
			result = "¡Sos un profesional!";
		
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
