using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
	public float MovementSpeed = 10;

	private float _horizontal = 0;
	private Transform _obj;

	private void Start()
	{
		_obj = this.gameObject.transform;
	}

	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			_horizontal = -1;
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			_horizontal = 1;
		else
			_horizontal = 0;

		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;

		_obj.transform.position += tempVect;
	}
}
