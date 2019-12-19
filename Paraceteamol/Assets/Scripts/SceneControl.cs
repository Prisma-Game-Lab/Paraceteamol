using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
	[Tooltip("Botão do teclado usado para resetar a scene.")]
	public KeyCode ResetKey;
	public CharacterSelection[] characterSelectionsScript;

	[Header("UI")]
	[SerializeField] private GameObject CharacterSelectionUI;
	[SerializeField] private GameObject PlacarUI;
	[SerializeField] private GameObject TimerUI;

	//private string _confirm = "pall_ps4_options";
	private bool HasSelecterChar = true;
	private bool aux = true;

	private void Start()
	{
		CharacterSelectionUI.SetActive(true);
		PlacarUI.SetActive(false);
		TimerUI.SetActive(false);
		Time.timeScale = 0f;
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(ResetKey))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		//if (Input.GetButton(_confirm) && HasSelecterChar)
		if (aux == true && characterSelectionsScript[0].hasChosen == true /*&& characterSelectionsScript[1].hasChosen == true && characterSelectionsScript[2].hasChosen == true && characterSelectionsScript[3].hasChosen == true*/)
		{
			//HasSelecterChar = false;
			CharacterSelectionUI.SetActive(false);
			PlacarUI.SetActive(true);
			TimerUI.SetActive(true);
			Time.timeScale = 1f;
			aux = false;
		}
	}

	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}