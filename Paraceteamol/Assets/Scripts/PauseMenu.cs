
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string soundPause;
    [FMODUnity.EventRef]
    public string soundResume;

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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown(pauseButton1) || Input.GetButtonDown(pauseButton2) || Input.GetButtonDown(pauseButton3) || Input.GetButtonDown(pauseButton4))
        {
            if (GameIsPaused)
            {
                FMODUnity.RuntimeManager.PlayOneShot(soundResume);
                Resume();
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot(soundPause);
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