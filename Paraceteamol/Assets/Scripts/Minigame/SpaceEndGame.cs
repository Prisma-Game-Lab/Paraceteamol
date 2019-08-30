using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEndGame : MonoBehaviour
{
	public GameObject WinScreen;
	public GameObject LoseScreen;

	private void FixedUpdate()
	{
		if (!GameObject.FindGameObjectWithTag("Enemy"))
			WinScreen.SetActive(true);
		else if (!GameObject.FindGameObjectWithTag("Player"))
			LoseScreen.SetActive(false);

	}
}
