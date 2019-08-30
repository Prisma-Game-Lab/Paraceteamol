using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
	public float StartSpeed = 1;
	[HideInInspector]
	public  Vector2 Direction;

	private void Start()
	{
		Direction = Vector2.one.normalized;    // Direction (1,1) normalized - going top right
	}

	private void FixedUpdate()
	{
		transform.Translate(Direction * StartSpeed * Time.deltaTime);
	}
}
