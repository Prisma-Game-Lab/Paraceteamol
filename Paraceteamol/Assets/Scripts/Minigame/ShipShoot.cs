using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShoot : MonoBehaviour
{
	public GameObject Projectile;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(Projectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
		}
	}
}
