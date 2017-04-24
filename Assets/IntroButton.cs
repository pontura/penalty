using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VRStandardAssets.Utils;

public class IntroButton : VRInteractiveItem {

	private Action OnOver;

	public void Init(Action  _OnOver)
	{
		this.OnOver = _OnOver;
	}
	void Update()
	{
		if (IsOver) {
			OnOver();
		}
	}
}
