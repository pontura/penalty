using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action OnShoot = delegate { };
	public static System.Action Goal = delegate { };
	public static System.Action BallCatched = delegate { };
	public static System.Action OnStartAgain = delegate { };
	public static System.Action<bool> OnShowPotenciometer = delegate { };
	public static System.Action<string, bool> OnShowResult = delegate { };
	public static System.Action<float> OnGoalKeeperThrow = delegate { };



}
