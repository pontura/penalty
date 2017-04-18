using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAsset : MonoBehaviour {

	public GameObject asset;

	void Start () {
		Events.OnShoot += OnShoot;
		Events.OnStartAgain += OnStartAgain;
	}
	void OnShoot () {
		asset.SetActive (false);
	}
	void OnStartAgain()
	{
		asset.SetActive (true);
	}
}
