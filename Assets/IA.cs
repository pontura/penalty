using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {

	public GameObject goalKeeperArea;

	void Start()
	{
		Events.OnStartAgain += OnStartAgain;
		goalKeeperArea.SetActive (false);
	}
	void OnDestroy()
	{
		Events.OnStartAgain -= OnStartAgain;
	}
	public void Calculate(Vector3 rot, float force)
	{		

		//force 10 es la mejor!
		int rand = Random.Range (0, (int)force);
		bool ataja = false;

		////ATAJA:
		if (force > 9.5f && force < 10.5f) {
			rand = Random.Range (0, 100);
			if (rand < 10) {
				ataja = true;
				print ("ATAJO DE PEDO. en perfect! con force: " + force + " rand: " + rand  + "> 10");
			} else {
				ataja = false;
				print ("GOL perfect! con force: " + force);
			}
		} else if (force < 4) {	
			ataja = true;
			print ("Atajo por patear sin fuerza " + force);
		} else if (rand < 3) {
			ataja = true;
			print ("Ataja force:" + force + " __ rand: " + rand + " < 3");
		} else {
			ataja = false;
			print ("GOL comun: force" + force + "  __ rand: " + rand);
		}

		float throwTo = rot.y / 4;
		if (ataja)
			CatchBall (throwTo);
		else
			ThrowOtherSide (throwTo*-1);
	}
	void CatchBall(float _x)
	{
		goalKeeperArea.SetActive (true);
		Events.OnGoalKeeperThrow (_x);
	}
	void ThrowOtherSide(float _x)
	{
		if (Mathf.Abs (_x) < 2) {
			if (_x > 0)
				_x += 2;
			else
				_x += -2;
		}
		Events.OnGoalKeeperThrow (_x);
	}
	void OnStartAgain()
	{
		goalKeeperArea.SetActive (false);
	}
}
