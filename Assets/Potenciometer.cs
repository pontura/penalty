using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potenciometer : MonoBehaviour {

	public GameObject asset;
	public Image bar;
	public float speed;
	public float value;
	private int multiplier = 1;
	private bool isOn;

	void Start () {
		Events.OnShowPotenciometer += OnShowPotenciometer;
		Events.OnShoot += OnShoot;
		speed /= 10;
		asset.SetActive (false);
	}
	void OnDisable () {
		Events.OnShowPotenciometer += OnShowPotenciometer;
		Events.OnShoot -= OnShoot;
	}
	void OnShoot()
	{
		asset.SetActive (false);
	}
	void OnShowPotenciometer(bool isOn)
	{
		this.isOn = isOn;
		if (isOn)
			asset.SetActive (true);
		else asset.SetActive (false);
	}
	void Update () {
		
		if (!isOn)
			return;
		
		value = bar.fillAmount + (speed*Time.deltaTime*multiplier);
		bar.fillAmount = value;
		if (value > 1)
			multiplier = -1;
		else if (value < 0)
			multiplier = 1;
	}
}
