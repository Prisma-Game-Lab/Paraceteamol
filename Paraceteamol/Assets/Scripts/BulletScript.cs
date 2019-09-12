using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[Tooltip("Velocidade com que a bola é lançada quando está com o player")]
	public float Speed = 20;

	[HideInInspector]
	public Rigidbody2D Rb;
	[HideInInspector]
	public bool PlayerIsPulling = false;
	

	private Vector2 _oldDirection;
	private Vector2 _oldPosition;

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			PlayerIsPulling = col.gameObject.GetComponentInChildren<AimController>().IsPulling;
			if (PlayerIsPulling)
			{
				Debug.Log(col.gameObject.name + " touched ball");

				//transform.position = col.gameObject.transform.position + new Vector3(0, 1, 0);

				//GetComponent<Rigidbody2D>().isKinematic = false;
				
				//_oldDirection = GetComponent<BallPhysics>().Direction;  // Get old direction
				//GetComponent<BallPhysics>().Direction = Vector2.zero;   // Set direction to zero.

				// O player não segura mais a bola
				//col.gameObject.GetComponentInChildren<AimController>().HasBall = true;
				//AimScript.HasBall = true;
				//Destroy(this.gameObject);
			}
			else
			{
				Debug.Log(col.gameObject.name + " released ball");
				//GetComponent<BallPhysics>().Direction = new Vector2(1, 0);
				//GetComponent<Rigidbody2D>().isKinematic = true;
			}
		}
	}
}
