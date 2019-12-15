using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
	[Tooltip("Botão do teclado usado para resetar a scene.")]
	public KeyCode ResetKey;
	public CharacterSelection[] characterSelectionsScript;

	[SerializeField] private GameObject CharacterSelectionUI;
	private string player1;
	private string _confirm = "pall_ps4_options";
	private bool HasSelecterChar = true;

	private void Start()
	{
		CharacterSelectionUI.SetActive(true);
		Time.timeScale = 0f;
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(ResetKey))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (Input.GetButtonDown(_confirm) && HasSelecterChar)
		{
			if (characterSelectionsScript[0].hasChosen == true && characterSelectionsScript[1].hasChosen == true && characterSelectionsScript[2].hasChosen == true && characterSelectionsScript[3].hasChosen == true)
			{
				//Spawnar o player 1 no local do player 1
				if (characterSelectionsScript[0].chosenChar == "Vovo")
				{
					
				}
				else if (characterSelectionsScript[0].chosenChar == "Fantasma")
				{

				}
				else if (characterSelectionsScript[0].chosenChar == "Tartaruga")
				{

				}
				else if (characterSelectionsScript[0].chosenChar == "Robo")
				{

				}

				//Spawnar o player 2 no local do player 2
				if (characterSelectionsScript[1].chosenChar == "Vovo")
				{

				}
				else if (characterSelectionsScript[1].chosenChar == "Fantasma")
				{

				}
				else if (characterSelectionsScript[1].chosenChar == "Tartaruga")
				{

				}
				else if (characterSelectionsScript[1].chosenChar == "Robo")
				{

				}

				//Spawnar o player 3 no local do player 3
				if (characterSelectionsScript[2].chosenChar == "Vovo")
				{

				}
				else if (characterSelectionsScript[2].chosenChar == "Fantasma")
				{

				}
				else if (characterSelectionsScript[2].chosenChar == "Tartaruga")
				{

				}
				else if (characterSelectionsScript[2].chosenChar == "Robo")
				{

				}

				//Spawnar o player 4 no local do player 4
				if (characterSelectionsScript[3].chosenChar == "Vovo")
				{

				}
				else if (characterSelectionsScript[3].chosenChar == "Fantasma")
				{

				}
				else if (characterSelectionsScript[3].chosenChar == "Tartaruga")
				{

				}
				else if (characterSelectionsScript[3].chosenChar == "Robo")
				{

				}
			}
			HasSelecterChar = false;
			CharacterSelectionUI.SetActive(false);
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