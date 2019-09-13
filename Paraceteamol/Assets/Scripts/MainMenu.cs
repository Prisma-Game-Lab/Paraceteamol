using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Load()
    {
        Debug.Log("Loading scene...");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
