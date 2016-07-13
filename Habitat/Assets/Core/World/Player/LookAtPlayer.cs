using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour
{
	public Player target;
	public float walkspeed = 1f;
	public float acceleration = 2f;
	private float vel;
	private Vector2 axis;

	void FixedUpdate () 
	{
		Move ();
	}

	void Move()
	{
		axis.Set (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector3 dir = (transform.forward * axis.y + transform.right * axis.x).normalized;
		vel = Mathf.MoveTowards (vel, walkspeed*axis.sqrMagnitude, acceleration*Time.deltaTime);
		transform.position += dir * vel * Time.deltaTime;
	}
}