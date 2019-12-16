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

	[Header("Spawn dos personagens")]
	[SerializeField] private GameObject SpawnP1;
	[SerializeField] private GameObject SpawnP2;
	[SerializeField] private GameObject SpawnP3;
	[SerializeField] private GameObject SpawnP4;
	private string _confirm = "pall_ps4_options";
	private bool HasSelecterChar = true;

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

		if (Input.GetButtonDown(_confirm) && HasSelecterChar)
		{
			if (characterSelectionsScript[0].hasSelected == true && characterSelectionsScript[1].hasSelected == true && characterSelectionsScript[2].hasSelected == true && characterSelectionsScript[3].hasSelected == true)
			{
				Debug.Log("Entrou na parte de instantiate");

				//Spawnar o player 1 no local do player 1
				if (characterSelectionsScript[0].chosenChar == "Vovo")
				{
					Instantiate(characterSelectionsScript[0].PLayerOp[0], SpawnP1.transform.position,  SpawnP1.transform.rotation);
				}
				else if (characterSelectionsScript[0].chosenChar == "Fantasma")
				{
					Instantiate(characterSelectionsScript[1].PLayerOp[0], SpawnP1.transform.position, SpawnP1.transform.rotation);
				}
				else if (characterSelectionsScript[0].chosenChar == "Tartaruga")
				{
					Instantiate(characterSelectionsScript[0].PLayerOp[2], SpawnP1.transform.position, SpawnP1.transform.rotation);
				}
				else if (characterSelectionsScript[0].chosenChar == "Robo")
				{
					Instantiate(characterSelectionsScript[0].PLayerOp[3], SpawnP1.transform.position, SpawnP1.transform.rotation);
				}
				
				//Spawnar o player 2 no local do player 2
				if (characterSelectionsScript[1].chosenChar == "Vovo")
				{
					Instantiate(characterSelectionsScript[1].PLayerOp[0], SpawnP2.transform.position, SpawnP2.transform.rotation);
				}
				else if (characterSelectionsScript[1].chosenChar == "Fantasma")
				{
					Instantiate(characterSelectionsScript[1].PLayerOp[1], SpawnP2.transform.position, SpawnP2.transform.rotation);
				}
				else if (characterSelectionsScript[1].chosenChar == "Tartaruga")
				{
					Instantiate(characterSelectionsScript[1].PLayerOp[2], SpawnP2.transform.position, SpawnP2.transform.rotation);
				}
				else if (characterSelectionsScript[1].chosenChar == "Robo")
				{
					Instantiate(characterSelectionsScript[1].PLayerOp[3], SpawnP2.transform.position, SpawnP2.transform.rotation);
				}

				//Spawnar o player 3 no local do player 3
				if (characterSelectionsScript[2].chosenChar == "Vovo")
				{
					Instantiate(characterSelectionsScript[2].PLayerOp[0], SpawnP3.transform.position, SpawnP3.transform.rotation);
				}
				else if (characterSelectionsScript[2].chosenChar == "Fantasma")
				{
					Instantiate(characterSelectionsScript[2].PLayerOp[1], SpawnP3.transform.position, SpawnP3.transform.rotation);
				}
				else if (characterSelectionsScript[2].chosenChar == "Tartaruga")
				{
					Instantiate(characterSelectionsScript[2].PLayerOp[2], SpawnP3.transform.position, SpawnP3.transform.rotation);
				}
				else if (characterSelectionsScript[2].chosenChar == "Robo")
				{
					Instantiate(characterSelectionsScript[2].PLayerOp[3], SpawnP3.transform.position, SpawnP3.transform.rotation);
				}

				//Spawnar o player 4 no local do player 4
				if (characterSelectionsScript[3].chosenChar == "Vovo")
				{
					Instantiate(characterSelectionsScript[3].PLayerOp[0], SpawnP4.transform.position, SpawnP4.transform.rotation);
				}
				else if (characterSelectionsScript[3].chosenChar == "Fantasma")
				{
					Instantiate(characterSelectionsScript[3].PLayerOp[1], SpawnP4.transform.position, SpawnP4.transform.rotation);
				}
				else if (characterSelectionsScript[3].chosenChar == "Tartaruga")
				{
					Instantiate(characterSelectionsScript[3].PLayerOp[2], SpawnP4.transform.position, SpawnP4.transform.rotation);
				}
				else if (characterSelectionsScript[3].chosenChar == "Robo")
				{
					Instantiate(characterSelectionsScript[3].PLayerOp[3], SpawnP4.transform.position, SpawnP4.transform.rotation);
				}
				
			}
			HasSelecterChar = false;
			CharacterSelectionUI.SetActive(false);
			PlacarUI.SetActive(true);
			TimerUI.SetActive(true);
			Time.timeScale = 1f;
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