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
	private GoalKeeerAnimations.directions[] directions;

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

		directions = new GoalKeeerAnimations.directions[7];
		directions[0] = GoalKeeerAnimations.directions.LEFT_TOP;
		directions[1] = GoalKeeerAnimations.directions.RIGHT_TOP;
		directions[2] = GoalKeeerAnimations.directions.LEFT_BOTTOM;
		directions[3] = GoalKeeerAnimations.directions.RIGHT_BOTTOM;
		directions[4] = GoalKeeerAnimations.directions.LEFT_MID;
		directions[5] = GoalKeeerAnimations.directions.RIGHT_MID;
		directions[6] = GoalKeeerAnimations.directions.MID_CENTER;
	}
	void OnDestroy () {
		state = states.IDLE;
		Events.OnGoalKeeperThrow -= OnGoalKeeperThrow;
		Events.BallCatched -= BallCatched;
		Events.OnStartAgain -= OnStartAgain;
	}
	void BallCatched(Ball ball)
	{
		if(anim.BallCatched (ball.transform.position))
		{
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
	GoalKeeerAnimations.directions dir;
	void OnGoalKeeperThrow(Vector2 pos, bool goal)
	{		

		if (pos.y < -14)
			ballPosition = ballPos.UP;
		else if (pos.y <-7)
			ballPosition = ballPos.MIDDLE;
		else
			ballPosition = ballPos.DOWN;

		print ("____" + pos +  "  ballPosition: " + ballPosition);

		if (   pos.x > 16 
			|| pos.x < -16 
		) {
			SeQuedaParadoYMira (pos.x);
			return;
		} 
		
		this.moveTo = initialPosition; 

		float centerX = 4f;
			

		if (pos.x > 0 && pos.x < centerX) {
			if (pos.y < -20)
				dir = GoalKeeerAnimations.directions.CENTER_TOP_LEFT;
			else
				dir = GoalKeeerAnimations.directions.MUEVE_IZQ;
		} else if (pos.x < 0 && pos.x > -centerX) {
			if (pos.y < -20)
				dir = GoalKeeerAnimations.directions.CENTER_TOP_RIGHT;
			else
				dir = GoalKeeerAnimations.directions.MUEVE_DER;
		}
		else if (ballPosition == ballPos.UP && pos.x > centerX)
			dir = GoalKeeerAnimations.directions.LEFT_TOP;
		else if (ballPosition == ballPos.UP && pos.x < -centerX)
			dir = GoalKeeerAnimations.directions.RIGHT_TOP;

		else if (ballPosition == ballPos.DOWN && pos.x > centerX)
			dir = GoalKeeerAnimations.directions.LEFT_BOTTOM;
		else if (ballPosition == ballPos.DOWN && pos.x < -centerX)
			dir = GoalKeeerAnimations.directions.RIGHT_BOTTOM;

		else if (ballPosition == ballPos.MIDDLE && pos.x >centerX)
			dir = GoalKeeerAnimations.directions.LEFT_MID;
		else if (ballPosition == ballPos.MIDDLE && pos.x <-centerX)
			dir = GoalKeeerAnimations.directions.RIGHT_MID;


		
		//Si ataja:
		if (!goal) {
			moveTo.x = pos.x*1.1f;
		}
		// no antaja:
		else {	
			dir = GetRandomTiradaSinAtajar(pos);
			if (dir == GoalKeeerAnimations.directions.LEFT_BOTTOM 
				|| dir == GoalKeeerAnimations.directions.LEFT_MID
				|| dir == GoalKeeerAnimations.directions.LEFT_TOP) {
				moveTo.x = Random.Range(7,10);
			} else {
				moveTo.x = - Random.Range(7,10);
			}
		}
		anim.JumpTo (dir, false);
		this.state = states.MOVEING;
		Invoke ("Reset", 0.5f);
	}
	GoalKeeerAnimations.directions GetRandomTiradaSinAtajar(Vector2 pos)
	{
		GoalKeeerAnimations.directions diferentDir = directions [Random.Range (0, directions.Length - 1)];
		if (diferentDir != dir)
			return diferentDir;
		else
			return GetRandomTiradaSinAtajar (pos);
	}
	void SeQuedaParadoYMira(float _x)
	{
		print ("PARADO");
		if (_x>0)
			anim.JumpTo (GoalKeeerAnimations.directions.CENTER_MIRA_IZQ, false);
		else if (_x<0)
			anim.JumpTo (GoalKeeerAnimations.directions.CENTER_MIRA_DER, false);
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
