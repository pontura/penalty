using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	const string PREFAB_PATH = "Game";    
	static Game mInstance = null;
	public GameManager gameManager;

	public static Game Instance
	{
		get
		{
			return mInstance;
		}
	}
	void Awake()
	{
		if (!mInstance)
			mInstance = this;
		else
		{
			Destroy(this.gameObject);
			return;
		}
		DontDestroyOnLoad(this.gameObject);
		gameManager = GetComponent<GameManager> ();

	}
}
