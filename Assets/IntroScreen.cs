using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class IntroScreen : MonoBehaviour {

	public GameObject asset;
	public VRInteractiveItem button;
	public Image over;
	public Image bar;
	private float speed = 1.5f;
	public bool clicked;
	public bool onOver;
	bool ready;

	void Start () {		
		over.enabled = false;
		bar.fillAmount = 0;
		button.OnOver +=  OnOver;
		button.OnOut += OnOut;
		button.OnDown += OnDown;
		button.OnUp += OnUp;
		Events.OnIntroScreen += OnIntroScreen;
	}
	public void OnIntroScreen()
	{
		ready = false;
		asset.SetActive (true);
	}
	void Update()
	{
		if (asset.activeSelf && Game.Instance.gameManager.DEBUG) {
			Events.OnRestart ();
			asset.SetActive (false);
		}
		if (clicked && onOver) {
			bar.fillAmount += Time.deltaTime * speed;
			if (bar.fillAmount >= 1) {
				
				bar.fillAmount = 1;
				Events.OnRestart ();
				ready = true;
				clicked = false;
				asset.SetActive (false);
			}
		}
	}
	void OnOver () {
		onOver = true;
		over.enabled = true;
		bar.fillAmount = 0;
	}
	void OnOut () {
		
		onOver = false;
		
		over.enabled = false;
		bar.fillAmount = 0;
	}
	void OnDown()
	{		
		clicked = true;
	}
	void OnUp()
	{		
		bar.fillAmount = 0;
	}
}
