using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class ResultsManager : MonoBehaviour {

	public results result;
	public enum results
	{
		NONE,
		GOL,
		CATCHED,
		OUT
	}

	void Start () {
		Events.BallCatched += BallCatched;
		Events.Goal += Goal;
		Events.OnStartAgain += OnStartAgain;
	}
	void OnDisable () {
		Events.BallCatched -= BallCatched;
		Events.Goal -= Goal;
		Events.OnStartAgain -= OnStartAgain;
	}
	void OnStartAgain()
	{
		result = results.NONE;
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
				return "GOL";
			case results.CATCHED:
				return "Atajada!";
		}
		return "Fuera!";
	}
}
