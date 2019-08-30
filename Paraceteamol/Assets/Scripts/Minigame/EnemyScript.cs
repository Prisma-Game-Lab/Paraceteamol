using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	public float ShotTime = 1;
	public GameObject Projectile;
	[Tooltip("Chance, in percents, of the enemy shooting.")] [Range(0, 100)] public int ChanceToShoot = 10;

	private bool _shotCheck = true;
	private float _randomNumber;

	private IEnumerator WaitToShoot()
	{
		yield return new WaitForSeconds(ShotTime);
		if (Random.Range(0, 100) < ChanceToShoot)
			Instantiate(Projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
		_shotCheck = true;
	}

	private void FixedUpdate()
	{
		if (_shotCheck)
		{

			StartCoroutine(WaitToShoot());

			_shotCheck = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
			Destroy(col.gameObject);
	}
}
