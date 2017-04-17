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

	void Start () {
		
	}

	void Update () {
		value = bar.fillAmount + (speed*Time.deltaTime*multiplier);
		bar.fillAmount = value;
		if (value > 1)
			multiplier = -1;
		else if (value < 0)
			multiplier = 1;
	}
}
