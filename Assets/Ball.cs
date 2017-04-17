using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed;
	public Rigidbody rb;

	public void Init(float _speed)
	{
		this.speed = _speed;
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}
}
