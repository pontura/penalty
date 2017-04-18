using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class InputManager : MonoBehaviour {

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
		Events.OnShowPotenciometer (true);
	}
	void OnUp()
	{
		Events.OnShowPotenciometer (false);
		Events.OnShoot ();
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Events.OnShoot ();
		}
	}
}
