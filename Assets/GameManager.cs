using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class GameManager : MonoBehaviour {

	public bool siempreAtaja;
	public bool DEBUG;

	public ParticleSystem particles;
	public GameObject all;
	public GameObject mainCamera;
	public GameObject ball;
	public Ball realBall;
	public Potenciometer potenciometer;
	public VRCameraFade vrCameraFade;
	public VRInput m_VRInput;
	private ResultsManager resultsManager;
	private IntroScreen introScreen;
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
		WAITING,
		ON_AIMING,
		SHOOTING
	}
	private int offsetRotation = 50;
	private BallsManager ballsManager;

	void Start () {

		all.SetActive (false);

	#if UNITY_ANDROID
		siempreAtaja = false;
	#endif

		introScreen = GetComponent<IntroScreen> ();
		introScreen.OnIntroScreen ();
		ballsManager = GetComponent<BallsManager> ();
		ia = GetComponent<IA> ();
		resultsManager = GetComponent<ResultsManager> ();
		Events.OnShoot += OnShoot;
		m_VRInput.OnDown += OnDown;
		m_VRInput.OnUp += OnUp;
		Events.OnStartAgain += OnStartAgain;
		Events.Goal += Goal;
	}
	void Goal()
	{
		particles.gameObject.SetActive (true);
		particles.Play ();
		Invoke ("ResetParticles", 6);
	}
	void ResetParticles()
	{
		particles.Stop ();
	}
	void OnStartAgain()
	{
		ResetBall ();
		StartCoroutine (StartAgainReoutine ());
		particles.Stop ();
		particles.gameObject.SetActive (false);
	}
	void Update()
	{
		if (state == states.WAITING)
			return;
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
	private float timeStartedPressing = 0;
	void OnDown()
	{
		if (state == states.ON_AIMING) {
			Events.OnShowPotenciometer (true);
			timeStartedPressing = Time.time;
		}
	}
	void OnUp()
	{
		if (timeStartedPressing == 0)
			return;
		float timePassed = Time.time - timeStartedPressing;
		if (timePassed < 0.15f ) {
			Events.OnShowPotenciometer (false);
			return;
		}
		timeStartedPressing = 0;
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
		yield return StartCoroutine(vrCameraFade.BeginFadeOut(0.5f, false));
		SetFloors(false);
		Events.OnShowResult(resultsManager.GetResult(), true);
		yield return new WaitForSeconds (3);
		Events.OnIntroScreen ();
	}
	IEnumerator StartAgainReoutine()
	{
		ball.SetActive (true);
		yield return new WaitForSeconds (0.2f);
		state = states.ON_AIMING;
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
	void ResetBall()
	{
		ballsManager.Reset ();
	}
	void SetFloors(bool isOn)
	{
		if (isOn)
			all.SetActive (true);
		else
			all.SetActive (false);
	}
}
