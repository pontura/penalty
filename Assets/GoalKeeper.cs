using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour {
	
	private GoalKeeerAnimations anim;
	public ballPos ballPosition;
	public Transform ballContainer;

	public enum ballPos
	{
		UP,
		MIDDLE,
		DOWN
	}

	public states state;
	public enum states
	{
		IDLE,
		MOVEING,
		DONE
	}
	private Vector3 moveTo;
	private Vector3 initialPosition;
	void Start () {
		anim = GetComponent<GoalKeeerAnimations> ();
		state = states.IDLE;
		Events.OnGoalKeeperThrow += OnGoalKeeperThrow;
		Events.OnStartAgain += OnStartAgain;
		Events.BallCatched += BallCatched;
		initialPosition = transform.localPosition;
	}
	void OnDestroy () {
		state = states.IDLE;
		Events.OnGoalKeeperThrow -= OnGoalKeeperThrow;
		Events.BallCatched -= BallCatched;
		Events.OnStartAgain -= OnStartAgain;
	}
	void BallCatched(Ball ball)
	{
		if (anim.direction == GoalKeeerAnimations.directions.MID_CENTER) {
			ball.transform.GetComponent<Rigidbody> ().isKinematic = true;
			ball.transform.SetParent (ballContainer);
			ball.transform.localPosition = Vector3.zero;
		}
	}
	void ResetBallIfHasOne()
	{
		Ball ball = ballContainer.gameObject.GetComponentInChildren<Ball> ();
		if (ball) {
			ball.transform.localPosition = new Vector3 (1000, 1000, 0);
		}
	}
	void Update () {
		if (state == states.MOVEING) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, moveTo, 0.02f);
		}
	}
	void OnGoalKeeperThrow(Vector2 pos, bool goal)
	{
		print ("____" + pos);

		if (pos.y < -14)
			ballPosition = ballPos.UP;
		else if (pos.y <-7)
			ballPosition = ballPos.MIDDLE;
		else
			ballPosition = ballPos.DOWN;


		if (   pos.x > 16 
			|| pos.x < -16 
		) {
			SeQuedaParadoYMira (pos.x);
			return;
		} 
		
		this.moveTo = initialPosition; 

		if (!goal) {
			moveTo.x = pos.x;
			if (ballPosition == ballPos.UP && pos.x > 0)
				anim.JumpTo (GoalKeeerAnimations.directions.LEFT_TOP, false);
			else if (ballPosition == ballPos.UP && pos.x < 0)
				anim.JumpTo (GoalKeeerAnimations.directions.RIGHT_TOP, false);
			else if (ballPosition == ballPos.DOWN && pos.x > 0)
				anim.JumpTo (GoalKeeerAnimations.directions.LEFT_BOTTOM, false);
			else if (ballPosition == ballPos.DOWN && pos.x < 0)
				anim.JumpTo (GoalKeeerAnimations.directions.RIGHT_BOTTOM, false);
			else if ( pos.x <2.5f && pos.x >-2.5f)
				anim.JumpTo (GoalKeeerAnimations.directions.MID_CENTER, false);
		}


		// no antaja:
		else {	
			moveTo.x = (pos.x * -1);
		}
		this.state = states.MOVEING;
		Invoke ("Reset", 0.5f);
	}
	void SeQuedaParadoYMira(float _x)
	{
		print ("PARADO");
		if (_x>0)
			anim.JumpTo (GoalKeeerAnimations.directions.LEFT_MID, false);
		else if (_x<0)
			anim.JumpTo (GoalKeeerAnimations.directions.RIGHT_MID, false);
		state = states.DONE;
	}
	void Reset()
	{
		state = states.DONE;
	}
	void OnStartAgain()
	{
		ResetBallIfHasOne ();
		state = states.IDLE;
		transform.localPosition = initialPosition;
	}
}
