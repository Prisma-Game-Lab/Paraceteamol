using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRestrictor : MonoBehaviour
{
	public GameObject BallGameObject;

	private Vector3 _ballPosition;
	private Vector2 _ballVelocity;

	private void Start()
	{
		_ballPosition = BallGameObject.transform.position;
		_ballVelocity = BallGameObject.GetComponent<Rigidbody2D>().velocity;
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "Ball")
		{
			col.gameObject.transform.position = _ballPosition;
			BallGameObject.GetComponent<Rigidbody2D>().velocity = _ballVelocity;
		}
	}
}
