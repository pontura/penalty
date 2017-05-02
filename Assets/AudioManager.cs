using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource music;
	public AudioSource sfx;

	public AudioClip fondo;
	public AudioClip fondo_ui;

	public AudioClip patear;
	public AudioClip atajada;
	public AudioClip pitada;
	public AudioClip palo;

	public AudioClip potenciometer1;
	public AudioClip potenciometer2;

	public AudioClip abucheo;
	public AudioClip gol;

	bool bola_paso_algo;

	void Start () {
		Events.OnStartAgain += OnStartAgain;
		Events.OnShowResult += OnShowResult;
		Events.OnPalo += OnPalo;
		Events.BallCatched += BallCatched;
		Events.Goal += Goal;
		Events.OnKick += OnKick;
		Events.OnPotenciometerChangeDirection += OnPotenciometerChangeDirection;
	}

	void OnStartAgain () {
		music.clip = fondo;
		music.Play ();
	}
	void OnShowResult(string s, bool isOn) {
		if(isOn)
		{
			music.clip = fondo_ui;
			music.Play ();
		}
	}

	void OnKick () {
		sfx.clip = patear;
		sfx.Play ();
		bola_paso_algo = false;
		Invoke ("CheckIfNoPasoNadaConBola", 0.25f);
	}
	void CheckIfNoPasoNadaConBola()
	{
		if (!bola_paso_algo)
			Abucheo ();
	}
	void OnPalo () {
		sfx.clip = palo;
		sfx.Play ();
		Invoke("Abucheo", 0.2f);
		bola_paso_algo = true;
	}
	void Abucheo()
	{
		sfx.clip = abucheo;
		sfx.Play ();
	}
	void BallCatched (Ball ball) {
		bola_paso_algo = true;
		Abucheo ();
	}
	void Goal () {
		bola_paso_algo = true;
		sfx.clip = gol;
		sfx.Play ();
	}
	void OnPotenciometerChangeDirection(bool up)
	{
		if (up)
			sfx.clip = potenciometer1;
		else
			sfx.clip = potenciometer2; 
		sfx.Play ();
	}
}
