using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string SelectionSound;
	public GameObject[] ButtonList;
	[Space]
	public GameObject ButtonSelector;

	private int _currentNum = 0;
	private bool _canSelect = true;
	private string _horizontal = "pall_ps4_horizontal";
	private string _confirm = "pall_ps4_confirm";

	private void Start()
	{
		ButtonSelector.transform.position = ButtonList[0].transform.position;
	}


	private void FixedUpdate()
	{
		if (Input.GetAxis(_horizontal) > 0 && _canSelect)
		{
            FMODUnity.RuntimeManager.PlayOneShot(SelectionSound);
			_currentNum++;
			_canSelect = false;
		}
		else if (Input.GetAxis(_horizontal) < 0 && _canSelect)
		{
            FMODUnity.RuntimeManager.PlayOneShot(SelectionSound);

            _currentNum--;
			_canSelect = false;
		}
		else if (Input.GetAxis(_horizontal) == 0)
		{
			_canSelect = true;
		}

		if (_currentNum < 0)
			_currentNum = ButtonList.Length - 1;
		else if (_currentNum > ButtonList.Length - 1)
			_currentNum = 0;

		ButtonSelector.transform.position = ButtonList[_currentNum].transform.position;

		if (Input.GetButton(_confirm))
			BtnClick(_currentNum);
	}

	private void BtnClick(int curNum)
	{
		ButtonList[curNum].GetComponent<Button>().onClick.Invoke();
	}
}
