using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{
	[Tooltip("Checks if it will be a vertical check (up/down), or horizontal check (left/right)")]
	public bool VerticalCheck = false;

	private BallPhysics _ballPhysicsScript;

	private void Start()
	{
		_ballPhysicsScript = GetComponentInParent<BallPhysics>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Ground")
		{
			if (VerticalCheck)
				_ballPhysicsScript.Direction.y = -_ballPhysicsScript.Direction.y;
			else
				_ballPhysicsScript.Direction.x = -_ballPhysicsScript.Direction.x;
		}
	}
}
