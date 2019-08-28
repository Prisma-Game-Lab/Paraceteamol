using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBallHit : MonoBehaviour
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
			_ballPhysicsScript.Direction.x = -_ballPhysicsScript.Direction.x;
		}
	}
}
