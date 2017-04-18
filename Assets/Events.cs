using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action OnShoot = delegate { };
	public static System.Action OnStartAgain = delegate { };
	public static System.Action<bool> OnShowPotenciometer = delegate { };

}
