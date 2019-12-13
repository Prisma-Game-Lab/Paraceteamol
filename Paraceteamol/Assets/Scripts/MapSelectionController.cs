using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionController : MonoBehaviour
{
	public GameObject[] ButtonList;
	[Space]
	public GameObject ButtonSelector;

	private int _currentNum = 0;
	private bool _canSelect = true;

	private void Start()
	{
		ButtonSelector.transform.position = ButtonList[0].transform.position;
	}


	private void FixedUpdate()
	{
		string horizontal = "pall_ps4_horizontal";

		if (Input.GetAxis(horizontal) > 0 && _canSelect)
		{
			_currentNum++;
			_canSelect = false;
		}
		else if (Input.GetAxis(horizontal) < 0 && _canSelect)
		{
			_currentNum--;
			_canSelect = false;
		}
		else if (Input.GetAxis(horizontal) == 0)
		{
			_canSelect = true;
		}

		if (_currentNum < 0)
			_currentNum = ButtonList.Length - 1;
		else if (_currentNum > ButtonList.Length - 1)
			_currentNum = 0;

		ButtonSelector.transform.position = ButtonList[_currentNum].transform.position;
	}
}
