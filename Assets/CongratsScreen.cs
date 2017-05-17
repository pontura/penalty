using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsScreen : MonoBehaviour {



	public GameObject panel;
	public Image bar;
	public bool done;
	private float speed = 0.3f;

	void Start () {
		panel.SetActive (false);
		bar.fillAmount = 0;
		done = true;
		Events.OnShowcongrats += OnShowcongrats;
	}
	void OnShowcongrats()
	{
		StartCoroutine (GotoIntro());
	}
	IEnumerator GotoIntro()
	{
		if (Game.Instance.isExcellium) {
			Events.OnSplash (true);
			yield return new WaitForSeconds (2.5f);
			Events.OnSplash (false);
		} 
		yield return new WaitForSeconds (0.01f);
		panel.SetActive (true);
		int sum = 0;
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
