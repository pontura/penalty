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
		RIGHT_BOTTOM
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
		anim.Play ("idle");
	}
	public void JumpTo(directions dir, bool isGoal)
	{
		this.direction = dir;
		print("JumpTo : " + dir);
		string animName = "";
		switch (dir) {
		case directions.LEFT_TOP:
			animName = "izq_arriba_no";
			break;
		case directions.RIGHT_TOP:
			animName = "der_arriba_si";
			break;
		case directions.LEFT_BOTTOM:
			animName = "izq_abajo_no";
			break;
		case directions.RIGHT_BOTTOM:
			animName = "der_arriba_si";
			break;
		case directions.LEFT_MID:
			animName = "center_izq_no";
			break;
		case directions.RIGHT_MID:
			animName = "center_izq_no";
			break;
		case directions.MID_CENTER:
			anim.Play ("centro_medio_si");
			break;
		}
		if (animName != "") {
			anim [animName].speed = 3f;
			anim.Play (animName);
		}
	}
}
