using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class ResultsManager : MonoBehaviour {

	public List<bool> penalesPateados;

	public results result;
	public enum results
	{
		NONE,
		GOL,
		CATCHED,
		OUT,
		PALO
	}

	void Start () {
		Events.BallCatched += BallCatched;
		Events.Goal += Goal;
		Events.OnStartAgain += OnStartAgain;
		Events.OnPalo += OnPalo;
		Events.OnGameOver += OnGameOver;
	}
	void OnDisable () {
		Events.BallCatched -= BallCatched;
		Events.Goal -= Goal;
		Events.OnStartAgain -= OnStartAgain;
		Events.OnPalo -= OnPalo;
		Events.OnGameOver -= OnGameOver;
	}
	public bool isReady()
	{
		if(penalesPateados.Count >= 3)
			return true;
		return false;
	}
	void OnGameOver()
	{
		penalesPateados.Clear ();
	}
	void OnIntroScreen()
	{
		OnStartAgain ();
	}
	void OnStartAgain()
	{
		result = results.NONE;
	}
	void OnPalo()
	{
		
		result = results.PALO;
	}
	void Goal()
	{
		result = results.GOL;
	}
	void BallCatched(Ball ball)
	{
		result = results.CATCHED;
	}
	public string GetResult()
	{
		switch (result) {
			case results.GOL:
				penalesPateados.Add (true);
				return "gol";
			case results.CATCHED:
				penalesPateados.Add (false);
				return "atajada";
			case results.PALO:
				penalesPateados.Add (false);
				return "palo";
		}
		penalesPateados.Add (false);
		return "afuera";
	}
}
