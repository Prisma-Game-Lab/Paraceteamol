
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    private string pauseButton1 = "p1_ps4_options";
    private string pauseButton2 = "p2_ps4_options";
    private string pauseButton3 = "p2_ps4_options";
    private string pauseButton4 = "p3_ps4_options";

    private void Start()
    {
        PauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButton(pauseButton1) || Input.GetButton(pauseButton2) || Input.GetButton(pauseButton3) || Input.GetButton(pauseButton4))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}