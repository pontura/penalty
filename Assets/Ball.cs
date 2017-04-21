using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed;
	public Rigidbody rb;
	bool done;

	public void Init(float _speed)
	{
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		done = false;
		this.speed = _speed;
		rb.AddForce(transform.forward * speed, ForceMode.Impulse);
	}
	void OnTriggerEnter(Collider other)
	{
		if (done)
			return;
		if (other.tag == "goalCollider") {
			Vector3 newVel = rb.velocity;
			newVel.z /= 10;
			rb.velocity = newVel;
			Events.Goal ();
			done = true;
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (done)
			return;
		if (other.gameObject.tag == "goalKeeperArea") {
			Events.BallCatched (this);
			done = true;
		}
	}

}
