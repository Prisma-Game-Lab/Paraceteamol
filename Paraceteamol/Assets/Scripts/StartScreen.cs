using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
	public GameObject StartBtn;

	private void Update()
	{
		if (Input.GetButton("pall_ps4_confirm"))
		{
			StartBtn.GetComponent<Button>().onClick.Invoke();
		}
	}
}
