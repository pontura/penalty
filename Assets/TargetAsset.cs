using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAsset : MonoBehaviour {

	public GameObject asset;
	public GameObject asset2;

	void Start () {
		Events.OnShoot += OnShoot;
		Events.OnStartAgain += OnStartAgain;
	}
	void OnShoot () {
		asset.SetActive (false);
		asset2.SetActive (true);
	}
	void OnStartAgain()
	{
		asset2.SetActive (false);
		asset.SetActive (true);
	}
}
