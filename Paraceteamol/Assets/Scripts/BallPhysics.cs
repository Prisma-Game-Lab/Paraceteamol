using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
	public float StartSpeed = 10;
	//[HideInInspector]
	public Vector2 Direction = Vector2.one;

	private bool _playerIsPulling;

	private void FixedUpdate()
	{
		_playerIsPulling = GetComponent<BulletScript>().PlayerIsPulling;
		if (_playerIsPulling)
		{
			//Direction = Vector2.zero;
			GetComponent<Rigidbody2D>().simulated = false;
		}
		else
		{
			GetComponent<Rigidbody2D>().simulated = true;
		}

		transform.Translate(Direction * StartSpeed * Time.deltaTime);
	}
}
