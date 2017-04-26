using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class SkyboxManager : MonoBehaviour {

	public Material stadium;
	public Material ui;
	public VRCameraFade vrCameraFade;

	void Start () {
		RenderSettings.skybox = ui;
		Events.OnShowResult += OnShowResult;
		Events.OnRestart += OnRestart;
	}
	void OnDestroy()
	{
		Events.OnShowResult -= OnShowResult;
		Events.OnRestart -= OnRestart;
	}
	void OnShowResult(string s, bool showIt)
	{
		if (showIt) {
			RenderSettings.skybox = ui;
			StartCoroutine(vrCameraFade.BeginFadeIn(0.5f, false));
		}
	}
	void OnRestart()
	{
		StartCoroutine (Restart());
	}
	IEnumerator Restart()
	{
		yield return StartCoroutine(vrCameraFade.BeginFadeOut(0.5f, false));
		RenderSettings.skybox = stadium;
		Events.OnStartAgain ();
	}
	
}
