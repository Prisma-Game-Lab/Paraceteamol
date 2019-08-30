using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	public float Speed = 10;
	public bool EnemyShot;

	private Transform _obj;
	private Vector3 _dir;

	void Start()
	{
		_obj = this.gameObject.transform;
	}

	private void FixedUpdate()
	{
		if (EnemyShot)
			_dir = new Vector3(0, -1, 0);
		else
			_dir = new Vector3(0, 1, 0);

		_obj.transform.position += (_dir * Speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (EnemyShot)
		{
			if (col.gameObject.tag == "Player")
			{
				Destroy(col.gameObject);
				Destroy(this.gameObject);
			}
		}
		else
		{
			if (col.gameObject.tag == "Enemy")
			{
				Destroy(col.gameObject);
				Destroy(this.gameObject);
			}
		}
	}
}
