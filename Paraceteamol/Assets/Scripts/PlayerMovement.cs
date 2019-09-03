using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public float MovementSpeed = 10;
	public float JumpHeight = 1;
	public float GravitySpeedModifier = 1;

	private float _horizontal = 0;
	private Transform _obj;
	private Rigidbody2D _rb;

	private void Start()
	{
		_obj = this.gameObject.transform;
		_rb = this.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		_rb.AddForce(new Vector2(0, -10 * GravitySpeedModifier));

        _horizontal = Input.GetAxis("Horizontal"); // Pega as configurações do projeto e usa como controle

		/*if (Input.GetKey(KeyCode.A))
			_horizontal = -1;
		else if (Input.GetKey(KeyCode.D))
			_horizontal = 1;
		else
			_horizontal = 0;*/

		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;

		_obj.transform.position += tempVect;

		Debug.DrawRay(transform.position, Vector2.down, Color.red);
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Jump"))
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, this.GetComponent<Collider2D>().bounds.extents.y + 0.01f), Vector2.down, 0.1f);

			if (hit && hit.transform.tag == "Ground")
			{
				_rb.AddForce(new Vector2(0, 1) * JumpHeight * 10);
			}
		}
	}
}
