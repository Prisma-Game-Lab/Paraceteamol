using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[Tooltip("Velocidade com que a bola é lançada quando está com o player")]
	public float Speed = 20;
	[HideInInspector]
	public Rigidbody2D Rb;
	
	void Start()
	{
		//Rb.velocity = transform.up * Speed;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<AimController>().IsPulling)
		{
			//AimScript.HasBall = true;
			Destroy(this);
		}
	}


}
