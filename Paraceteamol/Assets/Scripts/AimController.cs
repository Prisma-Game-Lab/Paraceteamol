using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AimController : MonoBehaviour
{
	public float Strenght = 5f;
	[Space]
	[Header("GameObjects")]
	[Tooltip("Colocar aqui os prefab da bola")] public GameObject BallPrefab;

	[HideInInspector]
	public bool IsPulling = false;
	[HideInInspector]
	public bool HasBall = false;

	private bool _playerOne;
	private float _horizontal;

	private void Start()
	{
		_playerOne = GetComponentInParent<PlayerMovement>().PlayerOne;
	}

	private void FixedUpdate()
	{
		if (Input.GetAxis(_playerOne ? "p1_horizontal" : "p2_horizontal") > 0)
			_horizontal = 0;
		else if (Input.GetAxis(_playerOne ? "p1_horizontal" : "p2_horizontal") < 0)
			_horizontal = 180;

		if (Input.GetAxis(_playerOne ? "p1_vertical" : "p2_vertical") > 0)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
		else if (Input.GetAxis(_playerOne ? "p1_vertical" : "p2_vertical") < 0)
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
		else
			transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal, 0));

		if (Input.GetButton(_playerOne ? "p1_fire1" : "p2_fire1"))
			IsPulling = true;
		else
			IsPulling = false;

		if (HasBall && Input.GetButtonDown(_playerOne ? "p1_fire2" : "p2_fire2"))
			ShootBall();
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball")
		{
			if (Input.GetButton(_playerOne ? "p1_fire1" : "p2_fire1"))
			{
				col.transform.position = Vector3.MoveTowards(col.transform.position, transform.position, Strenght);
				//col.GetComponent<BallPhysics>().Direction = transform.position;
			}
			else if (Input.GetButton(_playerOne ? "p1_fire2" : "p2_fire2"))
			{
				col.transform.position = Vector2.MoveTowards(col.transform.position, -transform.position, Strenght);
				col.GetComponent<BallPhysics>().Direction = new Vector2(-transform.position.x, -transform.position.y).normalized;
			}
		}
	}

	private void ShootBall()
	{
		// Criar a bola
		Debug.Log("shot ball");
		Instantiate(BallPrefab, transform.position, transform.rotation);
	}
}