using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class GameManager : MonoBehaviour {

	public bool siempreAtaja;

	public GameObject floor;
	public GameObject mainCamera;
	public GameObject ball;
	public Ball realBall;
	public Potenciometer potenciometer;
	public VRCameraFade vrCameraFade;
	public VRInput m_VRInput;
	private ResultsManager resultsManager;
	private IA ia;


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
		GOL
	}
	private int offsetRotation = 50;
	private BallsManager ballsManager;

	void Start () {
		ballsManager = GetComponent<BallsManager> ();
		ia = GetComponent<IA> ();
		resultsManager = GetComponent<ResultsManager> ();
		Events.OnShoot += OnShoot;
		m_VRInput.OnDown += OnDown;
		m_VRInput.OnUp += OnUp;
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
	void OnDisable()
	{
		Events.OnShoot -= OnShoot;
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
			Invoke ("OnHidePotenciometer", 1);
			Events.OnShoot ();
		}
	}
	void OnHidePotenciometer()
	{
		Events.OnShowPotenciometer (false);
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

		StartCoroutine (DoTransition ());
	}
	IEnumerator DoTransition()
	{
		yield return new WaitForSeconds (0.70f);
		Shoot ();
		yield return new WaitForSeconds (0.05f);
		RealShoot ();
		yield return new WaitForSeconds (1.5f);
		yield return StartCoroutine(vrCameraFade.BeginFadeOut(1, false));
		SetFloors(false);
		Events.OnShowResult(resultsManager.GetResult(), true);
		yield return new WaitForSeconds (1);
		Events.OnStartAgain ();
		ball.SetActive (true);
		state = states.ON_AIMING;
		yield return new WaitForSeconds (0.2f);
		SetFloors(true);
		Events.OnShowResult("", false);
		yield return StartCoroutine(vrCameraFade.BeginFadeIn(1, false));

	}
	Vector3 rot;
	float force;
	void Shoot()
	{
		rot = ball.transform.eulerAngles;
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

		force = Mathf.Lerp(10, 70, (float)result/10);
		ia.Calculate(rot, (float)result);

	}
	void RealShoot()
	{
		ballsManager.Init (ball.transform.localPosition, Quaternion.Euler (rot), force);
		ball.SetActive (false);
	}
	void SetFloors(bool isOn)
	{
		if (isOn)
			transform.localPosition = new Vector3 (0, 0, 0);
		else
			transform.localPosition = new Vector3 (0, -1000, 0);
	}
}
