using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject ball;
	public Ball realBall;
	public Transform container;
	public Potenciometer potenciometer;

	public states state;
	public enum states
	{
		ON_AIMING,
		SHOOTING,
		READY
	}

	void Start () {
		Events.OnShoot += OnShoot;
	}
	void Update()
	{
		if (state == states.ON_AIMING) {
			ball.transform.localEulerAngles =  mainCamera.transform.localEulerAngles;
		}
	}
	void OnShoot () {
		print ("shoot");
		Ball newBall = Instantiate (realBall, ball.transform.localPosition, ball.transform.rotation,container);

		//20 y 60:
		float value = Mathf.Lerp(10, 50, potenciometer.value);
		newBall.Init (value);
	}
}
