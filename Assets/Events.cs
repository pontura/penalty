using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action OnGameOver = delegate { };
	public static System.Action OnGameRestart = delegate { };

	public static System.Action OnIntroScreen = delegate { };
	public static System.Action OnShoot = delegate { };
	public static System.Action Goal = delegate { };
	public static System.Action OnKick = delegate { };
	public static System.Action OnShowTotalResult = delegate { };
	public static System.Action OnShowcongrats = delegate { };
	public static System.Action<Ball> BallCatched = delegate { };
	public static System.Action OnRestart = delegate { };
	public static System.Action OnStartAgain = delegate { };

	public static System.Action OnPalo = delegate { };
	public static System.Action<bool> OnShowPotenciometer = delegate { };
	public static System.Action<bool> OnPotenciometerChangeDirection = delegate { };
	public static System.Action<string, bool> OnShowResult = delegate { };
	public static System.Action<Vector2, bool> OnGoalKeeperThrow = delegate { };

	public static System.Action<bool> OnSplash = delegate { };

}
