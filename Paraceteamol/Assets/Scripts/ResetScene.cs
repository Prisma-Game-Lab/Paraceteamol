using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
