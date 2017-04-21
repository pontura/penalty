using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour {

	public Ball[] balls;
	public Ball realBall;
	public Transform container;
	private int id = 0;
	public int totalBalls = 5;

	void Start () {
		balls = new Ball[totalBalls];
		for (int a = 0; a < totalBalls; a++) {
			Ball newBall = Instantiate (realBall);
			newBall.transform.SetParent(container);
			newBall.gameObject.SetActive (false);
			balls [a] = newBall;
		}
	}
	
	public void Init(Vector3 pos, Quaternion rot, float force)
	{
		Ball newBall = balls [id];
		newBall.gameObject.SetActive (true);
		newBall.transform.position = pos;
		newBall.transform.rotation = rot;
		newBall.transform.SetParent(container);

		newBall.Init (force);
		id++;
		if (id == balls.Length)
			id = 0;
	}
}
