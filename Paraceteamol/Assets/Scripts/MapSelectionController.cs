using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionController : MonoBehaviour
{
	public GameObject[] ButtonList;
	[Space]
	public GameObject ButtonSelector;

	private GameObject[] _btnList;
	private int _btnQty;
	private int _currentNum = 0;
	private bool _canSelect = true;

	private void Start()
	{
		_btnList = ButtonList;
		_btnQty = _btnList.Length;
		ButtonSelector.transform.position = _btnList[0].transform.position;
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
			_currentNum = _btnList.Length - 1;
		else if (_currentNum > _btnList.Length - 1)
			_currentNum = 0;

		ButtonSelector.transform.position = Vector3.Lerp(ButtonSelector.transform.position,
			_btnList[_currentNum].transform.position, 1f);
	}
}
