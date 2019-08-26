using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
	public float StartSpeed = 1;
	public float MaxSpeed = 50;

	private Vector2 _direction;
	
	private void Start()
	{
		_direction = Vector2.one.normalized;	// Direction (1,1) normalized - going top right
	}

	private void FixedUpdate()
	{
		transform.Translate(_direction * StartSpeed * Time.deltaTime);

	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Wall")
		{
			// make raycast so it can see if contact is horizontal or vertical.
			// horizontal contact inverts x direction
			// vertical contact inverts y direction
		}
	}
}
