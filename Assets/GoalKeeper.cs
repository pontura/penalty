using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour {

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
		state = states.IDLE;
		Events.OnGoalKeeperThrow += OnGoalKeeperThrow;
		Events.OnStartAgain += OnStartAgain;
		initialPosition = transform.localPosition;
	}
	void OnDestroy () {
		state = states.IDLE;
		Events.OnGoalKeeperThrow -= OnGoalKeeperThrow;
		Events.OnStartAgain -= OnStartAgain;
	}

	void Update () {
		if (state == states.MOVEING) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, moveTo, 0.2f);
		}
	}
	void OnGoalKeeperThrow(float _x)
	{
		this.moveTo = initialPosition; 
		moveTo.x = _x;
		this.state = states.MOVEING;
		Invoke ("Reset", 0.5f);
	}
	void Reset()
	{
		state = states.DONE;
	}
	void OnStartAgain()
	{
		state = states.IDLE;
		transform.localPosition = initialPosition;
	}
}
