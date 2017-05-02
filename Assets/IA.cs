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
		bool vaMuyLento = false;

		////ATAJA:
		if (force > 9.5f && force < 10.5f) {
			rand = Random.Range (0, 100);
			if (rand < 20) {
				ataja = true;
				Debug.Log ("ATAJO DE PEDO. en perfect! con force: " + force + " rand: " + rand  + "> 10");
			} else if(rot.x<13 && rot.x>-13 && Random.Range (0, 100)<33)
			{
				ataja = true;
				Debug.Log ("ATAJO por patear al centro. en perfect! con force: " + force + " rand: " + rand  + "> 10");
			} else {
				ataja = false;
				Debug.Log ("GOL perfect! con force: " + force);
			}
		} else if (force < 3) {	
			ataja = true;
			Debug.Log ("Atajo por patear sin fuerza " + force);
			vaMuyLento = true;
		} else if (force < 4 && Random.Range (0, 100)<80) {	
			ataja = true;
			Debug.Log ("Atajo por patear sin fuerza " + force);
			vaMuyLento = true;
		} else if (rand < 3) {
			ataja = true;
			Debug.Log ("Ataja force:" + force + " __ rand: " + rand + " < 3");
		} else if(rot.x<14 && rot.x>-14 && Random.Range (0, 100)<35)
		{
			ataja = true;
			Debug.Log ("ATAJO por patear al centro. con force: " + force + " rand: " + rand  + "> 10");
		} else {
			ataja = false;
			Debug.Log ("GOL comun: force" + force + "  __ rand: " + rand);
		}

		float throwTo = rot.y / 2;
		Vector2 ballPos = new Vector2 (throwTo, rot.x);

		if (Game.Instance.gameManager.siempreAtaja)
			ataja = true;

		if (vaMuyLento) {
			ballPos.y = 0;
		}
		//ataja:
		if (ataja) {
			goalKeeperArea.SetActive (true);
			Events.OnGoalKeeperThrow (ballPos, false);
		}
		// se tira para el otro lado:
		else {
			Events.OnGoalKeeperThrow (ballPos, true);
		}


	}
	void OnStartAgain()
	{
		goalKeeperArea.SetActive (false);
	}
}
