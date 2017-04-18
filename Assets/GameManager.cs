using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class GameManager : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject ball;
	public Ball realBall;
	public Transform container;
	public Potenciometer potenciometer;
	public VRCameraFade vrCameraFade;

	public types type;
	public enum types
	{
		MIRA_DIRECTA,
		BOLA_TARGET
	}

	public states state;
	public enum states
	{
		ON_AIMING,
		SHOOTING,
		READY
	}
	private int offsetRotation = 50;

	void Start () {
		Events.OnShoot += OnShoot;
	}
	void Update()
	{
		if (state == states.SHOOTING)
			return;
		if (state == states.ON_AIMING) {
			switch (type) {
			case types.MIRA_DIRECTA:
				MiraDirecta ();
				break;
			case types.BOLA_TARGET:
				BolaDirecta ();
				break;
			}

		}
	}
	public VRInput m_VRInput;

	void OnEnable()
	{
		m_VRInput.OnDown += OnDown;
		m_VRInput.OnUp += OnUp;
	}
	void OnDisable()
	{
		m_VRInput.OnDown -= OnDown;
		m_VRInput.OnUp -= OnUp;
	}
	void OnDown()
	{
		if(state == states.ON_AIMING)
		Events.OnShowPotenciometer (true);
	}
	void OnUp()
	{
		if (state == states.ON_AIMING) {
			Events.OnShowPotenciometer (false);
			Events.OnShoot ();
		}
	}

	void MiraDirecta()
	{
		Vector3 cameraRot = mainCamera.transform.eulerAngles;
		ball.transform.eulerAngles = cameraRot;
	}
	void BolaDirecta()
	{
		Vector3 cameraRot = mainCamera.transform.eulerAngles;
		cameraRot.x *= 2;
		cameraRot.x -= offsetRotation;
		//print(" X : " + cameraRot.x);
		if (cameraRot.x > 0 && cameraRot.x < 180 )
			cameraRot.x = 0;

		ball.transform.eulerAngles = cameraRot;
	}
	void OnShoot () {
		if (state == states.SHOOTING)
			return;
		state = states.SHOOTING;
		Vector3 rot = ball.transform.eulerAngles;
		rot.x += -14;

		if (rot.y > 180)
			rot.y = rot.y - 360;
		if (rot.x > 180)
			rot.x = rot.x - 360;
			
		rot.y *= 1.1f;
		
		float result = potenciometer.value;
		result *= 10;
		if (result < 7) {
			result = result * 10 / 7;
		} else {			
			float diff = (result - 7);
			result = 10 + diff;

			if (rot.y < 0)
				rot.y -=  diff*1.5f;
			else
				rot.y += diff*1.5f;
			
			rot.x -= (diff*3);
		}
		if (rot.x > 0)
			rot.x = 0;

		float value = Mathf.Lerp(10, 70, (float)result/10);
		Ball newBall = Instantiate (realBall, ball.transform.localPosition, Quaternion.Euler (rot),container);
		newBall.Init (value);
		StartCoroutine (DoTransition ());
	}
	IEnumerator DoTransition()
	{
		yield return new WaitForSeconds (1.5f);
		yield return StartCoroutine(vrCameraFade.BeginFadeOut(1, false));
		print ("Done");
		yield return new WaitForSeconds (0.5f);
		Events.OnStartAgain ();
		state = states.ON_AIMING;
		yield return new WaitForSeconds (0.2f);
		yield return StartCoroutine(vrCameraFade.BeginFadeIn(1, false));

	}
}
