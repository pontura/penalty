using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follower : MonoBehaviour {

	public GameObject asset;
	public Text field;
	public Potenciometer potenciometer;
	public float MAX_Y;

	void Start () {
		Events.OnShowPotenciometer += OnShowPotenciometer;
		field.text = "";
	}
	void OnDestroy()
	{
		Events.OnShowPotenciometer -= OnShowPotenciometer;
	}
	void OnShowPotenciometer(bool isOn)
	{
		asset.SetActive (isOn);
	}
	void Update () {
		float _y = potenciometer.value;
		Vector3 pos = asset.transform.localPosition;
		pos.y = Mathf.Lerp(0,MAX_Y,_y);
		asset.transform.localPosition = pos;
	}
}
