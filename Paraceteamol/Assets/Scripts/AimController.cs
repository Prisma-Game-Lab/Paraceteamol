using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AimController : MonoBehaviour
{
	public float Strenght = 5f;

	[HideInInspector]
	public bool IsPulling = false;

	private bool _playerOne;
	private float _horizontal;

	private void Start()
	{
		_playerOne = GetComponentInParent<PlayerMovement>().PlayerOne;
	}
	private void FixedUpdate()
	{if (Input.GetAxis(_playerOne ? "p1_horizontal" : "p2_horizontal") > 0)
			_horizontal = 0;
		else if (Input.GetAxis(_playerOne ? "p1_horizontal" : "p2_horizontal") < 0)
			_horizontal = 180;

		if (Input.GetAxis(_playerOne ? "p1_vertical" : "p2_vertical") > 0)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
		else if (Input.GetAxis(_playerOne ? "p1_vertical" : "p2_vertical") < 0)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
		else
			transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal, 0));
	}
	
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball" && (Input.GetButton(_playerOne ? "p1_fire" : "p2_fire")))
		{
			Debug.DrawRay(transform.position, col.transform.position - transform.position, Color.red);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, col.transform.position - transform.position);
			if (hit && hit.transform.tag == "Ball")
			{
				IsPulling = true;
				hit.transform.GetComponent<Rigidbody2D>().AddForce(transform.position.normalized);
			}

		}
	}
}