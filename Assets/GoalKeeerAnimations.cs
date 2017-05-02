using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeerAnimations : MonoBehaviour {

	public Animation anim;

	public directions direction;
	public enum directions
	{
		LEFT_TOP,
		LEFT_MID,
		LEFT_BOTTOM,
		MID_TOP,
		MID_CENTER,
		MID_BOTTOM,
		RIGHT_TOP,
		RIGHT_MID,
		RIGHT_BOTTOM,
		CENTER_MIRA_IZQ,
		CENTER_MIRA_DER,
		CENTER_TOP_LEFT,
		CENTER_TOP_RIGHT,
		MUEVE_DER,
		MUEVE_IZQ
	}

	void Start () {
		Events.OnStartAgain += OnStartAgain;
		Idle ();
	}
	void OnStartAgain()
	{
		Idle ();
	}
	public void Idle()
	{
		anim.Play ("CINEMA_4D_idle");
	}
	public void JumpTo(directions dir, bool isGoal)
	{
		this.direction = dir;
		string animName = "CINEMA_4D_";
		float speed = 2f;
		switch (dir) {


		case directions.LEFT_TOP:
			animName += "izq_arriba_no";
			break;		
		case directions.LEFT_MID:
			animName += "izq_med_no";
			break;
		case directions.LEFT_BOTTOM:
			animName += "izq_abajo_no";
			break;



		case directions.RIGHT_TOP:
			animName += "der_arriba_no";
			break;
		case directions.RIGHT_MID:
			animName += "der_med_no";
			break;
		case directions.RIGHT_BOTTOM:
			animName += "der_abajo_no";
			break;	


		case directions.CENTER_TOP_LEFT:
			animName += "centro_arriba_no_izq";
			break;	
		case directions.CENTER_TOP_RIGHT:
			animName += "centro_arriba_no_der";
			break;	


		//case directions.MID_TOP:
		//case directions.MID_CENTER:
		//case directions.MID_BOTTOM:
			//return;
			//break;



		case directions.CENTER_MIRA_IZQ:
			animName += "centro_mira_izq";
			break;
		case directions.CENTER_MIRA_DER:
			animName += "centro_mira_der";
			break;



		case directions.MUEVE_DER:
			animName += "derecha";
			break;
		case directions.MUEVE_IZQ:
			animName += "izquierda";
			break;


		}


		if (animName != "") {
			anim [animName].speed = speed;
			anim.Play (animName);
		}
	}
	public bool BallCatched(Vector3 pos)
	{
		//print ("BallCatched " + pos + "  " + direction);
		if (direction == directions.MUEVE_DER
			|| direction == directions.MUEVE_IZQ) {

			string animName = "CINEMA_4D_";
			float speed = 1.5f;

			print ("_________Y: " + pos.y);

			if(pos.y<1)
				animName += "centro_abajo_si";
			else if(pos.y<2)
				animName += "centro_med_si";
			else
				animName += "centro_arriba_si";
			
			anim [animName].speed = speed;
			anim.Play (animName);
			return true;
		}
		return false;
	}
}
