using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
	[Tooltip("Botão do teclado usado para resetar a scene.")]
	public KeyCode ResetKey;

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(ResetKey))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}