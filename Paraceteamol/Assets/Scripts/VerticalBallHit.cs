using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBallHit : MonoBehaviour
{
	private BallPhysics _ballPhysicsScript;

	private void Start()
	{
		_ballPhysicsScript = GetComponentInParent<BallPhysics>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Wall")
		{
			_ballPhysicsScript.Direction.y = -_ballPhysicsScript.Direction.y;
		}
	}
}
