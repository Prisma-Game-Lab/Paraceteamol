using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
	[HideInInspector]
	public Transform FirePoint;
	[HideInInspector]
	public GameObject Bullet;
	[Tooltip(" Distância da mira")]
	public float MaxDistance = 10;
	[Tooltip("Força com que se puxa e empurra a bola")]
	public float strenght = 1;
	[HideInInspector]
	public bool HasBall;
	[HideInInspector]
	public bool IsPulling;

	//Algumas das variaveis precisam ser publicas para outro script poder pegar
	private BallPhysics _ballPhysics;
	private Rigidbody2D _ball;
	private GameObject _antiFirePoint;
	private bool _playerOne;

	void Update()
	{
		_playerOne = GetComponent<PlayerMovement>().PlayerOne;
		_antiFirePoint = GameObject.FindGameObjectWithTag("AntiPlayer");
		GameObject _ballRigidBody = GameObject.FindGameObjectWithTag("Ball");

		if (_ballRigidBody != null)
		{
			_ballPhysics = _ballRigidBody.GetComponent<BallPhysics>();
		}


		if (Input.GetButtonDown(_playerOne ? "p1_fire" : "p2_fire"))
		{
			PullRaycast();
			IsPulling = true;
		}

		else if (Input.GetButtonDown(_playerOne ? "p1_fire" : "p2_fire"))
		{
			PushRaycast();
			if (HasBall) //testa se o jogador tem a bola para poder lancar caso tenha
			{
				FreeBall();
			}
			HasBall = false;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			IsPulling = false;
		}
	}

	void FreeBall()
	{
		Instantiate(Bullet, FirePoint.position, FirePoint.rotation);    //cria a bola
	}

	void PullRaycast()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.up * MaxDistance);   //cria o raycast
		if (hitInfo)
		{
			Debug.DrawRay(FirePoint.position, FirePoint.up * MaxDistance, Color.red);   //feito apenas para testar na scene se o raycast esta atingindo o alvo
			_ball = hitInfo.transform.GetComponent<Rigidbody2D>();
			if (_ball != null && _ball.tag == "Ball")
			{
				_ball.transform.position = Vector2.MoveTowards(_ball.transform.position, transform.position, strenght);
				_ballPhysics.Direction = new Vector2(transform.position.x, transform.position.y).normalized;
			}
		}
	}

	void PushRaycast()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.up * MaxDistance);
		if (hitInfo)
		{
			Debug.DrawRay(FirePoint.position, FirePoint.up * MaxDistance);
			_ball = hitInfo.transform.GetComponent<Rigidbody2D>();
			if (_ball != null && _ball.tag == "Ball")
			{
				_ball.transform.position = Vector2.MoveTowards(_ball.transform.position, _antiFirePoint.transform.position, strenght);
				_ballPhysics.Direction = new Vector2(_antiFirePoint.transform.position.x, _antiFirePoint.transform.position.y).normalized;
			}
		}
	}
}
