using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
	public GameObject BackBtn;

	private string _back = "pall_ps4_back";

	private void Update()
	{
		if (Input.GetButton(_back))
		{
			BackBtn.GetComponent<Button>().onClick.Invoke();
		}
	}

	public void BackToMainMenu(string SceneName)
	{
		SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
	}
}
