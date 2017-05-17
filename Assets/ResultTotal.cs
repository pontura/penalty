using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTotal : MonoBehaviour {

	public GameObject panel;
	public Image bar;
	public bool done;
	private float speed = 0.2f;

	public GameObject result0;
	public GameObject result1;
	public GameObject result2;
	public GameObject result3;


	void Start () {
		panel.SetActive (false);
		bar.fillAmount = 0;
		done = true;
		Events.OnShowTotalResult += OnShowTotalResult;
	}
	void Reset()
	{
		result0.SetActive(false);
		result1.SetActive(false);
		result2.SetActive(false);
		result3.SetActive(false);
	}
	void OnShowTotalResult()
	{
		Reset ();
		panel.SetActive (true);
		int sum = 0;

		foreach (bool b in GetComponent<ResultsManager>().penalesPateados)
			if (b)
				sum++;
		
		if (sum == 0)
			result0.SetActive(true);
		else if (sum == 1)
			result1.SetActive(true);
		else if (sum == 2)
			result2.SetActive(true);
		else if (sum == 3)
			result3.SetActive(true);
		
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
		Events.OnShowcongrats ();
	}
}
