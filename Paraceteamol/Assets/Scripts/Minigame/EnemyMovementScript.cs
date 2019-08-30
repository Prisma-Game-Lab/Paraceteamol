using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
	public float MovementSpeed = 10;

	private float _horizontal = 1;
	private Transform _obj;

	private void Start()
	{
		_obj = this.gameObject.transform;
	}

	private void FixedUpdate()
	{
		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;

		_obj.transform.position += tempVect;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Wall")
		{
			_horizontal = -_horizontal;

			this.gameObject.transform.position -= new Vector3(0, 1, 0);
		}
	}
}
